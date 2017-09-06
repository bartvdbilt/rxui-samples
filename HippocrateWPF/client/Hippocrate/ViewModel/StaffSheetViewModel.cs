using GalaSoft.MvvmLight;
using System.Windows.Controls;
using Hippocrate.ServiceUser;
using System;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Hippocrate.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class StaffSheetViewModel : ViewModelBase, IUserConnectedChangedEventHandler, IUserSelected
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

        private bool _canadd;

        public bool CanAdd
        {
            get { return _canadd; }
            set
            {
                _canadd = value;
                RaisePropertyChanged("CanAdd");
            }
        }

        private string _staffid;

        public string StaffId
        {
            get { return _staffid; }
            set
            {
                _staffid = value;
                RaisePropertyChanged("StaffId");
            }
        }

        private string _staffname;

        public string StaffName
        {
            get { return _staffname; }
            set
            {
                _staffname = value;
                RaisePropertyChanged("StaffName");
            }
        }

        private string _stafffirstname;

        public string StaffFirstname
        {
            get { return _stafffirstname; }
            set
            {
                _stafffirstname = value;
                RaisePropertyChanged("StaffFirstname");
            }
        }

        private string _staffrole;

        public string StaffRole
        {
            get { return _staffrole; }
            set
            {
                _staffrole = value;
                RaisePropertyChanged("StaffRole");
            }
        }

        private BitmapImage _staffpicture;

        public BitmapImage StaffPicture
        {
            get { return _staffpicture; }
            set
            {
                _staffpicture = value;
                RaisePropertyChanged("StaffPicture");
            }
        }

        private ICommand _backcommand;

        public ICommand BackCommand
        {
            get { return _backcommand; }
            set
            {
                _backcommand = value;
            }
        }

        private ICommand _trashusercommand;

        public ICommand TrashUserCommand
        {
            get { return _trashusercommand; }
            set
            {
                _trashusercommand = value;
            }
        }

        private string SelectedLogin;
        /// <summary>
        /// Initializes a new instance of the StaffSheetViewModel class.
        /// </summary>
        public StaffSheetViewModel()
        {
             WindowContent = new View.StaffSheetView();
             WindowContent.DataContext = this;

            BackCommand = new RelayCommand(() =>
            {
                ViewModelLocator vm = new ViewModelLocator();
                vm.Window.DataContext = vm.StaffListView;
            });

            TrashUserCommand = new RelayCommand(() =>
            {
                ViewModelLocator vm = new ViewModelLocator();
                bool delete = BusinessManagement.User.DeleteUser(SelectedLogin);
                vm.StaffListView.UserListUpdate();
                vm.Window.DataContext = vm.StaffListView;
            });
        }


        public void UserConnectedChangedEventHandler(object sender, User e)
        {
            CanAdd = e.Role == "Infirmière" ? false : true;
        }

        public void UserSelectedEventHandler(User e)
        {
            SelectedLogin = e.Login;
            StaffId = e.Login;
            StaffFirstname = e.Firstname;
            StaffName = e.Name;
            StaffRole = e.Role;

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memory = new MemoryStream(e.Picture))
            {
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = null;
                bitmapImage.StreamSource = memory;
                bitmapImage.EndInit();
            }
            bitmapImage.Freeze();
            StaffPicture = bitmapImage;
        }
    }
}