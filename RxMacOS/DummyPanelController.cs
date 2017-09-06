
using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using ReactiveUI;

namespace ActivationTest
{
	public partial class DummyPanelController : ReactiveWindowController, IViewFor<DummyViewModel>
	{
		#region Constructors

		// Called when created from unmanaged code
		public DummyPanelController(IntPtr handle) : base(handle)
		{
			Initialize();
		}
		
		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public DummyPanelController(NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		// Call to load from the XIB/NIB file
		public DummyPanelController() : base("DummyPanel")
		{
			Initialize();
		}
		
		// Shared initialization code
		void Initialize()
		{
		}

		#endregion

		//strongly typed window accessor
		public new DummyPanel Window
		{
			get
			{
				return (DummyPanel) base.Window;
			}
		}

		public DummyViewModel ViewModel { get; set; }

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = (DummyViewModel) value; }
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			this.WhenActivated(SetUpBindings);
		}

		private void SetUpBindings(Action<IDisposable> d)
		{
			d(this.BindCommand(ViewModel, vm => vm.CloseCommand, controller => controller.CloseButton));

			d(ViewModel.CloseCommand.Subscribe(_ =>
				{
					try {
						var parentWindow = Window.SheetParent;
						Window.OrderOut(this);
						Close();
						parentWindow.EndSheet(Window);
					} catch (Exception ex) {
						var x = ex.Message;	
					}
				}));

//			d(ViewModel.CloseCommand.Subscribe(_ =>
//				{
//					try {
//						Window.OrderOut(this);
//						Close();
//						NSApplication.SharedApplication.EndSheet(Window);
//					} catch (Exception ex) {
//						var x = ex.Message;	
//					}
//				}));
		}
	}
}
