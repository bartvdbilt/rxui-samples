using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MvvmValidation;
using System;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Hippocrate.ViewModel
{
    public class AddPatientViewModel : ValidatableViewModelBase
    {

        private ICommand _cancelcommand;

        public ICommand CancelCommand
        {
            get { return _cancelcommand; }
            set { _cancelcommand = value; }
        }

        private ICommand _validateaddcommand;

        public ICommand ValidateAddCommand
        {
            get { return _validateaddcommand; }
            set { _validateaddcommand = value; }
        }


        private string _addfirstname;

        public string AddFirstname
        {
            get { return _addfirstname; }
            set
            {
                _addfirstname = value;
                RaisePropertyChanged("AddFirstname");
                UpdateSubmitButton();

                Exception e = ValidateTarget(() => AddFirstname);
                if (e != null) { throw e; }
            }
        }

        private string _addname;

        public string AddName
        {
            get { return _addname; }
            set
            {
                _addname = value;
                RaisePropertyChanged("AddName");
                UpdateSubmitButton();

                Exception e = ValidateTarget(() => AddName);
                if (e != null)
                    throw e;
            }
        }

        private DateTime _addBirthday;

        public DateTime AddBirthday
        {
            get { return _addBirthday; }
            set
            {
                _addBirthday = value;
                RaisePropertyChanged("AddBirthday");
                UpdateSubmitButton();

                Exception e = ValidateTarget(() => AddBirthday);
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


        public AddPatientViewModel()
        {
            _addBirthday = DateTime.Now;
            RaisePropertyChanged("AddBirthday");

            Validator.AddRequiredRule(() => AddFirstname, "Le prénom ne peut pas être vide");
            Validator.AddRequiredRule(() => AddName, "Le prénom ne peut pas être vide");
            Validator.AddRequiredRule(() => AddBirthday, "La date de naissance ne peut pas être vide");

            CancelCommand = new RelayCommand(() =>
            {
                CancelPopup();
            });

            ValidateAddCommand = new RelayCommand(() =>
            {
                BusinessManagement.Patient.AddPatient(AddFirstname, AddName, AddBirthday);
                PatientListUpdate();
                CancelPopup();
            });

            CanSubmit = false;
        }

        public void CancelPopup()
        {
            ViewModelLocator vml = new ViewModelLocator();
            _addfirstname = "";
            RaisePropertyChanged("AddFirstname");
            _addname = "";
            RaisePropertyChanged("AddName");
            _addBirthday = DateTime.Now;
            RaisePropertyChanged("AddBirthday");
            CanSubmit = false;
            RaisePropertyChanged("CanSubmit");

            vml.PatientList.DissmissPopup();
        }

        public void PatientListUpdate()
        {
            ViewModelLocator vml = new ViewModelLocator();
            vml.PatientList.PatientListUpdate();
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
