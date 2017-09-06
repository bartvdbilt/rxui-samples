
using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace ActivationTest
{
	public partial class SecondWindowController : AppKit.NSWindowController
	{
		#region Constructors

		// Called when created from unmanaged code
		public SecondWindowController(IntPtr handle) : base(handle)
		{
			Initialize();
		}
		
		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public SecondWindowController(NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		// Call to load from the XIB/NIB file
		public SecondWindowController() : base("SecondWindow")
		{
			Initialize();
		}
		
		// Shared initialization code
		void Initialize()
		{
		}

		#endregion

		//strongly typed window accessor
		public new SecondWindow Window
		{
			get
			{
				return (SecondWindow) base.Window;
			}
		}
	}
}

