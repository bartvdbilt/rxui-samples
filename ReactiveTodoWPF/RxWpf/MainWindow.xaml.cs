using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Windows;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ReactiveUI;

namespace RxWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, IViewFor<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            //UserError.RegisterHandler((Func<UserError, IObservable<RecoveryOptionResult>>)ViewModel.ErrorHandler);
            UserError.RegisterHandler((Func<UserError, IObservable<RecoveryOptionResult>>)ErrorHandler);
            DataContext = ViewModel;
            this.BindCommand(ViewModel, viewModel => viewModel.CreateCommand, w => w.textBox, "KeyDown");
        }

        private IObservable<RecoveryOptionResult> ErrorHandler(UserError arg)
        {
            if(arg.RecoveryOptions.Count != 2)
                throw new InvalidDataException("Expecting two recovery options, one affirmative, one negative");
            var aff = arg.RecoveryOptions.OfType<RecoveryCommand>().FirstOrDefault(x => x.IsDefault);
            var neg = arg.RecoveryOptions.OfType<RecoveryCommand>().FirstOrDefault(x => x.IsCancel);
            MessageDialogStyle style = MessageDialogStyle.AffirmativeAndNegative;
            MetroDialogSettings options = new MetroDialogSettings
            {
                AffirmativeButtonText = aff.CommandName,
                NegativeButtonText = neg.CommandName,
            };
            return this.ShowMessageAsync("", arg.ErrorMessage, style, options).ToObservable().Select(r =>
            {
                switch (r)
                {
                    case MessageDialogResult.Negative:
                        return RecoveryOptionResult.FailOperation;
                    case MessageDialogResult.Affirmative:
                        return RecoveryOptionResult.RetryOperation;
                    case MessageDialogResult.FirstAuxiliary:
                        return RecoveryOptionResult.CancelOperation;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(r), r, null);
                }
            });
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainWindowViewModel) value; }
        }

        public MainWindowViewModel ViewModel { get; set; }
    }
}
