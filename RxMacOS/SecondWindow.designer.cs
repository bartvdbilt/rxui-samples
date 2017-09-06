
namespace ActivationTest
{
	
	// Should subclass MonoMac.AppKit.NSWindow
	[Foundation.Register("SecondWindow")]
	public partial class SecondWindow
	{
	}
	
	// Should subclass MonoMac.AppKit.NSWindowController
	[Foundation.Register("SecondWindowController")]
	public partial class SecondWindowController
	{
	}
}

