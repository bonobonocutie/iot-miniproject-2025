using System.Windows;
using WpfIoTSimulatorApp.ViewModels;
using WpfIoTSimulatorApp.Views;

namespace WpfIoTSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var viewModel = new MainViewModel();
            var view = new MainView { 
                DataContext = viewModel,
            };

            viewModel.StartHmiRequested += view.StartHmiAni; // ViewModel 이벤트와 View 애니메이션 메서드 연결
            viewModel.StartSensorCheckRequested += view.StartSensorCheck;

            view.ShowDialog();
        }
    }

}
