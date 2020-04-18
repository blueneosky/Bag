using System.Windows;
using SatisfactoryModeler.ViewModels.Editors;
using ReactiveUI;

namespace SatisfactoryModeler.Views.Editors
{
    public partial class DoubleEditorView : IViewFor<DoubleEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(DoubleEditorViewModel), typeof(DoubleEditorView), new PropertyMetadata(null));

        public DoubleEditorViewModel ViewModel
        {
            get => (DoubleEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (DoubleEditorViewModel)value;
        }
        #endregion

        public DoubleEditorView()
        {
            InitializeComponent();

            this.WhenActivated(d => d(
                this.Bind(ViewModel, vm => vm.Value, v => v.upDown.Value)
            ));
        }
    }
}
