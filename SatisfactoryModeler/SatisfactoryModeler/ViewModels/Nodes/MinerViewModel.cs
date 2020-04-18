using DynamicData;
using NodeNetwork.Views;
using ReactiveUI;
using SatisfactoryModeler.Assets;
using SatisfactoryModeler.Models;
using SatisfactoryModeler.Persistance.Networks;
using SatisfactoryModeler.ViewModels.Editors;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public class MinerViewModel : ProducerNodeViewModel<Miner>
    {
        static MinerViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<MinerViewModel>));
            PersistableViewModelFactory.Instance.Register<Miner, MinerViewModel>(m => new MinerViewModel(m));
        }

        public PersistableValueNodeInputViewModel<object> MinerLevel { get; }
        public PersistableValueNodeInputViewModel<object> NodePurity { get; }
        public PersistableValueNodeInputViewModel<object> NodeType { get; }

        public MinerViewModel() : this(null) { }

        public MinerViewModel(Miner source) : base(source)
        {
            Name = "Miner";
            HeaderIcon = IconsManager.Current.Miner;

            MinerLevel = CreateInput<object>("MinerLevel", source, new EnumEditorViewModel(typeof(MinerLevel)));
            MinerLevel.Name = "Level";
            MinerLevel.Port.IsVisible = false;

            NodePurity = CreateInput<object>("NodePurity", source, new EnumEditorViewModel(typeof(ResourceNodePurity)));
            NodePurity.Name = "Purity";
            NodePurity.Port.IsVisible = false;

            NodeType = CreateInput<object>("NodeType", source, new EnumEditorViewModel(typeof(ResourceNodeType)));
            NodeType.Name = "Resource";
            NodeType.Port.IsVisible = false;

            var production = this.WhenAnyValue(vm => vm.MinerLevel.Value, vm => vm.NodeType.Value, vm => vm.NodePurity.Value, vm => vm.Override.Value)
               .Select(_ => Flow.From(
                   (ItemType?)(ResourceNodeType?)this.NodeType.Value,
                   60.0 * (MinerLevel.Value as MinerLevel?)?.ToFactor() * (NodePurity.Value as ResourceNodePurity?)?.ToFactor() * (Override.Value / 100.0)));
            Output.Value = production;

            SetupDynamicOutput(Output, v => v?.Type, v => v?.Rate, "{0}\n{1}/min", "ouput");

            Inputs.Add(MinerLevel);
            Inputs.Add(NodePurity);
            Inputs.Add(Override);
            Inputs.Add(NodeType);

            Outputs.Add(Output);
        }
    }
}
