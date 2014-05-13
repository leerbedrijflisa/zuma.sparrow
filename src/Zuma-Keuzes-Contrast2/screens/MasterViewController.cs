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
		public MasterViewController (DetailViewController detailProfileMenu = null) : base ()
		{
			this.detailProfileMenu = detailProfileMenu;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			ReadMenuSettings ();
			LoadItemsToTableSource ();



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

		public void ProfileSaved()
		{
			profileIsUnsaved = false;
			Console.WriteLine (profileIsUnsaved);
		}

		private void LoadItemsToTableSource()
		{
			items = ProfileNames.ToArray ();
			var itemsTable = new TableSource (items, detailProfileMenu, this);
			tblProfileList.Source = itemsTable;
			tblProfileList.ReloadData ();
		}

		private void CreateNewProfile(object sender, EventArgs args)
		{
			if (!profileIsUnsaved) {
				profileIsUnsaved = true;
				ProfileNames.Add ("Untiteld Profile");
				LoadItemsToTableSource ();
				detailProfileMenu.CreateEmptyProfile ();
			}
		}

		private DetailViewController detailProfileMenu;
		private string name; 
		object returnFirst;
		List<string> ProfileNames = new List<string> ();
		string[] items;
		bool profileIsUnsaved;
	}
}