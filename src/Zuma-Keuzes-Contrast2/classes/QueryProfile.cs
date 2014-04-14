using System;
using System.IO;
using System.Text;
using System.Data;
using Mono.Data.Sqlite;

namespace ZumaKeuzesContrast2
{
	public class QueryProfile
	{
		public QueryProfile ()
		{
		}

		public string[] returnProfileRow(int Row)
		{

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) {
				conn.Open ();
				using (var cmd = conn.CreateCommand ()) {

					cmd.CommandText = "SELECT * FROM Profile WHERE ID = @ID";
					cmd.Parameters.AddWithValue ("@ID", Row);
					using (SqliteDataReader rdr = cmd.ExecuteReader ()) {
						while (rdr.Read ()) {
							returnRow = rdr ["ID"];
							returnProfileName = rdr ["Name"];
							returnImageOne = rdr ["imageOne"];
							returnImageTwo = rdr ["imageTwo"];
							returnSoundOne = rdr ["soundOne"];
							returnSoundTwo = rdr ["soundTwo"];
						}
					}
				}
			}

			profileName = returnProfileName.ToString ();
			imageOne = returnImageOne.ToString ();
			imageTwo = returnImageTwo.ToString ();
			soundOne = returnSoundOne.ToString ();
			soundTwo = returnSoundTwo.ToString ();

			rowReturned = Convert.ToInt32 (returnRow);
			row = rowReturned.ToString ();

			databaseRow [0] = profileName;
			databaseRow [1] = imageOne;
			databaseRow [2] = imageTwo;
			databaseRow [3] = soundOne;
			databaseRow [4] = soundTwo;
			databaseRow [5] = row;

			return databaseRow;
		}

		public string[] ReadMenuSettings()
		{

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatebase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatebase);
			using (var conn = new SqliteConnection (connectionString)) {

				conn.Open ();
				string stm = "SELECT * FROM MenuOptions";

				using (SqliteCommand cmd = new SqliteCommand (stm, conn)) {
					using (SqliteDataReader rdr = cmd.ExecuteReader ()) {
						while (rdr.Read ()) {
							returnFirst = rdr ["scFirst"];
							returnClickTimer = rdr ["clickTimer"];
							returnDarkTimer = rdr ["darkTimer"];
							returnStoredProfile = rdr ["storedProfile"];

						}
					}
				}
			}

			first = returnFirst.ToString ();
			clickTimer = returnClickTimer.ToString ();
			darkTimer = returnDarkTimer.ToString ();
			storedProfile = returnStoredProfile.ToString ();

			menuSettings [0] = first;
			menuSettings [1] = clickTimer;
			menuSettings [2] = darkTimer;
			menuSettings [3] = storedProfile;

			return menuSettings;

		}

		public void CreateEmptyProfile(string name)
		{
			var varName = name;

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) {

				conn.Open ();

				using (var cmd = conn.CreateCommand ()) 
				{
					cmd.CommandText = "INSERT INTO Profile (Name, ImageOne, ImageTwo) VALUES ('@name', 'images/empty.png', 'images/empty.png')";
					cmd.Parameters.AddWithValue ("@name", varName);
					cmd.ExecuteNonQuery ();
				}
			}

		}

		private object returnProfileName, returnRow, returnImageOne, returnImageTwo, returnSoundOne, returnSoundTwo, returnFirst, returnClickTimer, returnDarkTimer, returnStoredProfile;
		private string profileName, imageOne, imageTwo, soundOne, soundTwo, row, first, clickTimer, darkTimer, storedProfile;
		private string[] databaseRow = new string[6], menuSettings = new string[4];
		private int rowReturned;
	}
}

