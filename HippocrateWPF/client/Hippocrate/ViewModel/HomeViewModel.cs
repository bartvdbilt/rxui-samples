using GalaSoft.MvvmLight;
using System.Windows.Controls;
using Hippocrate.ServiceUser;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;

namespace Hippocrate.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class HomeViewModel : ViewModelBase, IUserConnectedChangedEventHandler
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

        private string _dbutton1;
        public string DButton1
        {
            get { return _dbutton1; }
            set
            {
                _dbutton1 = value;
                RaisePropertyChanged("DButton1");
            }
        }
        
        private string _dbutton2;
        public string DButton2
        {
            get { return _dbutton2; }
            set
            {
                _dbutton2 = value;
                RaisePropertyChanged("DButton2");
            }
        }

        private ICommand _patientconsult;
        public ICommand PatientConsult
        {
            get { return _patientconsult; }
            set
            {
                _patientconsult = value;
                RaisePropertyChanged("PatientConsult");
            }
        }

        private ICommand _staffconsult;
        public ICommand StaffConsult
        {
            get { return _staffconsult; }
            set
            {
                _staffconsult = value;
                RaisePropertyChanged("StaffConsult");
            }
        }

        /// <summary>
        /// Initializes a new instance of the LoginViewModel class.
        /// </summary>
        public HomeViewModel()
        {
            WindowContent = new View.HomeView();
            WindowContent.DataContext = this;

            PatientConsult = new RelayCommand(() =>
            {
                ViewModelLocator vm = new ViewModelLocator();
                vm.Window.DataContext = vm.PatientList;
            });

            StaffConsult = new RelayCommand(() =>
            {
                ViewModelLocator vm = new ViewModelLocator();
                vm.Window.DataContext = vm.StaffListView;
            });
        }


        public void UserConnectedChangedEventHandler(object sender, User e)
        {
            if (e.Role != "Infirmière")
            {
                DButton2 = "Vous pouvez lire, créer et supprimer des fiches.";
                DButton1 = "Vous pouvez lire, créer et supprimer les fiches du personnel.";
            }
            else
            {
                DButton2 = "Vous pouvez lire des fiches.";
                DButton1 = "Vous pouvez lire les fiches du personnel hospitalier.";
            }
        }
    }
}