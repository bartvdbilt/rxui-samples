/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Hippocrate"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Hippocrate.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            
            SimpleIoc.Default.Register<AddStaffViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<PatientSheetViewModel>();
            SimpleIoc.Default.Register<PatientListViewModel>();
            SimpleIoc.Default.Register<StaffListViewModel>();
            SimpleIoc.Default.Register<StaffSheetViewModel>();
            SimpleIoc.Default.Register<SidebarViewModel>();
            SimpleIoc.Default.Register<AccountViewModel>();
            SimpleIoc.Default.Register<PatientListViewModel>();
            SimpleIoc.Default.Register<AddPatientViewModel>();
            SimpleIoc.Default.Register<AddObservationViewModel>();

            SimpleIoc.Default.Register<View.MainWindow>();
            SimpleIoc.Default.Register<View.PatientSheetView>();
        }

        public AddStaffViewModel AddStaff
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddStaffViewModel>();
            }
        }

        public HomeViewModel Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomeViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public PatientSheetViewModel PatientSheet
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PatientSheetViewModel>();
            }
        }

        public StaffListViewModel StaffListView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StaffListViewModel>();
            }
        }

        public StaffSheetViewModel StaffSheet
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StaffSheetViewModel>();
            }
        }
        
        public SidebarViewModel Sidebar
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SidebarViewModel>();
            }
        }

        public AccountViewModel Account
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccountViewModel>();
            }
        }

        public PatientListViewModel PatientList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PatientListViewModel>();
            }
        }

        public AddPatientViewModel AddPatient
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddPatientViewModel>();
            }
        }

        public AddObservationViewModel AddObservation
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddObservationViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        public View.MainWindow Window
        {
            get
            {
                return ServiceLocator.Current.GetInstance<View.MainWindow>();
            }
        }

        public View.PatientSheetView PatientSheetView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<View.PatientSheetView>();
            }
        }
    }
}