using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Gitfs.Util;

namespace Gitfs.Engine
{
    internal class Clone : Command
    {


        private List<int> _changesetIds;

        private bool VerboseMode { get { return Env.VerboseMode; } }

        public override bool Proceed(string[] args)
        {
            Debug.Assert(args.Any() && String.Equals(args[0], Commands.ConstClone, StringComparison.OrdinalIgnoreCase));
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

        private bool Proceed()
        {
            GetAllChangesets();

            if (false == VerboseMode)
            {
                Output.InitiateCursorPosition("Checkout from " + Env.VirtualServerPath + ": ");
                Output.WriteAtCursor("0%");
            }

            VersionControlServer server = Env.VersionControlServer;

            HashSet<ChangeType> changeTypes = new HashSet<ChangeType> { };

            int percent = 0;
            for (int i = 0; i < _changesetIds.Count; i++)
            {
                int changesetId = _changesetIds[i];
                Changeset changeset = server.GetChangeset(changesetId, true, true);
                string commiter = changeset.Committer;
                DateTime date = changeset.CreationDate;
                string comment = changeset.Comment;

                if (VerboseMode)
                {
                    Output.WriteLine(changesetId + " (" + commiter + ") " + date + " " + comment);
                }
                else
                {
                    int newPercent = i * 100 / _changesetIds.Count;
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
                }
            }

            if (false == VerboseMode)
            {
                Output.LastWriteAtCursor("Done");
                Output.WriteLine("");
            }

//Add, Encoding
//Add, Edit, Encoding
//Edit
//Delete
//Rename
//Edit, Rename
//Rename, Delete
//Edit, Merge
//Merge
//Encoding, Branch
//Edit, Rename, Merge
//Encoding, Branch, Merge
//Delete, Merge
//Rename, Merge
//Edit, Encoding, Branch, Merge
//Undelete
//Undelete, Merge
//Edit, Undelete, Merge
//Edit, Undelete
//Rename, Delete, Merge

            foreach (ChangeType changeType in changeTypes)
            {
                Output.WriteLine(changeType.ToString());
            }

#warning TODO ALPHA point
            return true;
        }

        private void GetAllChangesets()
        {
            Output.InitiateCursorPosition("Get changesets from " + Env.VirtualServerPath + ": ");
            Output.WriteAtCursor("0%");

            VersionControlServer server = Env.VersionControlServer;

            _changesetIds = new List<int> { };
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

                _changesetIds.Add(changesetId);
            }
            _changesetIds.Reverse();
            Output.LastWriteAtCursor("Done");
            Output.WriteLine("");
        }

        private bool ExtractArgs(ref string[] args)
        {
            bool? _verboseMode = null;
            bool? _deepMode = null;
            bool? _withTag = null;
            string _projectcollection = null;
            string _serverpath = null;
            string _directory = null;

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

                        case "--tag":
                            _withTag = _withTag ?? true;
                            break;

                        case "--no-tag":
                            _withTag = _withTag ?? false;
                            break;

                        default:
                            return false;
                    }
                }
                else
                {
                    switch (targetCount)
                    {
                        case 0:
                            _projectcollection = arg;
                            break;

                        case 1:
                            _serverpath = arg;
                            break;

                        case 2:
                            _directory = arg;
                            break;

                        default:
                            return false;
                    }
                    targetCount++;
                }
                args = args.Skip(1).ToArray();
            }

            if (String.IsNullOrEmpty(_projectcollection)
                || String.IsNullOrEmpty(_serverpath))
            {
                return false;
            }

            _directory = _directory ?? "./" + _serverpath.Split('/', '\\').LastOrDefault();

            Env.VerboseMode = _verboseMode ?? false;   // default value
            Env.DeepMode = _deepMode ?? true;          // default value
            Env.WithTag = _withTag ?? true;            // default value
            Env.Projectcollection = _projectcollection;
            Env.Serverpath = _serverpath;
            Env.Directory = Path.GetFullPath(_directory);

            return true;
        }

        public override void ShowHelp()
        {
            StringBuilder sb = new StringBuilder()
                .AppendLine("usage: git-tfs clone [--deep|--shallow] <projectcollection> <serverpath> [<directory>]")
                .AppendLine("")
                .AppendLine("Arguments:")
                .AppendLine("    --verbose, -v         Verbose mode (default: none)")
                .AppendLine("    --deep, --shallow")
                .AppendLine("                          Creates a shallow clone of the specified depth, or a deep clone of all TFS changesets, and sets the default depth for pull operations (default: --deep)")
                .AppendLine("    --tag, --no-tag       Determine whether to tag all commits that map to changesets downloaded from TFS (default: --tag)")
                .AppendLine("    <projectcollection>   The TFS project collection URL")
                .AppendLine("    <serverpath>          The TFS server path to clone")
                .AppendLine("    <directory>           The name of a new directory to clone into.  If the directory is not specified, the name of the TFS folder will be used.  If this directory exists, it must be empty")
                .AppendLine("")
                .AppendLine("Clones a path from Microsoft Team Foundation Server, creating a new git repository.")
                ;
            Output.Write(sb.ToString());
        }
    }
}