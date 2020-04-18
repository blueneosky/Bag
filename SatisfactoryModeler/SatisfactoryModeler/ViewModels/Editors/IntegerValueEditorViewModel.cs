using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;
using SatisfactoryModeler.Views.Editors;
using System;

namespace SatisfactoryModeler.ViewModels.Editors
{
    public class IntegerValueEditorViewModel : ValueEditorViewModel<int?>, IModifiableValueEditorViewModel
    {
        static IntegerValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new IntegerValueEditorView(), typeof(IViewFor<IntegerValueEditorViewModel>));
        }

        public IntegerValueEditorViewModel()
            : this(0, int.MinValue, int.MaxValue)
        {
        }

        public IntegerValueEditorViewModel(int current, int min, int max)
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
            set => Value = Convert.ToInt32(value);
        }

    }
}
