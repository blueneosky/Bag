using DynamicData;
using Newtonsoft.Json.Linq;
using NodeNetwork.Views;
using ReactiveUI;
using SatisfactoryModeler.Assets.Converters;
using SatisfactoryModeler.Models;
using SatisfactoryModeler.Persistance.Networks;
using SatisfactoryModeler.ViewModels.Editors;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public class ExternalSourceViewModel : PersistableNodeViewModel<ExternalSource>
    {
        static ExternalSourceViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<ExternalSourceViewModel>));
            PersistableViewModelFactory.Instance.Register<ExternalSource, ExternalSourceViewModel>(m => new ExternalSourceViewModel(m));
        }

        public PersistableValueNodeInputViewModel<string> Tag { get; }
        public PersistableValueNodeInputViewModel<object> Type { get; }
        public PersistableValueNodeInputViewModel<double?> Rate { get; }

        public PersistableValueNodeOutputViewModel<ItemFlow?> Output { get; }

        public ExternalSourceViewModel() : this(null) { }

        public ExternalSourceViewModel(ExternalSource source) : base(source)
        {
            Name = "External src";

            Tag = CreateInput<string>("Tag", source, new StringValueEditorViewModel());
            Tag.Name = "Tag";
            Tag.Port.IsVisible = false;

            Type = CreateInput<object>("ItemType", source, new ComboEditorViewModel(ItemType.All), ItemTypeToIdConverter.Default);
            Type.Name = "Item";
            Type.Port.IsVisible = false;

            Rate = CreateInput<double?>("Rate", source, new DoubleEditorViewModel(0, 0, double.MaxValue));
            Rate.Name = "Rate";
            Rate.Port.IsVisible = false;

            Output = CreateOutput<ItemFlow?>("Output", source);
            Output.Name = "output";

            var delivery = this.WhenAnyValue(vm => vm.Type.Value, vm => vm.Rate.Value)
               .Select(_ => ItemFlow.From((ItemType)this.Type.Value, this.Rate.Value));
            Output.Value = delivery;

            SetupDynamicOutput(Output, v => v?.Type, v => v?.Rate, "{0}\n{1}/min", "ouput");

            Inputs.Add(Tag);
            Inputs.Add(Type);
            Inputs.Add(Rate);

            Outputs.Add(Output);
        }
    }
}
