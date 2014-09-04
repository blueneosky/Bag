using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Gitfs.Util;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Gitfs.Engine
{
    internal class Pull : Command
    {
        private const string ConstGitkeepFileName = ".gitkeep";

        private bool VerboseMode { get { return Env.VerboseMode; } }

        public override bool Proceed(string[] args)
        {
            Debug.Assert(args.Any() && String.Equals(args[0], Commands.ConstPull, StringComparison.OrdinalIgnoreCase));
            args = args.Skip(1).ToArray();

            bool success = ExtractArgs(ref args);
            if (false == success)
            {
                ShowHelp();
                return false;
            }

            try
            {
                if (false == Proceed())
                    return false;
            }
            catch (Exception e)
            {
                Output.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public bool Proceed()
        {
#warning TODO BETA - make shallow proceed
            return DeepProceed();
        }

        private bool DeepProceed()
        {
            IEnumerable<int> changesetIds = GetAllChangesets();

            if (false == VerboseMode)
            {
                Output.InitiateCursorPosition("Checkout from " + Env.VirtualServerPath + ": ");
                Output.WriteAtCursor("0%");
            }

            VersionControlServer server = Env.VersionControlServer;

            HashSet<ChangeType> changeTypes = new HashSet<ChangeType> { };

            int percent = 0;
            int totalChangesetIds = changesetIds.Count();
            int i = -1;
            foreach (int changesetId in changesetIds)
            {
                i++;

                Changeset changeset = server.GetChangeset(changesetId, true, true);

                CheckoutFromTfsAndCommit(new[] { changeset });
                continue;

                string commiter = changeset.Committer;
                DateTime date = changeset.CreationDate;
                string comment = changeset.Comment;
                UserDomainInfo commiterInfo = ActiveDirectoryHelper.GetUserDomainInfo(commiter);

                if (VerboseMode)
                {
                    //Output.WriteLine(changesetId + " (" + commiter + ") " + date + " " + comment);
                }
                else
                {
                    int newPercent = i * 100 / totalChangesetIds;
                    if (newPercent > percent)
                    {
                        percent = newPercent;
                        Output.WriteAtCursor(percent + "%");
                    }
                }

                Change[] changes = changeset.Changes;
                foreach (Change change in changes)
                {
                    //change.ChangeType== ChangeType.
                    changeTypes.Add(change.ChangeType);
                    //if (change.ChangeType == ChangeType.Merge)
                    //    Output.WriteLine("cool");
                    Output.WriteLine(change.Item.ServerItem);
                }
            }

            if (false == VerboseMode)
            {
                Output.LastWriteAtCursor("Done");
                Output.WriteLine("");
            }

            foreach (ChangeType changeType in changeTypes)
            {
                Output.WriteLine(changeType.ToString());
            }

#warning TODO ALPHA point
            return true;
        }

        #region CheckoutFromTfsAndCommit

        private bool CheckoutFromTfsAndCommit(IEnumerable<Changeset> changesets)
        {
            bool success;

            if (false == changesets.Any())
                return false;

            // checkout from tfs
            foreach (Changeset changeset in changesets)
            {
                success = CheckoutFromTfs(changeset);
                if (false == success)
                    return false;
            }

            Changeset lastChangeset = changesets.Last();
            string commiter = lastChangeset.Committer;
            DateTime date = lastChangeset.CreationDate;
            string comment = lastChangeset.Comment;
            UserDomainInfo commiterInfo = ActiveDirectoryHelper.GetUserDomainInfo(commiter);

#if DEBUG
            return true;
#endif
            // commit
            success = GitHelper.Commit(comment, date, commiterInfo.GitAuthor);
            if (false == success)
                return false;

            if (Env.WithTag)
            {
                // tag
                int changesetId = lastChangeset.ChangesetId;
                string tagName = String.Format("tfs_C{1}", changesetId);
                success = GitHelper.Tag(tagName);
                if (false == success)
                    return false;
            }

            return success;
        }

        private bool CheckoutFromTfs(Changeset changeset)
        {
            Change[] changes = changeset.Changes;

            List<Change> addFilesList = new List<Change>(changes.Length);
            List<Change> addFoldersList = new List<Change>(changes.Length);
            List<Change> removeFilesList = new List<Change>(changes.Length);
            List<Change> removeFoldersList = new List<Change>(changes.Length);

            foreach (Change change in changes)
            {
                // Delete   =>	RM
                // Undelete =>	Add
                // Rename   =>	RM old; Add new
                // <autre>  =>	Add

                ChangeType changeType = change.ChangeType;
                ItemType itemType = change.Item.ItemType;
                if (itemType == ItemType.Any)
                    return false;

                if (changeType.HasFlag(ChangeType.Delete))
                {
                    // delete
                    if (itemType == ItemType.File)
                    {
                        removeFilesList.Add(change);
                    }
                    else
                    {
                        removeFoldersList.Add(change);
                    }
                }
                else if (changeType.HasFlag(ChangeType.Undelete))
                {
                    // undelete
                    if (itemType == ItemType.File)
                    {
                        addFilesList.Add(change);
                    }
                    else
                    {
                        addFoldersList.Add(change);
                    }
                }
                else if (changeType.HasFlag(ChangeType.Rename))
                {
                    // rename
                    Change previousChange = GetPreviousChange(change);
                    if (previousChange == null)
                        return false;

                    if (itemType == ItemType.File)
                    {
                        removeFilesList.Add(previousChange);
                        addFilesList.Add(change);
                    }
                    else
                    {
                        removeFoldersList.Add(previousChange);
                        addFoldersList.Add(change);
                    }
                }
                else
                {
                    // other
                    if (itemType == ItemType.File)
                    {
                        addFilesList.Add(change);
                    }
                    else
                    {
                        addFoldersList.Add(change);
                    }
                }
            }

            bool dryRunMode = Env.DryRunMode;
            // delete files
            foreach (Change change in removeFilesList)
            {
                if (dryRunMode)
                {
                    Output.WriteLine("Remove File : " + change.Item.ServerItem);
                }
                else
                {
#warning TODO ALPHA ALPHA point

                }
            }

            // delete folders
            foreach (Change change in removeFoldersList.OrderByDescending(c => c.Item.ServerItem.Length))
            {
                if (dryRunMode)
                {
                    Output.WriteLine("Remove File : " + change.Item.ServerItem + ConstGitkeepFileName);
                    Output.WriteLine("Remove Folder : " + change.Item.ServerItem);
                }
                else
                {
#warning TODO ALPHA ALPHA point

                }
            }

            // add folders
            foreach (Change change in addFoldersList.OrderBy(c => c.Item.ServerItem.Length))
            {
                if (dryRunMode)
                {
                    Output.WriteLine("Add Folder : " + change.Item.ServerItem);
                    Output.WriteLine("Add File : " + change.Item.ServerItem + ConstGitkeepFileName);
                }
                else
                {
#warning TODO ALPHA ALPHA point

                }
            }

            // add files
            foreach (Change change in addFilesList)
            {
                if (dryRunMode)
                {
                    Output.WriteLine("Add File : " + change.Item.ServerItem);
                }
                else
                {
#warning TODO ALPHA ALPHA point

                }
            }

            return true;
        }

        #endregion CheckoutFromTfsAndCommit

        private Change GetPreviousChange(Change change)
        {
            Item item = change.Item;
            VersionSpec versionSpec = VersionSpec.ParseSingleSpec(Convert.ToString(item.ChangesetId), null);

            Changeset previousChangeset = Env.VersionControlServer.QueryHistory(
                item.ServerItem
                , versionSpec
                , item.DeletionId
                , RecursionType.None
                , null  // any user
                , null  // from the begining
                , versionSpec //versionSpec
                , Int32.MaxValue
                , true
                , false)
                .Cast<Changeset>()
                .Skip(1)    // first point to versionSpec
                .FirstOrDefault();
            if (previousChangeset == null)
                return null;
            Change previousChange = previousChangeset.Changes
                .Where(c => c.Item.ItemId == item.ItemId)
                .FirstOrDefault();
            if (previousChange == null)
                return null;

            return previousChange;
        }

        private IEnumerable<int> GetAllChangesets()
        {
            Output.InitiateCursorPosition("Get changesets from " + Env.VirtualServerPath + ": ");
            Output.WriteAtCursor("0%");

            VersionControlServer server = Env.VersionControlServer;

            List<int> changesetIds = new List<int> { };
            IEnumerable<Changeset> query = server.QueryHistory(Env.Serverpath, VersionSpec.Latest, 0, RecursionType.Full, null, null, null, Int32.MaxValue, false, true)
                .OfType<Changeset>();
            int? lastChangesetId = null;
            int percent = 0;
            foreach (Changeset changeset in query)
            {
                int changesetId = changeset.ChangesetId;
                if (lastChangesetId == null)
                    lastChangesetId = changesetId;
                int newPercent = (lastChangesetId.Value - changesetId) * 100 / lastChangesetId.Value;
                if (newPercent > percent)
                {
                    percent = newPercent;
                    Output.WriteAtCursor(percent + "%");
                }

                changesetIds.Add(changesetId);
            }
            changesetIds.Reverse();
            Output.LastWriteAtCursor("Done");
            Output.WriteLine("");

            return changesetIds;
        }

        private bool ExtractArgs(ref string[] args)
        {
            bool? _dryRunMode = null;
            bool? _verboseMode = null;
            bool? _deepMode = null;

            int targetCount = 0;
            while (args.Any())
            {
                string arg = args[0];
                if (arg.StartsWith("-", StringComparison.OrdinalIgnoreCase))
                {
                    switch (arg)
                    {
                        case "--dry-run":
                            _dryRunMode = _dryRunMode ?? true;
                            break;

                        case "-v":
                        case "--verbose":
                            _verboseMode = _verboseMode ?? true;
                            break;

                        case "--deep":
                            _deepMode = _deepMode ?? true;
                            break;

                        case "--shallow":
                            _deepMode = _deepMode ?? false;
                            break;

                        default:
                            return false;
                    }
                }
                else
                {
                    return false;
                }
                args = args.Skip(1).ToArray();
            }

            string rootPath;
            bool success = GitHelper.GetRootPath(out rootPath);
            if ((false == success) || String.IsNullOrEmpty(rootPath))
            {
                Output.WriteLine("You are not under a git repo");
                return false;
            }
            Directory.SetCurrentDirectory(rootPath);

            Env.LoadFromGitConfig();
            Env.DryRunMode = _dryRunMode ?? false;      // default value
            Env.VerboseMode = _verboseMode ?? false;    // default value
            if (_deepMode.HasValue)
                Env.DeepMode = _deepMode.Value;

            if (String.IsNullOrEmpty(Env.Projectcollection) || String.IsNullOrEmpty(Env.Serverpath))
            {
                Output.WriteLine("The repo are not initialised for git-tfs");
                return false;
            }

            bool isClean = GitHelper.IsCleanHead();
            if (false == isClean)
            {
                Output.WriteLine("The repo must be clean, without modification into HEAD");
                return false;
            }

            string lastCommit = GitHelper.GetLastCommit();
            string lastTfsCommit = GitHelper.ConfigGetLastCommit();
            if ((lastCommit != null) && (false == String.Equals(lastCommit, lastTfsCommit, StringComparison.OrdinalIgnoreCase)))
            {
                Output.WriteLine("The repo must be at the same state than the Tfs server");
                return false;
            }

            return true;
        }

        public override void ShowHelp()
        {
            StringBuilder sb = new StringBuilder()
                .AppendLine("usage: git-tfs pull [--verbose|-v] [--deep|--shallow]")
                .AppendLine("")
                .AppendLine("Arguments:")
                .AppendLine("    --dry-run             show all operations without write anythings (default: none)")
                .AppendLine("    --verbose, -v         Verbose mode (default: none)")
                .AppendLine("    --deep, --shallow     Performs a shallow fetch of the specified depth, or a deep fetch of all TFS changesets since the last fetch. If omitted, the depth value provided during clone or configure is used.")
                .AppendLine("")
                .AppendLine("Pulls the latest code from TFS and merge/rebase the changes into master.")
                ;
        }
    }
}