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

                if (changesetId < 4420) continue;
                Changeset changeset = server.GetChangeset(changesetId, true, true);
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

            //Add                                   => Add
            //Add, Edit                             => Add, Content
            //Edit                                  => Content
            //Delete                                => Rm
            //Rename                                => Rm oldX, Add newX
            //Edit, Rename                          => Rm oldX, Add newX, Content
            //Rename, Delete                        => Rm
            //Edit, Merge                           => Content
            //Merge                                 => Add
            //Encoding, Branch                      => Add
            //Edit, Rename, Merge                   => Rm oldX, Add newX, Content
            //Encoding, Branch, Merge               => Add, Content
            //Delete, Merge                         => Rm
            //Rename, Merge                         => Rm oldX, Add newX, Content
            //Edit, Encoding, Branch, Merge         => Add, Content
            //Undelete                              => Add, Content
            //Undelete, Merge                       => Add, Content
            //Edit, Undelete, Merge                 => Add, Content
            //Edit, Undelete                        => Add, Content
            //Rename, Delete, Merge                 => Rm

            // =>

            // Delete   =>	RM
            // Undelete =>	Add; Content
            // Rename   =>	RM old; Add new; Content si autre flag
            // <autre>  =>	Add si existe pas; Content

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

            foreach (Changeset changeset in changesets)
            {
                success = CheckoutFromTfs(changeset);
                if (false == success)
                    return false;
            }

            Changeset lastChangeset = changesets.Last();
            int changesetId = lastChangeset.ChangesetId;
            string commiter = lastChangeset.Committer;
            DateTime date = lastChangeset.CreationDate;
            string comment = lastChangeset.Comment;
            UserDomainInfo commiterInfo = ActiveDirectoryHelper.GetUserDomainInfo(commiter);

            success = GitHelper.Commit(comment, date, commiterInfo.GitAuthor);

            return success;
        }

        private bool CheckoutFromTfs(Changeset changeset)
        {
            return false;
#warning TODO ALPHA ALPHA point
        }

        #endregion CheckoutFromTfsAndCommit

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
            Env.VerboseMode = _verboseMode ?? false;   // default value
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
                .AppendLine("    --verbose, -v         Verbose mode (default: none)")
                .AppendLine("    --deep, --shallow     Performs a shallow fetch of the specified depth, or a deep fetch of all TFS changesets since the last fetch. If omitted, the depth value provided during clone or configure is used.")
                .AppendLine("")
                .AppendLine("Pulls the latest code from TFS and merge/rebase the changes into master.")
                ;
        }
    }
}