﻿
using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace ActivationTest
{
	public partial class DummyPanel : AppKit.NSWindow
	{
		#region Constructors

		// Called when created from unmanaged code
		public DummyPanel(IntPtr handle) : base(handle)
		{
			Initialize();
		}
		
		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public DummyPanel(NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		// Shared initialization code
		void Initialize()
		{
		}

		#endregion
	}
}

