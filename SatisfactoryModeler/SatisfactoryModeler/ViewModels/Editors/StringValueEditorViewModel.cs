using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;
using SatisfactoryModeler.Views.Editors;
using System;

namespace SatisfactoryModeler.ViewModels.Editors
{
    public class StringValueEditorViewModel : ValueEditorViewModel<string>, IModifiableValueEditorViewModel
    {
        static StringValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new StringValueEditorView(), typeof(IViewFor<StringValueEditorViewModel>));
        }

        public StringValueEditorViewModel()
        {
            Value = "";
        }

        object IModifiableValueEditorViewModel.Value
        {
            get => Value;
            set => Value = Convert.ToString(value);
        }

    }
}
