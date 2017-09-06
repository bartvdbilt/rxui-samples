
namespace ActivationTest
{
	
	// Should subclass MonoMac.AppKit.NSView
	[Foundation.Register("SubViewTwo")]
	public partial class SubViewTwo
	{
	}
	
	// Should subclass MonoMac.AppKit.NSViewController
	[Foundation.Register("SubViewTwoController")]
	public partial class SubViewTwoController
	{
	}
}

