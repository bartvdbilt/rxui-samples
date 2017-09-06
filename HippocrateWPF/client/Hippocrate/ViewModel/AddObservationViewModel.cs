using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MvvmValidation;
using System;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Collections.Generic;
using Hippocrate.Helper;

namespace Hippocrate.ViewModel
{
    public class AddObservationViewModel : ValidatableViewModelBase
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

        private ICommand _addprescriptioncommand;

        public ICommand AddPrescriptionCommand
        {
            get { return _addprescriptioncommand; }
            set { _addprescriptioncommand = value; }
        }

        private ICommand _reinitcommand;

        public ICommand ReinitCommand
        {
            get { return _reinitcommand; }
            set { _reinitcommand = value; }
        }

        private ICommand _addobservationpicture;

        public ICommand AddObservationPicture
        {
            get { return _addobservationpicture; }
            set { _addobservationpicture = value; }
        }

        private ICommand _deletepictures;

        public ICommand DeletePictures
        {
            get { return _deletepictures; }
            set { _deletepictures = value; }
        }


        private string _bloodpressure;

        public string BloodPressure
        {
            get { return _bloodpressure; }
            set
            {
                _bloodpressure = value;
                RaisePropertyChanged("BloodPressure");
                UpdateSubmitButton();

                Exception e = ValidateTarget(() => BloodPressure);
                if (e != null)
                    throw e;
            }
        }

        private string _poids;

        public string Poids
        {
            get { return _poids; }
            set {
                _poids = value;
                RaisePropertyChanged("Poids");
                UpdateSubmitButton();

                Exception e = ValidateTarget(() => Poids);
                if (e != null)
                    throw e;
            }
        }

        private string _comment;

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; RaisePropertyChanged("Comment"); }
        }

        private string _addprescription;

        public string AddPrescription
        {
            get { return _addprescription; }
            set { _addprescription = value;
                CanAddPrescription = !(value == "" || value == null);
                RaisePropertyChanged("AddPrescription");
            }
        }


        private bool _canaddprescription;

        public bool CanAddPrescription
        {
            get { return _canaddprescription; }
            set { _canaddprescription = value; RaisePropertyChanged("CanAddPrescription"); }
        }

        private ObservableCollection<string> _prescriptions;

        public ObservableCollection<string> Prescriptions
        {
            get { return _prescriptions; }
            set { _prescriptions = value; RaisePropertyChanged("Prescriptions"); UpdateSubmitButton(); }
        }

        private ObservableCollection<BitmapImage> _pictures;

        public ObservableCollection<BitmapImage> Pictures
        {
            get { return _pictures; }
            set { _pictures = value; RaisePropertyChanged("Pictures"); }
        }


        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged("Date");
                UpdateSubmitButton();

                Exception e = ValidateTarget(() => Date);
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


        public AddObservationViewModel()
        {
            int parse = 0;
            Prescriptions = new ObservableCollection<string>();
            Pictures = new ObservableCollection<BitmapImage>();
            _date = DateTime.Now;
            RaisePropertyChanged("Date");
            CanAddPrescription = false;

            Validator.AddRequiredRule(() => Date, "La date ne peut pas être vide");
            Validator.AddRequiredRule(() => BloodPressure, "La pression sanguine ne peut pas être vide");
            Validator.AddRule(() => BloodPressure,
                              () => RuleResult.Assert(int.TryParse(BloodPressure, out parse), "La pression sanguine doit être un nombre"));
            Validator.AddRequiredRule(() => Poids, "Le poids ne peut pas être vide");
            Validator.AddRule(() => Poids,
                  () => RuleResult.Assert(int.TryParse(Poids, out parse), "Le poids doit être un nombre"));
            Validator.AddRule(() => Prescriptions,
                              () => RuleResult.Assert(Prescriptions.Count > 0, "Il doit y avoir au moins une prescription"));

            CancelCommand = new RelayCommand(() =>
            {
                CancelPopup();
            });

            ValidateAddCommand = new RelayCommand(() =>
            {
                try
                {
                    ViewModelLocator vml = new ViewModelLocator();
                    ServiceObservation.Observation o = new ServiceObservation.Observation();
                    o.BloodPressure = int.Parse(BloodPressure);
                    o.Weight = int.Parse(Poids);
                    o.Comment = Comment;
                    o.Date = Date;
                    o.Prescription = new List<string>(Prescriptions).ToArray();

                    List<byte[]> images = new List<byte[]>();
                    foreach (BitmapImage arr in Pictures)
                        images.Add(Tools.ConvertImage(arr));
                    o.Pictures = images.ToArray();

                    BusinessManagement.Observation.AddObservation(vml.PatientSheet.PatientId, o);
                    vml.PatientSheet.PatientUpdate();
                    vml.PatientList.PatientListUpdate();
                    CancelPopup();
                }
                catch (Exception){}
            });

            AddPrescriptionCommand = new RelayCommand(() =>
            {
                if (CanAddPrescription)
                {
                    Prescriptions.Add(AddPrescription);
                    AddPrescription = "";
                    UpdateSubmitButton();
                }
            });

            ReinitCommand = new RelayCommand(() =>
            {
                Prescriptions = new ObservableCollection<string>();
            });

            AddObservationPicture = new RelayCommand(() =>
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|BMP Files: (*.BMP; *.DIB; *.RLE) | *.BMP; *.DIB; *.RLE |" + "JPEG Files: (*.JPG; *.JPEG; *.JPE; *.JFIF)| *.JPG; *.JPEG; *.JPE; *.JFIF |GIF Files: (*.GIF) | *.GIF | " + "TIFF Files: (*.TIF; *.TIFF)| *.TIF; *.TIFF |" + "PNG Files: (*.PNG) | *.PNG |" + "All Files | *.* ";

                ViewModelLocator vml = new ViewModelLocator();
                vml.PatientSheet.DissmissPopup();
                if (openDialog.ShowDialog().Value)
                {
                    vml.PatientSheet.CanViewAdd = true;
                    Pictures.Add(new BitmapImage(new Uri(openDialog.FileName)));
                }

                vml.PatientSheet.CanViewAdd = true;
            });

            DeletePictures = new RelayCommand(() =>
            {
                Pictures = new ObservableCollection<BitmapImage>();
            });

            CanSubmit = false;
        }

        public void CancelPopup()
        {
            Prescriptions = new ObservableCollection<string>();
            Pictures = new ObservableCollection<BitmapImage>();
            ViewModelLocator vml = new ViewModelLocator();
            vml.PatientSheet.DissmissPopup();
            _poids = "";
            RaisePropertyChanged("Poids");
            _date = DateTime.Now;
            RaisePropertyChanged("Date");
            _bloodpressure = "";
            RaisePropertyChanged("BloodPressure");
            _comment = "";
            RaisePropertyChanged("Comment");
            AddPrescription = "";
            CanSubmit = false;
            RaisePropertyChanged("CanSubmit");
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
