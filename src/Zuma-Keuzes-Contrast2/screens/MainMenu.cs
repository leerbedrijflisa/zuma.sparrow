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

		MainViewController4 viewController;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//Create's a db if there isn't one already with a table to handle the Menu segement button options
			Create_Zuma_DB();

			btnAdd.SetImage (UIImage.FromFile ("AddBTN.png"), UIControlState.Normal);
			btnSubtract.SetImage (UIImage.FromFile ("SubtractBTN.png"), UIControlState.Normal);

			int Timer = 5;

			LblTimer.Text = Timer.ToString();

			btnAdd.TouchUpInside += (sender, e) => {
				Timer ++;
				LblTimer.Text = Timer.ToString();
			};

			btnSubtract.TouchUpInside += (sender, e) => {
				if(Timer <= 1) 
				{ 
					btnSubtract.SetImage (UIImage.FromFile ("AddSubtractBTN.png"), UIControlState.Disabled);
				} 
				else 
				{ 
					btnSubtract.SetImage (UIImage.FromFile ("AddSubtractBTN.png"), UIControlState.Disabled);
					Timer --;
					LblTimer.Text = Timer.ToString();
				}

			};

			btnGo.TouchUpInside += (sender, e) => {

				int segmetDifficultyLevel = scChoice.SelectedSegment;
				int segmetType = scSingleChoiceOptions.SelectedSegment;
	
				enter_scValue(segmetDifficultyLevel, segmetType, Timer);
				if(viewController == null)
				{
					viewController = new MainViewController4();
				}

				NavigationController.PushViewController(viewController, false);

			};
		}

		public void Create_Zuma_DB()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatebase = Path.Combine (documents, "db_Zuma_Keuzes.db");
			SqliteConnection.CreateFile (pathToDatebase);

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatebase);
			using (var conn = new SqliteConnection (connectionString)) {

				conn.Open ();
				using (var cmd = conn.CreateCommand ()) {

					cmd.CommandText = "CREATE TABLE MenuOptions (MenuOptionsID INTEGER PRIMARY KEY AUTOINCREMENT, scFirst INTEGER, scSecond INTEGER, Timer INTERGER)";
					cmd.CommandType = CommandType.Text;
					cmd.ExecuteNonQuery ();
				}

			}
		}

		public void enter_scValue(int scFirst, int scSecond, int Timer)
		{
			var varScFirst = scFirst;
			var varScSecond = scSecond;
			var varTimer = Timer;

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatebase = Path.Combine (documents, "db_Zuma_Keuzes.db");
			//SqliteConnection.CreateFile (pathToDatebase);

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatebase);
			using (var conn = new SqliteConnection (connectionString)) {

				conn.Open ();

				using (var cmd = conn.CreateCommand ()) {

					cmd.CommandText = "INSERT INTO MenuOptions (scFirst, scSecond, Timer) VALUES (@First, @Second, @Timer)";
					cmd.Parameters.AddWithValue ("@First", varScFirst);
					cmd.Parameters.AddWithValue ("@Second", varScSecond);
					cmd.Parameters.AddWithValue ("@Timer", varTimer);
					cmd.ExecuteNonQuery ();

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