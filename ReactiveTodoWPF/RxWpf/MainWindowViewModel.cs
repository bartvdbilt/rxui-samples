using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro;

namespace RxWpf
{
    public enum Filtering
    {
        All,
        Done,
        NotDone
    }

    static class Ext
    {
        public static IObservable<TArgs> SelectArgs<TArgs>(this ReactiveCommand<object> cmd)
        {
            return cmd.OfType<EventPattern<TArgs>>().Select(e => e.EventArgs);
        }
    }
    public class MainWindowViewModel : ReactiveObject
    {
        public ReactiveList<TodoItem> Items { get; }
        public IReactiveDerivedList<TodoItem> FilteredItems { get; }

        public ReactiveCommand<object> CreateCommand { get; }
        public ReactiveCommand<TodoItem> LoadCommand { get; }
        public ReactiveCommand<object> ChangeThemeCommand { get; }
        public ReactiveCommand<object> ChangeAccentCommand { get; }

        #region Properties

        private string _newItemText = "";

        public string NewItemText
        {
            get { return _newItemText; }
            set { this.RaiseAndSetIfChanged(ref _newItemText, value); }
        }

        private int _itemsLeftCount;

        public int ItemsLeftCount
        {
            get { return _itemsLeftCount; }
            set { this.RaiseAndSetIfChanged(ref _itemsLeftCount, value); }
        }

        private Filtering _filter;

        public Filtering Filter
        {
            get { return _filter; }
            set
            {
                this.RaiseAndSetIfChanged(ref _filter, value);
                FilteredItems.Reset();
            }
        }

        #endregion

        private int _ThemeAccent = 0;
        private int _ThemeIndex = 0;

        public MainWindowViewModel()
        {
            Items = new ReactiveList<TodoItem> { ChangeTrackingEnabled = true };
            Items.ItemsAdded.Subscribe(i => i.DeleteCommand.Subscribe(_ => Items.Remove(i)));

            var itemDoneChanged = Items.ItemChanged.Where(x => x.PropertyName == "Done").Select(_ => Unit.Default);
            var countChanged = Items.CountChanged.Select(_ => Unit.Default);
            itemDoneChanged.Merge(countChanged).Subscribe(x1 => ItemsLeftCount = Items.Count(i => !i.Done));

            FilteredItems = Items.CreateDerivedCollection(x => x, (Func<TodoItem, bool>)FilterItem);

            var canCreate = this.ObservableForProperty(vm => vm.NewItemText).Select(s => !String.IsNullOrWhiteSpace(s.Value));
            CreateCommand = ReactiveCommand.Create(canCreate);
            CreateCommand.SelectArgs<KeyEventArgs>()
                .Where(x => x.Key == Key.Enter)
                .Subscribe(CreateItemExecute);
            //Items.Add(new TodoItem("ASDASD"));

            ChangeThemeCommand = ReactiveCommand.Create();
            ChangeThemeCommand.Subscribe(_ =>
            {


                ThemeManager.ChangeAppStyle(Application.Current,
                    ThemeManager.DetectAppStyle(Application.Current).Item2,
                    ThemeManager.AppThemes.ElementAt(_ThemeIndex++ % ThemeManager.AppThemes.Count()));
            });
            ChangeAccentCommand = ReactiveCommand.Create();
            ChangeAccentCommand.Subscribe(_ =>
            {


                ThemeManager.ChangeAppStyle(Application.Current,
                    ThemeManager.Accents.ElementAt(_ThemeAccent++ % ThemeManager.Accents.Count()),
                    ThemeManager.DetectAppStyle(Application.Current).Item1);
            });

            LoadCommand = ReactiveCommand.CreateAsyncObservable(_ => Observable.Create<TodoItem>(obs =>
            {
                if (!failedYet)
                {
                    obs.OnError(new Exception("test"));
                    failedYet = true;
                }
                else
                {
                    failedYet = false;
                    obs.OnNext(new TodoItem("a"));
                    obs.OnNext(new TodoItem("b"));
                    obs.OnNext(new TodoItem("c"));
                    obs.OnCompleted();
                }
                return Disposable.Empty;
            }));
            var observable = LoadCommand.ThrownExceptions.Select(ex => new UserError(ex.Message, recoveryOptions: new[] {RecoveryCommand.Yes, RecoveryCommand.Cancel }));
   
            //_LastError = observable.ToProperty(this, x => x.LastError);
            
            observable.SelectMany(UserError.Throw).Subscribe(result =>
                {
                    //this.RaisePropertyChanged("HasError");
                    switch (result)
                    {
                        case RecoveryOptionResult.CancelOperation:
                            return;
                        case RecoveryOptionResult.RetryOperation:
                            LoadCommand.Execute(null);
                            break;
                    }
                });
            LoadCommand.Subscribe(Items.Add);

        }

        private static bool failedYet = false;

        private bool FilterItem(TodoItem i)
        {
            if (Filter == Filtering.All)
                return true;
            return i.Done == (Filter == Filtering.Done);
        }

        private void CreateItemExecute(object args)
        {
            Items.Add(new TodoItem(NewItemText));
            NewItemText = "";
        }

        //public ObservableAsPropertyHelper<UserError> _LastError { get; }
        //public UserError LastError => _LastError.Value;
        //public bool HasError => LastError != null;

        //public IObservable<RecoveryOptionResult> ErrorHandler(UserError arg)
        //{
        //    var abort = new RecoveryCommand("Abort") { IsCancel = true };
        //    var retry = new RecoveryCommand("Retry") { IsDefault = true };
        //    arg.RecoveryOptions.Add(abort);
        //    arg.RecoveryOptions.Add(retry);
        //    this.RaisePropertyChanged("HasError");
        //    this.RaisePropertyChanged("LastError");
        //    return Observable.Merge(
        //        abort.Select(_ => RecoveryOptionResult.CancelOperation),
        //        retry.Select(_ => RecoveryOptionResult.RetryOperation));
        //}
    }
}
