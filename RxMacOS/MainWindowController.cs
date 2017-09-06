using System;
using System.Reactive.Disposables;
using AppKit;
using Foundation;
using ReactiveUI;

namespace ActivationTest
{
	public partial class MainWindowController : ReactiveWindowController, IViewFor<MainViewModel>
	{
		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController(IntPtr handle) : base(handle)
		{
			Initialize();
		}
		
		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public MainWindowController(NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		// Call to load from the XIB/NIB file
		public MainWindowController() : base("MainWindow")
		{
			Initialize();
		}
		
		// Shared initialization code
		void Initialize()
		{
		}

		#endregion

		//strongly typed window accessor
		public new MainWindow Window
		{
			get
			{
				return (MainWindow) base.Window;
			}
		}
			
		public MainViewModel ViewModel { get; set; }

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = (MainViewModel) value; }
		}


		public NSMenuItem MyMenuItem { get { return AppDelegate.Self.MyMenuItem; } }

		private ViewModelViewHost _subViewHost;

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			ViewModel = new MainViewModel();


			this.WhenActivated(d =>
				{
					d(this.BindCommand(ViewModel, vm => vm.MyMenuCommand, c => c.MyMenuItem));

					d(this.BindCommand(ViewModel, vm => vm.MyCommand, c => c.ToggleButton));
					d(this.WhenAnyValue(c => c.ViewModel.SubViewModel).Subscribe(vm => _subViewHost.ViewModel = vm));

					d(this.BindCommand(ViewModel, vm => vm.ShowPanelCommand, controller => controller.PanelButton));

					d(ViewModel.ShowPanelCommand.Subscribe(_ =>
						{
							var controller = new DummyPanelController { ViewModel = ViewModel.PanelViewModel };
							Window.BeginSheet(controller.Window, __ => 
								{
									controller.Dispose();
									controller = null;
								});
						}));

//					d(ViewModel.ShowPanelCommand.Subscribe(_ =>
//						{
//							var controller = new DummyPanelController { ViewModel = ViewModel.PanelViewModel };
//							NSApplication.SharedApplication.BeginSheet(controller.Window, Window, () => 
//								{
//									controller.Dispose();
//									controller = null;
//								});
//						}));

					d(Disposable.Create(() => Console.WriteLine("MainWindowController disposed.")));
				});

			_subViewHost = new ViewModelViewHost(CustomView);
			_subViewHost.ViewModel = ViewModel.SubViewModel;
		}
	}
}
