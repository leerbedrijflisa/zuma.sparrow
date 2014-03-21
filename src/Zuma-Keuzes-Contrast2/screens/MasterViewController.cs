using System;
using System.Drawing;
using System.Linq;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;
using Mono.Data.Sqlite;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace ZumaKeuzesContrast2
{
	public partial class MasterViewController : DialogViewController 
	{

		object returnFirst;
		string name;
		List<string> ProfileNames = new List<string> ();

		public MasterViewController () : base (null)
		{
	
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
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

