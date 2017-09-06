using AppKit;
using Foundation;

namespace ActivationTest
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		MainWindowController mainWindowController;
		SecondWindowController secondWindowController;

		public static AppDelegate Self { get; private set; }

		public override void DidFinishLaunching(NSNotification notification)
		{
			Self = this;

			mainWindowController = new MainWindowController();
			mainWindowController.Window.MakeKeyAndOrderFront(this);
//
//			secondWindowController = new SecondWindowController();
//			secondWindowController.Window.MakeKeyAndOrderFront(this);
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
		{
			return true;
		}
	}
}

