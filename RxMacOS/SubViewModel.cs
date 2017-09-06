using System;
using ReactiveUI;
using System.Reactive.Disposables;

namespace ActivationTest
{
	public class SubViewModel : ReactiveObject, ISupportsActivation
	{
		public ViewModelActivator Activator { get; private set; }

		public SubViewModel()
		{
			Activator = new ViewModelActivator();

			this.WhenActivated(d =>
				{
					Console.WriteLine("Activating M1 View Model");
					d(Disposable.Create(() => Console.WriteLine("Deactivating M1 View Model")));
				});
		}
	}
}
