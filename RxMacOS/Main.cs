using System;
using CoreGraphics;
using Foundation;
using AppKit;
using ObjCRuntime;
using Splat;
using ReactiveUI;

namespace ActivationTest
{
	class MainClass
	{
		static void Main(string[] args)
		{
			Locator.CurrentMutable.Register(() => new SubViewController(), typeof(IViewFor<SubViewModel>));
			Locator.CurrentMutable.Register(() => new SubViewTwoController(), typeof(IViewFor<SubViewTwoModel>));

			NSApplication.Init();
			NSApplication.Main(args);
		}
	}
}

