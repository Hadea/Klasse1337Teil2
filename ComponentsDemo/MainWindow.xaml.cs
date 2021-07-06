using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComponentsDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void btnStackpanel_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new StackpanelDemoPage());
        private void btnGrid_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new GridDemoPage());
        private void btnButton_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new ButtonDemoPage());
        private void btnTextBox_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new TextBoxDemoPage());
        private void btnTextBlock_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new TextBlockDemoPage());
        private void btnImage_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new ImageDemoPage());
        private void btnSlider_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new SliderDemoPage());
        private void btnScrollBar_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new ScrollBarDemoPage());
        private void btnColor_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new ColorDemoPage());
        private void btnRadioCheck_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new RadioCheckDemoPage());
        private void btnComboBox_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new ComboBoxDemoPage());
        private void btnListView_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new ListViewDemoPage());
        private void btnAudio_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new AudioDemoPage());
        private void btnBindings_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new BindingsDemoPage());
        private void btnBindingDirection_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new BindingDirectionDemoPage());
        private void btnFormatting_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new FormattingDemoPage());
        private void btnFormattingExercise_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new FormattingExercisePage());
        private void btnMenuAndCommand_Click(object sender, RoutedEventArgs e)
        {
            //frmContent.Navigate(new MenuAndCommandDemoPage());
            var window = new MenuAndCommandsDemoWindow();
            window.Show();
        }

        private void btnDockPanel_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new DockPanelDemoPage());
        private void btnValidation_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new ValidationDemoPage());
        private void btnGrouping_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new GroupingDemoPage());
        private void btnProgress_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new ProgressDemoPage());

        private void btnViewport3D_Click(object sender, RoutedEventArgs e) => frmContent.Navigate(new Viewport3DDemoPage());
    }
}
