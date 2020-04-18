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

        public PersistableValueNodeInputViewModel<Flow?> Input { get; }
        public PersistableValueNodeInputViewModel<int?> LeftRate { get; }
        public PersistableValueNodeInputViewModel<int?> CenterRate { get; }
        public PersistableValueNodeInputViewModel<int?> RightRate { get; }

        public PersistableValueNodeOutputViewModel<Flow?> Left { get; }
        public PersistableValueNodeOutputViewModel<Flow?> Center { get; }
        public PersistableValueNodeOutputViewModel<Flow?> Right { get; }

        public SplitterViewModel() : this(null) { }

        public SplitterViewModel(Splitter source) : base(source)
        {
            Name = "Splitter";
            //HeaderIcon = IconsManager.Current.Splitter;
            
            Input = CreateInput<Flow?>("Input", source, null);
            Input.Name = "Input";

            LeftRate = CreateInput<int?>("RightRate", source, new IntegerValueEditorViewModel(33, 0, 100));
            LeftRate.Name = "Right rate (%)";
            LeftRate.Port.IsVisible = false;

            CenterRate = CreateInput<int?>("RightRate", source, new IntegerValueEditorViewModel(33, 0, 100));
            CenterRate.Name = "Right rate (%)";
            CenterRate.Port.IsVisible = false;

            RightRate = CreateInput<int?>("RightRate", source, new IntegerValueEditorViewModel(33, 0, 100));
            RightRate.Name = "Right rate (%)";
            RightRate.Port.IsVisible = false;

            Left = CreateOutput<Flow?>("Left", source, null);
            Left.Name = "Left";

            Center = CreateOutput<Flow?>("Center", source, null);
            Center.Name = "Center";

            Right = CreateOutput<Flow?>("Right", source, null);
            Right.Name = "Right";

            Left.Value = this.WhenAnyValue(vm => vm.Input.Value, vm => vm.LeftRate.Value)
                .Select(_ => Flow.From(
                 this.Input.Value?.Type,
                 this.Input.Value?.Rate * this.LeftRate.Value / 100.0));
            Center.Value = this.WhenAnyValue(vm => vm.Input.Value, vm => vm.CenterRate.Value)
                .Select(_ => Flow.From(
                 this.Input.Value?.Type,
                 this.Input.Value?.Rate * this.CenterRate.Value / 100.0));
            Right.Value = this.WhenAnyValue(vm => vm.Input.Value, vm => vm.RightRate.Value)
                .Select(_ => Flow.From(
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
