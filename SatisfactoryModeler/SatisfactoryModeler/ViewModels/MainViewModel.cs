using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using NodeNetwork;
using NodeNetwork.Toolkit.NodeList;
using NodeNetwork.ViewModels;
using ReactiveUI;
using SatisfactoryModeler.Persistance;
using SatisfactoryModeler.Persistance.Networks;
using SatisfactoryModeler.ViewModels.Nodes;

namespace SatisfactoryModeler.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        static MainViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<MainViewModel>));
        }

        public NodeListViewModel ListViewModel { get; } = new NodeListViewModel();
        public NetworkViewModel NetworkViewModel { get; } = new NetworkViewModel();

        public MainViewModel()
        {
            var vmTypes = this.GetType().Assembly.GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(NodeViewModel).IsAssignableFrom(t));
            foreach (var vmType in vmTypes)
            {
                ListViewModel.AddNodeType(vmType);
            }

            try
            {
                Restore();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                NetworkViewModel.Connections.Clear();
                NetworkViewModel.Nodes.Clear();
            }
          
            NetworkViewModel.Validator = network =>
            {
                //bool containsLoops = GraphAlgorithms.FindLoops(network).Any();
                //if (containsLoops)
                //{
                //    return new NetworkValidationResult(false, false, new ErrorMessageViewModel("Network contains loops!"));
                //}

                //bool containsDivisionByZero = GraphAlgorithms.GetConnectedNodesBubbling(output)
                //    .OfType<DivisionNodeViewModel>()
                //    .Any(n => n.Input2.Value == 0);
                //if (containsDivisionByZero)
                //{
                //    return new NetworkValidationResult(false, true, new ErrorMessageViewModel("Network contains division by zero!"));
                //}

                return new NetworkValidationResult(true, true, null);
            };
        }

        private string SessionFilePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SatisfactoryModeler",
            "session.json");

        private void Restore()
        {
            if (!File.Exists(SessionFilePath))
                return;

            var engine = PersistanceFactory.Instance.GetDefault<NodesNetwork>();
            var nodesNetwork = engine.RestoreFrom(SessionFilePath);

            var nodesByIds = new Dictionary<Guid, NodeViewModel>();

            var nodes = nodesNetwork.Nodes.Select(PersistableViewModelFactory.Instance.Create)
                .OfType<NodeViewModel>();
            foreach (var node in nodes)
            {
                NetworkViewModel.Nodes.Add(node);
                nodesByIds.Add(node.CastTo<IPersistable>().Id, node);
            }

            var connections = nodesNetwork.Connections;
            foreach (var connection in connections)
            {
                var nodeInput = nodesByIds[connection.InputId].Inputs.Items
                    .OfType<IPersistablePort>()
                    .First(p => p.PortName == connection.InputPortName)
                    .CastTo<NodeInputViewModel>();
                var nodeOutput = nodesByIds[connection.OutputId].Outputs.Items
                    .OfType<IPersistablePort>()
                    .First(p => p.PortName == connection.OutputPortName)
                    .CastTo<NodeOutputViewModel>();
                NetworkViewModel.Connections.Add(new ConnectionViewModel(NetworkViewModel, nodeInput, nodeOutput));
            }
        }

        public async Task Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SessionFilePath));

            var nodes = NetworkViewModel.Nodes.Items
                .OfType<IPersistable>().Persist<BaseNode>().ToArray();

            var connections = NetworkViewModel.Connections.Items
                .Select(Persist).ToArray();

            await Task.Run(() =>
            {
                var nodesNetwork = new NodesNetwork
                {
                    Nodes = nodes,
                    Connections = connections,
                };

                var engine = PersistanceFactory.Instance.GetDefault<NodesNetwork>();
                engine.StoreTo(nodesNetwork, SessionFilePath);
            });
        }

        private static Connection Persist(ConnectionViewModel connection)
        {
            var input = connection.Input.CastTo<IPersistablePort>();
            var output = connection.Output.CastTo<IPersistablePort>();
            return new Connection
            {
                InputId = input.Parent.Id,
                InputPortName = input.PortName,
                OutputId = output.Parent.Id,
                OutputPortName = output.PortName,
            };
        }
    }
}
