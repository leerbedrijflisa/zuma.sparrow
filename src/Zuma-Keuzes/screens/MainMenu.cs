using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Mono.Data.Sqlite;
using System.IO;
using System.Text;
using System.Data;

namespace ZumaKeuzes
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

		MainScreenViewController viewController;


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//CreateDataBase();

			btnGo.TouchUpInside += (sender, e) => {


				if (scChoice.SelectedSegment == 0)
				{


					int segmetSingleChoice = scSingleChoiceOptions.SelectedSegment;

					Console.WriteLine(segmetSingleChoice);

					if(viewController == null)
					{
						viewController = new MainScreenViewController();
					}

					NavigationController.PushViewController(viewController, true);
				}

			};

		}

		void CreateDataBase()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes");
			SqliteConnection.CreateFile (pathToDatabase);

			//create table
			var connectionString = string.Format ("Data Source=[0];version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) {
				conn.Open ();
				using (var cmd = conn.CreateCommand ()) {
					cmd.CommandText = "CREATE TABLE Choice (Option INTEGER)";
					cmd.CommandType = CommandType.Text;
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

