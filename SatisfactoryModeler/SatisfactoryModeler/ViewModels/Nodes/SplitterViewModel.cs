using DynamicData;
using Newtonsoft.Json.Linq;
using NodeNetwork.Views;
using ReactiveUI;
using SatisfactoryModeler.Models;
using SatisfactoryModeler.Persistance.Networks;
using SatisfactoryModeler.ViewModels.Editors;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public class SplitterViewModel : PersistableNodeViewModel<Splitter>
    {
        static SplitterViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<SplitterViewModel>));
            PersistableViewModelFactory.Instance.Register<Splitter, SplitterViewModel>(m => new SplitterViewModel(m));
        }

        public PersistableValueNodeInputViewModel<ItemFlow?> Input { get; }
        public PersistableValueNodeInputViewModel<double?> LeftRate { get; }
        public PersistableValueNodeInputViewModel<double?> CenterRate { get; }
        public PersistableValueNodeInputViewModel<double?> RightRate { get; }

        public PersistableValueNodeOutputViewModel<ItemFlow?> Left { get; }
        public PersistableValueNodeOutputViewModel<ItemFlow?> Center { get; }
        public PersistableValueNodeOutputViewModel<ItemFlow?> Right { get; }

        public SplitterViewModel() : this(null) { }

        public SplitterViewModel(Splitter source) : base(source)
        {
            Name = "Splitter";
            //HeaderIcon = IconsManager.Current.Splitter;
            
            Input = CreateInput<ItemFlow?>("Input", source);
            Input.Name = "Input";

            LeftRate = CreateInput<double?>("RightRate", source, new DoubleEditorViewModel(1/3.0, 0, 100));
            LeftRate.Name = "Right rate (%)";
            LeftRate.Port.IsVisible = false;

            CenterRate = CreateInput<double?>("RightRate", source, new DoubleEditorViewModel(1 / 3.0, 0, 100));
            CenterRate.Name = "Right rate (%)";
            CenterRate.Port.IsVisible = false;

            RightRate = CreateInput<double?>("RightRate", source, new DoubleEditorViewModel(1 / 3.0, 0, 100));
            RightRate.Name = "Right rate (%)";
            RightRate.Port.IsVisible = false;

            Left = CreateOutput<ItemFlow?>("Left", source);
            Left.Name = "Left";

            Center = CreateOutput<ItemFlow?>("Center", source);
            Center.Name = "Center";

            Right = CreateOutput<ItemFlow?>("Right", source);
            Right.Name = "Right";

            Left.Value = this.WhenAnyValue(vm => vm.Input.Value, vm => vm.LeftRate.Value)
                .Select(_ => ItemFlow.From(
                 this.Input.Value?.Type,
                 this.Input.Value?.Rate * this.LeftRate.Value / 100.0));
            Center.Value = this.WhenAnyValue(vm => vm.Input.Value, vm => vm.CenterRate.Value)
                .Select(_ => ItemFlow.From(
                 this.Input.Value?.Type,
                 this.Input.Value?.Rate * this.CenterRate.Value / 100.0));
            Right.Value = this.WhenAnyValue(vm => vm.Input.Value, vm => vm.RightRate.Value)
                .Select(_ => ItemFlow.From(
                 this.Input.Value?.Type,
                 this.Input.Value?.Rate * this.RightRate.Value / 100.0));

            SetupDynamicOutput(Left, v => v?.Type, v => v?.Rate, "[l] {0}\n{1}/min", "left");
            SetupDynamicOutput(Center, v => v?.Type, v => v?.Rate, "[c] {0}\n{1}/min", "center");
            SetupDynamicOutput(Right, v => v?.Type, v => v?.Rate, "[r] {0}\n{1}/min", "right");


            this.Inputs.Add(Input);
            this.Inputs.Add(LeftRate);
            this.Inputs.Add(CenterRate);
            this.Inputs.Add(RightRate);
            this.Outputs.Add(Left);
            this.Outputs.Add(Center);
            this.Outputs.Add(Right);
        }
    }
}
