using System;
using System.IO;
using System.Text;
using System.Data;
using Mono.Data.Sqlite;

namespace ZumaKeuzesContrast2
{
	public class DatabaseRequests
	{
//		public object returnFirst { get; } 
//		public object returnSecond { get; }
//		public object returnTimer { get; }

		public static void CreateZumaSparrowDB()
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

		public static void StoreMenuSettings(int scFirst, int scSecond, int Timer)
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
	}
}

