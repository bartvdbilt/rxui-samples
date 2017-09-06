using Hippocrate.DataAccess;
using Hippocrate.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

namespace Hippocrate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
        public App()
        { }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ViewModelLocator vm = new ViewModelLocator();

            vm.Login.UserChangedEventHandler += Login_UserChangedEventHandler;

            LoginViewModel loginViewModel = vm.Login;

            vm.Window.sidebar.DataContext = vm.Sidebar;
            vm.Window.DataContext = loginViewModel;
            vm.Window.Show();
        }

        private void Login_UserChangedEventHandler(object sender, ServiceUser.User e)
        {
            ViewModelLocator vm = new ViewModelLocator();

            vm.Account.UserConnectedChangedEventHandler(sender, e);
            vm.Home.UserConnectedChangedEventHandler(sender, e);
            vm.Sidebar.UserConnectedChangedEventHandler(sender, e);
            vm.StaffListView.UserConnectedChangedEventHandler(sender, e);
            vm.StaffSheet.UserConnectedChangedEventHandler(sender, e);
            vm.PatientList.UserConnectedChangedEventHandler(sender, e);
            vm.PatientSheet.UserConnectedChangedEventHandler(sender, e);
        }
    }
}
