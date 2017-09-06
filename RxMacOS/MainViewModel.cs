using System;
using ReactiveUI;
using System.Reactive.Linq;
using ReactiveUI.Legacy;

namespace ActivationTest
{
	public class MainViewModel : ReactiveObject, ISupportsActivation
	{
		public ViewModelActivator Activator { get; private set; }

		public ReactiveCommand<bool> MyCommand { get; private set; }
		public ReactiveCommand<object> MyMenuCommand { get; private set; }
		public ReactiveCommand<object> ShowPanelCommand { get; private set; }

		private ReactiveObject _subViewModel;
		public ReactiveObject SubViewModel
		{
			get { return _subViewModel; }
			private set { this.RaiseAndSetIfChanged(ref _subViewModel, value); }
		}

		public DummyViewModel PanelViewModel { get; private set; }

		public MainViewModel()
		{
			Activator = new ViewModelActivator();


			var m1 = new SubViewModel();
			var m2 = new SubViewTwoModel();

			PanelViewModel = new DummyViewModel();

			this.WhenActivated(d =>
				{
					// length of time for the button to be disabled, simulating work
					var timeSpan = TimeSpan.FromSeconds(3);
					d(MyCommand = ReactiveCommand.CreateAsyncObservable(_ => Observable.Return(true).Delay(timeSpan)));

					// menu should only be enabled when 2nd subview is active
					var enableWhen = this
						.WhenAnyValue(x => x.SubViewModel)
						.Select(subViewModel => subViewModel == m2);
					d(MyMenuCommand = ReactiveCommand.Create(enableWhen));

					d(MyCommand.Subscribe(_ => SubViewModel = (SubViewModel == m2) ? (ReactiveObject) m1 : m2));

					d(ShowPanelCommand = ReactiveCommand.Create());

					MyCommand.Execute(null);
				});

			SubViewModel = m1;
		}
	}
}
