using GalaSoft.MvvmLight;
using System.Windows.Controls;
using Hippocrate.ServiceUser;
using System;
using System.Threading.Tasks;

namespace Hippocrate.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AccountViewModel : ViewModelBase, IUserConnectedChangedEventHandler
    {
        private UserControl _windowContent;

        public UserControl WindowContent
        {
            get { return _windowContent; }
            set
            {
                _windowContent = value;
                RaisePropertyChanged("WindowContent");
            }
        }

        private string _firstname;

        public string Firstname
        {
            get { return _firstname;  }
            set
            {
                _firstname = value;
                RaisePropertyChanged("Firstname");
            }
        }

        private string _lastname;

        public string Lastname
        {
            get { return _lastname; }
            set
            {
                _lastname = value;
                RaisePropertyChanged("Lastname");
            }
        }

        private string _role;

        public string Role
        {
            get { return _role; }
            set
            {
                _role = value;
                RaisePropertyChanged("Role");
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string _login;

        public string Login
        {
            get { return _login; }
            set {
                _login = value;
                RaisePropertyChanged("Login");
            }
        }


        /// <summary>
        /// Initializes a new instance of the AddPatientViewModel class.
        /// </summary>
        public AccountViewModel()
        {
            WindowContent = new View.AccountView();
            WindowContent.DataContext = this;
        }
        public void UserConnectedChangedEventHandler(object sender, User e)
        { 
            this.Firstname = e.Firstname;
            this.Lastname = e.Name;
            this.Role = e.Role;
            this.Login = e.Login;
            this.Password = e.Pwd;
        }
    }
}