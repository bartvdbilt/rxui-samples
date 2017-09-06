
using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using ReactiveUI;

namespace ActivationTest
{
	public partial class SubView : ReactiveView, IActivatable
	{
		#region Constructors

		// Called when created from unmanaged code
		public SubView(IntPtr handle) : base(handle)
		{
			Initialize();
		}
		
		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public SubView(NSCoder coder) : base(coder)
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

