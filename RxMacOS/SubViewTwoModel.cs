using ReactiveUI;
using System;
using System.Reactive.Disposables;

namespace ActivationTest
{
	public class SubViewTwoModel : ReactiveObject, ISupportsActivation
	{
		public ViewModelActivator Activator { get; private set; }

		public SubViewTwoModel()
		{
			Activator = new ViewModelActivator();

			this.WhenActivated(d =>
				{
					Console.WriteLine("Activating M2 View Model");
					d(Disposable.Create(() => Console.WriteLine("Deactivating M2 View Model")));
				});
		}
	}
}

