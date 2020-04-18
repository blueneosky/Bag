using System.Linq;
using System.Reactive.Linq;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;
using SatisfactoryModeler.Views.Editors;

namespace SatisfactoryModeler.ViewModels.Editors
{
    public class ComboEditorViewModel : ValueEditorViewModel<object>, IModifiableValueEditorViewModel
    {
        static ComboEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new ComboEditorView(), typeof(IViewFor<ComboEditorViewModel>));
        }

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

        public ComboEditorViewModel(object[] options)
            : this(options, options.Select(o => o.ToString()).ToArray())
        {
        }

        public ComboEditorViewModel(object[] options, string[] labels)
        {
            this.Options = options;
            this.OptionLabels = labels;

            this.WhenAnyValue(vm => vm.SelectedOptionIndex)
               .Select(i => i == -1 ? null : Options[i])
               .BindTo(this, vm => vm.Value);
        }
    }
}
