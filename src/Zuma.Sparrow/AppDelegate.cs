﻿using System;
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

				var profileYesNo = new ChoiceProfileData();

				profileYesNo.Name = "Ja/Nee";
				profileYesNo.FirstOptionImageUrl = "yes.jpg";
				profileYesNo.FirstOptionAudioUrl = "yes.mp3";
				profileYesNo.SecondOptionImageUrl = "no.jpg";
				profileYesNo.SecondOptionAudioUrl = "no.mp3";
				profileYesNo.ProfileType = 1;

				var choiceProfileDataYesNo = db.Table<ChoiceProfileData>().Where(profile => profile.Name == profileYesNo.Name).FirstOrDefault();
				if (choiceProfileDataYesNo == null)
				{
					db.Insert(profileYesNo);
				}

				var profileLeftRight = new ChoiceProfileData();

				profileLeftRight.Name = "Links/Rechts";
				profileLeftRight.FirstOptionImageUrl = "Left.png";
				profileLeftRight.FirstOptionAudioUrl = "Left.mp3";
				profileLeftRight.SecondOptionImageUrl = "Right.png";
				profileLeftRight.SecondOptionAudioUrl = "Right.mp3";
				profileLeftRight.ProfileType = 1;

				var choiceProfileDataLeftRight = db.Table<ChoiceProfileData>().Where(profile => profile.Name == profileLeftRight.Name).FirstOrDefault();
				if (choiceProfileDataLeftRight == null)
				{
					db.Insert(profileLeftRight);
				}
			}
		}
	}
}