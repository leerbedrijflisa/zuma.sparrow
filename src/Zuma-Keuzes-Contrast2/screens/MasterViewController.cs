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
	public partial class MasterViewController : UIViewController
	{
		public MasterViewController (DetailViewController detailProfileMenu) : base ()
		{
			this.detailProfileMenu = detailProfileMenu;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			ReadMenuSettings ();

			items = ProfileNames.ToArray ();

			itemstable = new TableSource (items, detailProfileMenu);

			tblProfileList.Source = itemstable;

			NSIndexPath currentRow = tblProfileList.IndexPathForSelectedRow;

			btnCreateNewProfile.TouchUpInside += CreateNewProfile;

		}
			
		public void ReadMenuSettings()
		{

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatebase = Path.Combine (documents, "db_Zuma_Keuzes.db");

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

		private void CreateNewProfile(object sender, EventArgs args)
		{
			View.AddSubview (inputProfileName);
			newProfileName = inputProfileName.Text;
			btnCreateNewProfile.Hidden = true;
		}

		private DetailViewController detailProfileMenu;
		private string name, newProfileName;
		object returnFirst;
		List<string> ProfileNames = new List<string> ();
		string[] items;
		TableSource itemstable;
		QueryProfile queryProfile = new QueryProfile ();

		UITextField inputProfileName = new UITextField
		{
			Placeholder = "Vul naam in",
			BorderStyle = UITextBorderStyle.RoundedRect,
			Frame = new RectangleF(5, 30, 275, 25)
		};

		UIButton btnSaveProfileName = new UIButton()
		{
			UIButton.FromType(UIButtonType.RoundedRect),

		};
	}
}

