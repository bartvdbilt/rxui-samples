
using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using ReactiveUI;
using System.Reactive.Disposables;

namespace ActivationTest
{
	public partial class SubViewTwoController : ReactiveViewController, IViewFor<SubViewTwoModel>
	{
		#region Constructors

		// Called when created from unmanaged code
		public SubViewTwoController(IntPtr handle) : base(handle)
		{
			Initialize();
		}
		
		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public SubViewTwoController(NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		// Call to load from the XIB/NIB file
		public SubViewTwoController() : base("SubViewTwo", NSBundle.MainBundle)
		{
			Initialize();
		}
		
		// Shared initialization code
		void Initialize()
		{
		}

		#endregion

		//strongly typed view accessor
		public new SubViewTwo View
		{
			get
			{
				return (SubViewTwo) base.View;
			}
		}

		public SubViewTwoModel ViewModel { get; set; }

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = (SubViewTwoModel) value; }
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			View.WhenActivated(d =>
				{
					Console.WriteLine("Activating M2 Controller");
					d(Disposable.Create(() => Console.WriteLine("View M2 disposing")));
				}, this);
		}
	}
}

