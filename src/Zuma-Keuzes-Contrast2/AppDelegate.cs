using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ZumaKeuzesContrast2
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			var rootNavigationController = new UINavigationController ();

			MainMenu mainMenu = new MainMenu ();

			rootNavigationController.PushViewController (mainMenu, false);

			this.window.RootViewController = rootNavigationController;
			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

