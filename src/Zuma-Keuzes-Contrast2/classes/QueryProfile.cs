using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;
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

					cmd.CommandText = "SELECT * FROM Profile WHERE storedInRow = @storedInRow";
					cmd.Parameters.AddWithValue ("@storedInRow", Row);
					using (SqliteDataReader rdr = cmd.ExecuteReader ()) {
						while (rdr.Read ()) {
							returnRow = rdr ["ID"];
							returnProfileName = rdr ["Name"];
							returnImageOne = rdr ["imageOne"];
							returnImageTwo = rdr ["imageTwo"];
							returnSoundOne = rdr ["soundOne"];
							returnSoundTwo = rdr ["soundTwo"];
							returnDefaultProfile = rdr ["defaultProfile"];
							returnStoredInRow = rdr ["storedInRow"];
						}
					}
				}
			}

			profileName = returnProfileName.ToString ();
			imageOne = returnImageOne.ToString ();
			imageTwo = returnImageTwo.ToString ();
			soundOne = returnSoundOne.ToString ();
			soundTwo = returnSoundTwo.ToString ();
			defaultProfile = returnDefaultProfile.ToString ();
			storedInRow = returnStoredInRow.ToString ();

			Console.WriteLine ("default profile " + defaultProfile);

			rowReturned = Convert.ToInt32 (returnRow);
			row = rowReturned.ToString ();

			databaseRow [0] = profileName;
			databaseRow [1] = imageOne;
			databaseRow [2] = imageTwo;
			databaseRow [3] = soundOne;
			databaseRow [4] = soundTwo;
			databaseRow [5] = row;
			databaseRow [6] = defaultProfile;
			databaseRow [7] = storedInRow;

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

		public List<string> ReadProfilesNames()
		{
			List<string> ProfileNames = new List<string> ();
			List<object> ProfileID = new List<object> ();
			var count = 1;

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
							var name = returnFirst.ToString ();
							ProfileNames.Add (name);
						}
					}
				}
			}
			return ProfileNames;
		}

		public void ReadStoredInRowTest()
		{
			var connectionString = ConnectionString ();
			using (var conn = new SqliteConnection (connectionString)) {
				conn.Open ();
				string stm = "SELECT * FROM Profile";

				using (SqliteCommand cmd = new SqliteCommand (stm, conn)) {
					using (SqliteDataReader rdr = cmd.ExecuteReader ()) {
						while (rdr.Read ()) {
							var storedInRow = rdr ["storedInRow"];
							Console.WriteLine (storedInRow.ToString () + " read storedInRow");
						}
					}
				}
			}
		}

		private string ConnectionString()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatebase = Path.Combine (documents, "db_Zuma_Keuzes.db");
			var connectionString = String.Format ("Data source={0};Version=3", pathToDatebase);

			return connectionString;
		}
	
		private object returnProfileName, returnRow, returnImageOne, returnImageTwo, returnSoundOne, returnSoundTwo, returnFirst, returnClickTimer, returnDarkTimer, returnStoredProfile, returnDefaultProfile, returnStoredInRow;
		private string profileName, imageOne, imageTwo, soundOne, soundTwo, row, first, clickTimer, darkTimer, storedProfile, defaultProfile, storedInRow;
		private string[] databaseRow = new string[8], menuSettings = new string[4];
		private int rowReturned;
	}
}

