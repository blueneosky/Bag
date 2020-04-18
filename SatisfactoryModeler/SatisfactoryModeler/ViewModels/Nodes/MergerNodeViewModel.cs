﻿using DynamicData;
using Newtonsoft.Json.Linq;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using SatisfactoryModeler.Assets;
using SatisfactoryModeler.Models;
using SatisfactoryModeler.Persistance.Networks;
using SatisfactoryModeler.Persistance.Objects;
using SatisfactoryModeler.ViewModels.Editors;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public class MergerNodeViewModel : PersistableNodeViewModel<Merger>
    {
        static MergerNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<MergerNodeViewModel>));
            PersistableViewModelFactory.Instance.Register<Merger, MergerNodeViewModel>(m => new MergerNodeViewModel(m));
        }
        public PersistableValueNodeInputViewModel<ItemRate?> Left { get; }
        public PersistableValueNodeInputViewModel<ItemRate?> Center { get; }
        public PersistableValueNodeInputViewModel<ItemRate?> Right { get; }

        public PersistableValueNodeOutputViewModel<ItemRate?> Output { get; }

        public MergerNodeViewModel() : this(null) { }

        public MergerNodeViewModel(Merger source) : base(source)
        {
            Name = "Merger";
            //HeaderIcon = IconsManager.Current.Merger;

            Left = CreateInput<ItemRate?>("Left", source, null);
            Left.Name = "Left";

            Center = CreateInput<ItemRate?>("Center", source, null);
            Center.Name = "Center";

            Right = CreateInput<ItemRate?>("Right", source, null);
            Right.Name = "Right";

            Output = CreateOutput<ItemRate?>("Output", source, null);
            Output.Name = "Output";

            Output.Value = this.WhenAnyValue(vm => vm.Left.Value, vm => vm.Center.Value, vm => vm.Right.Value)
                 .Select(_ => ItemRate.From(
                  (this.Left.Value ?? this.Center.Value ?? this.Right.Value)?.Type,
                  (this.Left.Value?.Rate ?? 0.0) + (this.Center.Value?.Rate ?? 0.0) + (this.Right.Value?.Rate ?? 0.0)));

            SetupDynamicOutput(Output, v => v?.Type, v => v?.Rate, "{0}\n{1}/min", "output");

            this.Inputs.Add(Left);
            this.Inputs.Add(Center);
            this.Inputs.Add(Right);
            this.Outputs.Add(Output);
        }
    }
}