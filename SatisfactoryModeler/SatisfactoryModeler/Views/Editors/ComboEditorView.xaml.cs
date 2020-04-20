using System.Reactive.Disposables;
using System.Windows;
using ReactiveUI;
using SatisfactoryModeler.ViewModels.Editors;

namespace SatisfactoryModeler.Views.Editors
{
    public partial class ComboEditorView : IViewFor<ComboEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(ComboEditorViewModel), typeof(ComboEditorView), new PropertyMetadata(null));

        public ComboEditorViewModel ViewModel
        {
            get => (ComboEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ComboEditorViewModel)value;
        }
        #endregion

        public ComboEditorView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.OptionLabels, v => v.valueComboBox.ItemsSource).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.SelectedOptionIndex, v => v.valueComboBox.SelectedIndex).DisposeWith(d);
            });
        }
    }
}
