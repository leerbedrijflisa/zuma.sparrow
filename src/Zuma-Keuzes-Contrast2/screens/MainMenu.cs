using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Mono.Data.Sqlite;
using System.IO;
using System.Text;
using System.Data;

namespace ZumaKeuzesContrast2
{
	public partial class MainMenu : UIViewController
	{

		public MainMenu () : base ("MainMenu", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.LandscapeLeft | UIInterfaceOrientationMask.LandscapeRight;
		}
			

		MainViewController4 viewController;
		ProfileMenu profileMenu;

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//Create's a db if there isn't one already with a table to handle the Menu segement button options
			DatabaseRequests.CreateZumaSparrowDB ();
			DatabaseRequests.StaticProfiles ();
			returnSelectedRow ();

			btnAdd.SetImage (UIImage.FromFile ("images/AddBTN.png"), UIControlState.Normal);
			btnSubtract.SetImage (UIImage.FromFile ("images/SubtractBTN.png"), UIControlState.Normal);

			int Timer = 5;

			btnChoiceProfile.TouchUpInside += (sender, e) => {
				if (profileMenu == null) {
					profileMenu = new ProfileMenu ();
				}

				NavigationController.PushViewController(profileMenu, false);
			};						

			LblTimer.Text = Timer.ToString();

			btnAdd.TouchUpInside += (sender, e) => {
				Timer ++;
				LblTimer.Text = Timer.ToString();
			};

			btnSubtract.TouchUpInside += (sender, e) => {
				if(Timer <= 1) 
				{ 
					btnSubtract.SetImage (UIImage.FromFile ("images/AddSubtractBTN.png"), UIControlState.Disabled);
				} 
				else 
				{ 
					btnSubtract.SetImage (UIImage.FromFile ("images/AddSubtractBTN.png"), UIControlState.Disabled);
					Timer --;
					LblTimer.Text = Timer.ToString();
				}

			};

			btnGo.TouchUpInside += (sender, e) => {

				int segmetDifficultyLevel = scChoice.SelectedSegment;
				int segmetType = scSingleChoiceOptions.SelectedSegment;
	
				DatabaseRequests.StoreMenuSettings(segmetDifficultyLevel, segmetType, Timer);
				if(viewController == null)
				{
					viewController = new MainViewController4();
				}

				NavigationController.PushViewController(viewController, false);

			};
		}

		public void returnSelectedRow()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = string.Format ("Data source={0};Version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) 
			{
				conn.Open ();
				string stm = "SELECT * FROM Profile";

				using (SqliteCommand cmd = new SqliteCommand (stm, conn)) {
					using (SqliteDataReader rdr = cmd.ExecuteReader ()) {
						while (rdr.Read ()) {
							object getSelectedRow = rdr ["selectedRow"];
						}
					}
				}
			}
				

		}

		public override void ViewWillAppear (bool animated) {
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, animated);
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			this.NavigationController.SetNavigationBarHidden (false, animated);
		}
	}
}