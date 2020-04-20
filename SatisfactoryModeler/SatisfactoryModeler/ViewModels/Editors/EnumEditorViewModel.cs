using System;
using System.Linq;
using System.Reactive.Linq;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;
using SatisfactoryModeler.Views.Editors;

namespace SatisfactoryModeler.ViewModels.Editors
{
    public class EnumEditorViewModel : ValueEditorViewModel<object>, IModifiableValueEditorViewModel
    {
        static EnumEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new EnumEditorView(), typeof(IViewFor<EnumEditorViewModel>));
        }

        private readonly Type enumType;

        public object[] Options { get; }
        public string[] OptionLabels { get; }

        #region SelectedOptionIndex
        private int _selectedOptionIndex;

        public int SelectedOptionIndex
        {
            get => _selectedOptionIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedOptionIndex, value);
        }
        #endregion

        public EnumEditorViewModel(Type enumType)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException(enumType.Name + " is not an enum type");

            Options = Enum.GetValues(enumType).Cast<object>().ToArray();
            OptionLabels = Options.Select(c => Enum.GetName(enumType, c)).ToArray();

            this.WhenAnyValue(vm => vm.SelectedOptionIndex)
                .Select(i => i == -1 ? null : Options[i])
                .BindTo(this, vm => vm.Value);
            this.enumType = enumType;
        }

        object IModifiableValueEditorViewModel.Value
        {
            get => Value;
            set
            {
                if (value == null) return;
                var val = Enum.Parse(this.enumType, Convert.ToString(value));
                var index = Options.Select((v, i) => (v, i))
                    .Where(t => val.Equals(t.v))
                    .Select(t => t.i)
                    .FirstOrDefault();
                this.SelectedOptionIndex = index;
            }
        }
    }
}
