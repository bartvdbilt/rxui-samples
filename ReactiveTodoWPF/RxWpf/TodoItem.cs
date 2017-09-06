using ReactiveUI;
using System.Reactive;

namespace RxWpf
{
    public class TodoItem : ReactiveObject
    {
        private bool _done;

        public TodoItem(string text)
        {
            Text = text;
            DeleteCommand = ReactiveCommand.Create();
        }

        public string Text { get; private set; }

        public bool Done
        {
            get { return _done; }
            set { this.RaiseAndSetIfChanged(ref _done, value); }
        }

        public IReactiveCommand<object> DeleteCommand { get; }
    }
}
