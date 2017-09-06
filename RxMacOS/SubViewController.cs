using System;
using Foundation;
using ReactiveUI;
using System.Reactive.Disposables;

namespace ActivationTest
{
	public partial class SubViewController : ReactiveViewController, IViewFor<SubViewModel>
	{
		#region Constructors

		// Called when created from unmanaged code
		public SubViewController(IntPtr handle) : base(handle)
		{
			Initialize();
		}
		
		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public SubViewController(NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		// Call to load from the XIB/NIB file
		public SubViewController() : base("SubView", NSBundle.MainBundle)
		{
			Initialize();
		}
		
		// Shared initialization code
		void Initialize()
		{
		}

		#endregion

		//strongly typed view accessor
		public new SubView View
		{
			get
			{
				return (SubView) base.View;
			}
		}

		public SubViewModel ViewModel { get; set; }

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = (SubViewModel) value; }
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			View.WhenActivated(d =>
				{
					Console.WriteLine("Activating M1 Controller");
					d(Disposable.Create(() => Console.WriteLine("View M1 disposing")));
				}, this);
		}
	}
}

