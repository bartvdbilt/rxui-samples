using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.Legacy;

namespace ActivationTest
{
	public class DummyViewModel : ReactiveObject, ISupportsActivation
	{
		public ViewModelActivator Activator { get; private set; }

		public ReactiveCommand<object> CloseCommand { get; private set; }

		public DummyViewModel()
		{
			Activator = new ViewModelActivator();

			this.WhenActivated(SetUpBindings);
		}

		private void SetUpBindings(Action<IDisposable> d)
		{
			d(CloseCommand = ReactiveUI.Legacy.ReactiveCommand.Create());

			d(Disposable.Create(() => Debug.WriteLine("Panel disposed!")));
		}
	}
}
