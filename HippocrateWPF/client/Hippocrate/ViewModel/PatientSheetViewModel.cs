using GalaSoft.MvvmLight;
using System.Windows.Controls;
using Hippocrate.ServiceUser;
using Hippocrate.ServicePatient;
using Hippocrate.ServiceObservation;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.IO;
using LiveCharts.Series;
using Hippocrate.DataAccess;
using Hippocrate.ServiceLive;
using System.Windows.Media;
using System.Threading.Tasks;

namespace Hippocrate.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PatientSheetViewModel : ViewModelBase, IUserConnectedChangedEventHandler,  IPatientSelected, IServiceLiveCallback
    {
        #region Getter / Setters
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


        private string _displayedname;

        public string DisplayedName
        {
            get { return _displayedname; }
            set
            {
                _displayedname = value;
                RaisePropertyChanged("DisplayedName");
            }
        }

        private string _displayedbirthday;

        public string DisplayedBirthday
        {
            get { return _displayedbirthday; }
            set
            {
                _displayedbirthday = value;
                RaisePropertyChanged("DisplayedBirthday");
            }
        }

        private ServicePatient.Observation _selectedobservation;

        public ServicePatient.Observation SelectedObservation
        {
            get { return _selectedobservation; }
            set
            {
                if (value != null)
                {
                    Selected = true;
                    ObservationDate = value.Date.ToString();
                    ObservationWeight = value.Weight.ToString();
                    ObservationBloodPressure = value.BloodPressure.ToString();
                    ObservationPrescription = new ObservableCollection<string>(value.Prescription);
                    ObservationComment = value.Comment;

                    ObservationPicture = new ObservableCollection<BitmapImage>();
                    foreach (var picture in value.Pictures)
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        using (MemoryStream memory = new MemoryStream(picture))
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
                        // Update user image
                        ObservationPicture.Add(bitmapImage);
                    }
                }

                _selectedobservation = value;
                RaisePropertyChanged("SelectedObservation");
            }
        }

        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChanged("Selected");
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

        private bool _canviewadd;

        public bool CanViewAdd
        {
            get { return _canviewadd; }
            set { _canviewadd = value; RaisePropertyChanged("CanViewAdd"); }
        }

        private UserControl _addobservationcontent;

        public UserControl AddObservationContent
        {
            get { return _addobservationcontent; }
            set
            {
                _addobservationcontent = value;
                RaisePropertyChanged("AddObservationContent");
            }
        }

        private string _observationdate;
        public string ObservationDate
        {
            get { return _observationdate; }
            set
            {
                _observationdate = value;
                RaisePropertyChanged("ObservationDate");
            }
        }
        
        private int _patientid;
        public int PatientId
        {
            get { return _patientid; }
            set
            {
                _patientid = value;
                RaisePropertyChanged("PatientId");
            }
        }

        private ObservableCollection<ServicePatient.Observation> _observations;

        public ObservableCollection<ServicePatient.Observation> Observations
        {
            get { return _observations; }
            set
            {
                _observations = value;
                RaisePropertyChanged("Observations");
            }
        }

        private ObservableCollection<BitmapImage> _observationpicture;

        public ObservableCollection<BitmapImage> ObservationPicture
        {
            get { return _observationpicture; }
            set
            {
                _observationpicture = value;
                RaisePropertyChanged("ObservationPicture");
            }
        }

        private ObservableCollection<Serie> _patientbloodpressureserie;

        public ObservableCollection<Serie> PatientBloodPressureSerie
        {
            get { return _patientbloodpressureserie; }
            set
            {
                _patientbloodpressureserie = value;
                RaisePropertyChanged("PatientBloodPressureSerie");
            }
        }

        private ObservableCollection<Serie> _patienttemperatureserie;

        public ObservableCollection<Serie> PatientTemperatureSerie
        {
            get { return _patienttemperatureserie; }
            set
            {
                _patienttemperatureserie = value;
                RaisePropertyChanged("PatientTemperatureSerie");
            }
        }


        private ICommand _backCommand;

        public ICommand BackCommand
        {
            get { return _backCommand; }
            set { _backCommand = value; }
        }

        private ICommand _addCommand;

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }

        private ICommand _trashpatientCommand;

        public ICommand TrashPatientCommand
        {
            get { return _trashpatientCommand; }
            set { _trashpatientCommand = value; }
        }

        private string _observationweight;
        public string ObservationWeight {
            get
            {
                return _observationweight;
            }
            set {
                _observationweight = value;
                RaisePropertyChanged("ObservationWeight");
            }
        }

        private string _observationbloodpressure;
        public string ObservationBloodPressure
        {
            get
            {
                return _observationbloodpressure;
            }
            set
            {
                _observationbloodpressure = value;
                RaisePropertyChanged("ObservationBloodPressure");
            }
        }

        private ObservableCollection<string> _observationprescription;
        public ObservableCollection<string> ObservationPrescription
        {
            get
            {
                return _observationprescription;
            }
            set
            {
                _observationprescription = value;
                RaisePropertyChanged("ObservationPrescription");
            }
        }

        private string _observationcomment;
        public string ObservationComment
        {
            get
            {
                return _observationcomment;
            }
            set
            {
                _observationcomment = value;
                RaisePropertyChanged("ObservationComment");
            }
        }

        private ServicePatient.Patient _patient;

        public ServicePatient.Patient Patient
        {
            get { return _patient; }
            set { _patient = value; }
        }

        private ServiceLiveManager s;

        #endregion
        /// <summary>
        /// Initializes a new instance of the PatientsListViewModel class.
        /// </summary>
        public PatientSheetViewModel()
        {
            ViewModelLocator vml = new ViewModelLocator();
             WindowContent = vml.PatientSheetView;
             WindowContent.DataContext = this;

            Selected = false;
            BackCommand = new RelayCommand(() =>
            {
                ViewModelLocator vm = new ViewModelLocator();
                Selected = false;
                SelectedObservation = null;
                vm.Window.DataContext = vm.PatientList;
            });

            AddCommand = new RelayCommand(() => {
                CanViewAdd = true;
            });

            TrashPatientCommand = new RelayCommand(() =>
            {
                ViewModelLocator vm = new ViewModelLocator();
                bool delete = BusinessManagement.Patient.DeletePatient(PatientId);
                vm.PatientList.PatientListUpdate();
                Selected = false;
                SelectedObservation = null;
                vm.Window.DataContext = vm.PatientList;
            });

            PatientBloodPressureSerie = new ObservableCollection<Serie>
            {
                new LineSerie
                {
                    Name = "BloodPressure",
                    PrimaryValues = new ObservableCollection<double> { 0}
                }
            };

            PatientTemperatureSerie = new ObservableCollection<Serie>
            {
                new LineSerie
                {
                    Name = "Temperature",
                    PrimaryValues = new ObservableCollection<double> { 38},
                    Color=Color.FromArgb(255,255,0,0)
                }
            };

            CanViewAdd = false;
            
            View.AddObservationView addObservationView = new View.AddObservationView();
            AddObservationContent = addObservationView;
            addObservationView.DataContext = vml.AddObservation;
            s = new ServiceLiveManager(this);
            
        }

        public void UserConnectedChangedEventHandler(object sender, User e)
        {
            CanAdd = e.Role.Equals("Infirmière") ? false : true;
        }

        public void DissmissPopup()
        {
            CanViewAdd = false;
        }

        public void PatientSelectedEventHandler(Patient e)
        {
            if (!s.IsSubscribed())
                s.Subscribe();
            DisplayedName = e.Firstname.ToUpper() + " " + e.Name;
            DisplayedBirthday = "Née le " + e.Birthday.ToString("dd/MM/yyyy");
            PatientId = e.Id;

            if (e.Observations != null)
                Observations = new ObservableCollection<ServicePatient.Observation>(new List<ServicePatient.Observation>(e.Observations));
            else
                Observations = new ObservableCollection<ServicePatient.Observation>(new List<ServicePatient.Observation>());
        }

        public void PatientUpdate()
        {
            Patient p = BusinessManagement.Patient.GetPatient(PatientId);
            Observations = new ObservableCollection<ServicePatient.Observation>(new List<ServicePatient.Observation>(p.Observations));
        }

        public void PushDataHeart(double requestData)
        {
            if (PatientBloodPressureSerie[0].PrimaryValues.Count > 50)
                PatientBloodPressureSerie[0].PrimaryValues.RemoveAt(0);

                PatientBloodPressureSerie[0].PrimaryValues.Add(requestData);
        }

        public void PushDataTemp(double requestData)
        {
           if (PatientTemperatureSerie[0].PrimaryValues.Count > 50)
                PatientTemperatureSerie[0].PrimaryValues.RemoveAt(0);

            PatientTemperatureSerie[0].PrimaryValues.Add(requestData);
        }
    }
}