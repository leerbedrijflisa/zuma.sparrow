using System;
using System.IO;
using System.Text;
using System.Data;
using Mono.Data.Sqlite;

namespace ZumaKeuzesContrast2
{
	public class DatabaseRequests
	{

		public DatabaseRequests ()
		{
		}

		public static void CreateDatabase()
		{

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatebase = Path.Combine (documents, "db_Zuma_Keuzes.db");
			SqliteConnection.CreateFile (pathToDatebase);

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatebase);
			using (var conn = new SqliteConnection (connectionString)) {

				conn.Open ();
				using (var cmd = conn.CreateCommand ()) {
					cmd.CommandText = "CREATE TABLE MenuOptions (MenuOptionsID INTEGER PRIMARY KEY AUTOINCREMENT, scFirst INTEGER, clickTimer INTERGER, darkTimer INTERGER, storedProfile VARCHAR(255));";
					cmd.CommandType = CommandType.Text;
					cmd.ExecuteNonQuery ();
				}

				using (var cmd = conn.CreateCommand ()) {

					cmd.CommandText = "CREATE TABLE Profile (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name VARCHAR(255), ImageOne VARCHAR(255), ImageTwo VARCHAR(255), SoundOne VARCHAR(255), SoundTwo VARCHAR(255), selectedRow INTEGER, defaultProfile INTEGER);";
					cmd.CommandType = CommandType.Text;
					cmd.ExecuteNonQuery ();
				}

			}
		}

		public static void StoreMenuSettings(int scFirst, int clickTimer, int darkTimer, string storedProfile)
		{
			var varScFirst = scFirst;
			var varClickTimer = clickTimer;
			var varDarkTimer = darkTimer;
			var varStoredProfile = storedProfile;

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) {

				conn.Open ();

				using (var cmd = conn.CreateCommand ()) {

					cmd.CommandText = "INSERT INTO MenuOptions (scFirst, clickTimer, darkTimer, storedProfile) VALUES (@First, @clickTimer, @darkTimer, @storedProfile)";
					cmd.Parameters.AddWithValue ("@First", varScFirst);
					cmd.Parameters.AddWithValue ("@clickTimer", varClickTimer);
					cmd.Parameters.AddWithValue ("@darkTimer", varDarkTimer);
					cmd.Parameters.AddWithValue ("@storedProfile", varStoredProfile);
					cmd.ExecuteNonQuery ();

				}
			}
		}

		public static void CreateDefaultProfiles()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) 
			{
				conn.Open ();
				using (var cmd = conn.CreateCommand ()) 
				{
					cmd.CommandText = "INSERT INTO MenuOptions (scFirst, clickTimer, darkTimer, storedProfile) VALUES (0, 5, 5, '1')";
					cmd.ExecuteNonQuery ();
				}

				using (var cmd = conn.CreateCommand ()) 
				{
					cmd.CommandText = "INSERT INTO Profile (Name, ImageOne, ImageTwo, SoundOne, SoundTwo, defaultProfile) VALUES ('Links/Rechts', 'images/LeftArrow2.png', 'images/RightArrow2.png', 'sounds/Left.mp3', 'sounds/Right.mp3', 1)";
					cmd.ExecuteNonQuery ();
				}

				using (var cmd = conn.CreateCommand ()) 
				{
					cmd.CommandText = "INSERT INTO Profile (Name, ImageOne, ImageTwo, SoundOne, SoundTwo, defaultProfile) VALUES ('Ja/Nee', 'images/Yes.jpg', 'images/No.jpg', 'sounds/Yes.mp3', 'sounds/No.mp3', 1)";
					cmd.ExecuteNonQuery ();
				}

				using (var cmd = conn.CreateCommand ()) 
				{
					cmd.CommandText = "INSERT INTO Profile (Name, ImageOne, ImageTwo, SoundOne, SoundTwo, defaultProfile) VALUES ('Nee/Ja', 'images/No.jpg', 'images/Yes.jpg', 'sounds/No.mp3', 'sounds/Yes.mp3', 1)";
					cmd.ExecuteNonQuery ();
				}

				using (var cmd = conn.CreateCommand ()) 
				{
					cmd.CommandText = "INSERT INTO Profile (Name, ImageOne, ImageTwo, SoundOne, SoundTwo, defaultProfile) VALUES ('eten/drinken', 'images/eten.jpg', 'images/beker.jpg', 'sounds/hungry.mp3', 'sounds/thirsty.mp3', 1)";
					cmd.ExecuteNonQuery ();
				}

			}
		}

		public static void StoreNewProfile(string name, object leftImage, object rightImage, string leftSnd, string rightSnd)
		{
			var varName = name;
			var varLeftSnd = leftSnd;
			var varRightSnd = rightSnd;

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) {

				conn.Open ();

				using (var cmd = conn.CreateCommand ()) {

					cmd.CommandText = "INSERT INTO Profile (Name, ImageOne, ImageTwo, SoundOne, SoundTwo, defaultProfile) VALUES (@name, @leftImage, @rightImage, @leftSnd, @rightSnd, 0)";
					cmd.Parameters.AddWithValue ("@name", varName);
					cmd.Parameters.AddWithValue ("@leftImage", leftImage);
					cmd.Parameters.AddWithValue ("@rightImage", rightImage);
					cmd.Parameters.AddWithValue ("@leftSnd", varLeftSnd);
					cmd.Parameters.AddWithValue ("@rightSnd", varRightSnd);
					cmd.ExecuteNonQuery ();

				}
			}
		}

		public static void SetSelectedRow(string rowSelected)
		{
			var addRowSelected = rowSelected; 

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) 
			{
				conn.Open ();
				using (var cmd = conn.CreateCommand ()) 
				{
					cmd.CommandText = "INSERT INTO Profile (selectedRow) VALUES (@rowSelected)";
					cmd.Parameters.AddWithValue ("@rowSelected", addRowSelected);
					cmd.ExecuteNonQuery ();
				}
			}
		}
	}
}