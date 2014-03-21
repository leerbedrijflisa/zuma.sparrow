using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Mono.Data.Sqlite;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace ZumaKeuzesContrast2
{
	public partial class ProfileMenu : UISplitViewController
	{
		UIViewController masterProfileMenuView, detailProfileMenuView;

		public ProfileMenu () : base ()
		{
			masterProfileMenuView = new MasterViewController ();
			detailProfileMenuView = new DetailViewController ();

			ViewControllers = new UIViewController[] 
			{ masterProfileMenuView, detailProfileMenuView };
		}
			
		string name;
		object returnFirst;
		List<string> ProfileNames = new List<string> ();
		string[] items;
		TableSource itemstable;
		MainMenu mainMenu;

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ReadMenuSettings ();
		
			items = ProfileNames.ToArray ();

			itemstable = new TableSource (items);

			//lisProfiles.Source = itemstable;
//			vwDetial.Add (detailProfileMenuView);

			btnSaveProfile.Hidden = true;

			btnSaveProfile.TouchUpInside += (sender, e) => {
				if(mainMenu == null)
				{
					mainMenu = new MainMenu();
				}

				NavigationController.PushViewController(mainMenu, false);
			};

		}

		public void ReadMenuSettings()
		{

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatebase = Path.Combine (documents, "db_Zuma_Keuzes.db");
			//SqliteConnection.CreateFile (pathToDatebase);

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatebase);
			using (var conn = new SqliteConnection (connectionString)) {

				conn.Open ();
				string stm = "SELECT * FROM Profile";

				using (SqliteCommand cmd = new SqliteCommand (stm, conn)) {
					using (SqliteDataReader rdr = cmd.ExecuteReader ()) {
						while (rdr.Read ()) {
							returnFirst = rdr ["Name"];
							name = returnFirst.ToString ();
							ProfileNames.Add (name);
						}
					}
				}
			}
		}

	}
}
