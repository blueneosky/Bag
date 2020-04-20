using System.Collections.Generic;
using System.Diagnostics;
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

        public ComboEditorViewModel(IEnumerable<object> options)
            : this(options, options.Select(o => o.ToString()))
        {
        }

        public ComboEditorViewModel(IEnumerable<object> options, IEnumerable<string> labels)
        {
            this.Options = options.ToArray();
            this.OptionLabels = labels.ToArray();
            Debug.Assert(this.Options.Length == this.OptionLabels.Length);

            this.WhenAnyValue(vm => vm.SelectedOptionIndex)
               .Select(i => i == -1 ? null : Options[i])
               .BindTo(this, vm => vm.Value);
        }

        //object IModifiableValueEditorViewModel.Value
        //{
        //    get => Value;
        //    set
        //    {
        //        if (value == null) return;
        //        var val = Enum.Parse(this.enumType, Convert.ToString(value));
        //        var index = Options.Select((v, i) => (v, i))
        //            .Where(t => val.Equals(t.v))
        //            .Select(t => t.i)
        //            .FirstOrDefault();
        //        this.SelectedOptionIndex = index;
        //    }
        //}
    }
}
