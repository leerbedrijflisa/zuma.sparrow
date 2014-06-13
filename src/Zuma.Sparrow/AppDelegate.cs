using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SQLite;

namespace Zuma.Sparrow
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;

		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			CreateDatabase();

			// create a new window instance based on the screen size
			window = new UIWindow(UIScreen.MainScreen.Bounds);
			
			// If you have defined a root view controller, set it here:
			var navigationController = new NavigationController();
			var startupViewController = new MainMenuViewController();

			navigationController.PushViewController(startupViewController, true);
			window.RootViewController = navigationController;
			
			// make the window visible
			window.MakeKeyAndVisible();
			
			return true;
		}

		private void CreateDatabase()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Sparrow.db");

			using (var db = new SQLiteConnection(pathToDatabase))
			{
				db.CreateTable<ChoiceProfileData>();

				var profile = new ChoiceProfileData();

				profile.Name = "Ja/Nee";
				profile.FirstOptionImageUrl = "yes.jpg";
				profile.FirstOptionAudioUrl = "yes.mp3";
				profile.SecondOptionImageUrl = "no.jpg";
				profile.SecondOptionAudioUrl = "no.mp3";

				db.Insert(profile);
			}
		}
	}
}