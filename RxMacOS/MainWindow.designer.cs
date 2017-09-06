// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ActivationTest
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		AppKit.NSView CustomView { get; set; }

		[Outlet]
		AppKit.NSButton PanelButton { get; set; }

		[Outlet]
		AppKit.NSButton ToggleButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CustomView != null) {
				CustomView.Dispose ();
				CustomView = null;
			}

			if (ToggleButton != null) {
				ToggleButton.Dispose ();
				ToggleButton = null;
			}

			if (PanelButton != null) {
				PanelButton.Dispose ();
				PanelButton = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
