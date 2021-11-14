using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;
using SatisfactoryModeler.Views.Editors;
using System;

namespace SatisfactoryModeler.ViewModels.Editors
{
    public class DoubleEditorViewModel : ValueEditorViewModel<double?>, IModifiableValueEditorViewModel
    {
        static DoubleEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new DoubleEditorView(), typeof(IViewFor<DoubleEditorViewModel>));
        }

        public DoubleEditorViewModel()
            : this(0, double.MinValue, double.MaxValue)
        {
        }

        public DoubleEditorViewModel(double current, double min, double max)
        {
            Value = current;
            ValueChanged.Subscribe(_ =>
            {
                if (Value < min) Value = min;
                if (Value > max) Value = max;
            });
        }

        object IModifiableValueEditorViewModel.Value
        {
            get => Value;
            set => Value = Convert.ToDouble(value);
        }
    }
}
