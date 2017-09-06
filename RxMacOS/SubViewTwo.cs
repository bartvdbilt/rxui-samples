
using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using ReactiveUI;

namespace ActivationTest
{
	public partial class SubViewTwo : ReactiveView, IActivatable
	{
		#region Constructors

		// Called when created from unmanaged code
		public SubViewTwo(IntPtr handle) : base(handle)
		{
			Initialize();
		}
		
		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public SubViewTwo(NSCoder coder) : base(coder)
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

