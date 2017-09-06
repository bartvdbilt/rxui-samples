using Hippocrate.ServiceUser;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using MvvmValidation;
using System.IO;
using System.Windows.Media.Imaging;
using System.Linq.Expressions;
using Hippocrate.Helper;

namespace Hippocrate.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AddStaffViewModel : ValidatableViewModelBase
    {
        #region get/set
        private ICommand _cancelcommand;

        public ICommand CancelCommand
        {
            get { return _cancelcommand; }
            set { _cancelcommand = value; }
        }

        private ICommand _createcommand;

        public ICommand CreateCommand
        {
            get { return _createcommand; }
            set { _createcommand = value; }
        }

        private ICommand _choosepicturecommand;

        public ICommand ChoosePictureCommand
        {
            get { return _choosepicturecommand; }
            set { _choosepicturecommand = value; }
        }

        private string _firstname;

        public string Firstname
        {
            get { return _firstname; }
            set
            {
                _firstname = value;
                RaisePropertyChanged("Firstname");
                UpdateSubmitButton();

                Exception e = ValidateTarget(() => Firstname);
                if (e != null)
                    throw e;
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
                UpdateSubmitButton();

                Exception e = ValidateTarget(() => Name);
                if (e != null)
                    throw e;
            }
        }

        private string _login;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged("Login");
                UpdateSubmitButton();

                Exception e = ValidateTarget(() => Login);
                if (e != null)
                    throw e;
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
                UpdateSubmitButton();

                Exception e = ValidateTarget(() => Role);
                if (e != null)
                    throw e;
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
                UpdateSubmitButton();

                Exception e = ValidateTarget(() => Password);
                if (e != null)
                    throw e;
            }
        }

        private bool _cansubmit;

        public bool CanSubmit
        {
            get { return _cansubmit; }
            set { _cansubmit = value; RaisePropertyChanged("CanSubmit"); }
        }


        private ObservableCollection<string> _listrole;

        public ObservableCollection<string> ListRole
        {
            get { return _listrole; }
            set { _listrole = value; RaisePropertyChanged("ListRole"); }
        }

        private BitmapImage _image;

        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; RaisePropertyChanged("Image"); }
        }

        #endregion
        /// <summary>
        /// Initializes a new instance of the AddStaffViewModel class.
        /// </summary>
        public AddStaffViewModel()
        {
            CanSubmit = false;
            Validator.AddRequiredRule(() => Firstname, "Le prénom ne peut pas être vide");
            Validator.AddRequiredRule(() => Name, "Le prénom ne peut pas être vide");

            Validator.AddRequiredRule(() => Login, "Le login ne peut pas être vide");
            Validator.AddRule(() => Login,
                              () => RuleResult.Assert(Login != null && Login != "" && Login.Split(' ').Length == 1, "Le login doit être composé d'un seul mot"));


            Validator.AddRequiredRule(() => Role, "Le role ne peut pas être vide");

            Validator.AddRequiredRule(() => Password, "Le mot de passe ne peut pas être vide");
            Validator.AddRule(() => Password,
                              () => RuleResult.Assert(Password.Length >= 4, "Le mot de passe doit contenir au moins 4 caractères"));

            CancelCommand = new RelayCommand(() =>
            {
                CancelPopup();
            });

            ChoosePictureCommand = new RelayCommand(() =>
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|BMP Files: (*.BMP; *.DIB; *.RLE) | *.BMP; *.DIB; *.RLE |" + "JPEG Files: (*.JPG; *.JPEG; *.JPE; *.JFIF)| *.JPG; *.JPEG; *.JPE; *.JFIF |GIF Files: (*.GIF) | *.GIF | " + "TIFF Files: (*.TIF; *.TIFF)| *.TIF; *.TIFF |" + "PNG Files: (*.PNG) | *.PNG |" + "All Files | *.* ";

                ViewModelLocator vml = new ViewModelLocator();
                vml.StaffListView.DissmissPopup();
                if (openDialog.ShowDialog().Value)
                {
                    vml.StaffListView.CanViewAdd = true;
                    Image = new BitmapImage(new Uri(openDialog.FileName));
                }
                vml.StaffListView.CanViewAdd = true;
            });

            CreateCommand = new RelayCommand(() =>
            {
                User user = new User();
                user.Firstname = Firstname;
                user.Name = Name;
                user.Login = Login;
                user.Role = Role;
                user.Picture = Tools.ConvertImage(Image);
                user.Pwd = Password;
                try
                {
                    BusinessManagement.User.AddUser(user);
                    ViewModelLocator vml = new ViewModelLocator();
                    vml.StaffListView.UserListUpdate();
                    CancelPopup();
                }
                catch (Exception)
                {
                    // ErrorMessage = "La taille de l'image est trop grande";
                }
            });

            Image = new BitmapImage(new Uri("/Assets/anonym.jpg", UriKind.Relative));
            ListRole = new ObservableCollection<string>() { "Infirmière", "Medecin", "Chirurgien", "Radiologue" };
        }

        public void CancelPopup()
        {
            _firstname = "";
            RaisePropertyChanged("Firstname");
            _name = "";
            RaisePropertyChanged("Name");
            _login = "";
            RaisePropertyChanged("Login");
            _role = "";
            RaisePropertyChanged("Role");
            _password = "";
            RaisePropertyChanged("Password");
            CanSubmit = false;
            Image = new BitmapImage(new Uri("/Assets/anonym.jpg", UriKind.Relative));
            ViewModelLocator vml = new ViewModelLocator();
            vml.StaffListView.DissmissPopup();
        }

        private void UpdateSubmitButton()
        {
            ValidationResult v = Validator.ValidateAll();
            CanSubmit = v.IsValid;
        }

        private Exception ValidateTarget(Expression<Func<object>> expression)
        {
            ValidationResult result = Validator.Validate(expression);
            if (!result.IsValid)
            {
                return new Exception(string.Join("\n", result.ErrorList));
            }
            return null;
        }
    }
}