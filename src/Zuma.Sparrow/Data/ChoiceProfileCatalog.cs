using System;
using System.IO;
using System.Collections.Generic;
using SQLite;

namespace Zuma.Sparrow
{
	public class ChoiceProfileCatalog
	{
		public ChoiceProfile Find(string name)
		{
			ChoiceProfileData choiceProfileData;

			using (var db = new SQLiteConnection(PathToDatabase()))
			{
				choiceProfileData = db.Table<ChoiceProfileData>().Where(profile => profile.Name == name).FirstOrDefault();
			}

			var firstOption = new Option();
			firstOption.ImageUrl = choiceProfileData.FirstOptionImageUrl;
			firstOption.AudioUrl = choiceProfileData.FirstOptionAudioUrl;

			var secondOption = new Option();
			secondOption.ImageUrl = choiceProfileData.SecondOptionImageUrl;
			secondOption.AudioUrl = choiceProfileData.SecondOptionAudioUrl;

			var choiceProfile = new ChoiceProfile();

			choiceProfile.Id = choiceProfileData.Id;
			choiceProfile.Name = choiceProfileData.Name;
			choiceProfile.FirstOption = firstOption;
			choiceProfile.SecondOption = secondOption;

			return choiceProfile;
		}

		public List<string> ReturnProfileNames()
		{
			var names = new List<string>();

			using (var db = new SQLiteConnection(PathToDatabase()))
			{
				var choiceProfiles = db.Table<ChoiceProfileData>();
				foreach (var profile in choiceProfiles)
				{
					names.Add(profile.Name);
				}
			}

			return names;
		}

		public void Create(ChoiceProfile newChoiceProfile)
		{
			var newChoiceProfileData = new ChoiceProfileData();
			newChoiceProfileData.Name = newChoiceProfile.Name;
			newChoiceProfileData.FirstOptionImageUrl = newChoiceProfile.FirstOption.ImageUrl;
			newChoiceProfileData.FirstOptionAudioUrl = newChoiceProfile.FirstOption.AudioUrl;
			newChoiceProfileData.SecondOptionImageUrl = newChoiceProfile.SecondOption.ImageUrl;
			newChoiceProfileData.SecondOptionAudioUrl = newChoiceProfile.SecondOption.AudioUrl;

			using (var db = new SQLiteConnection(PathToDatabase()))
			{
				db.Insert(newChoiceProfileData);
			}
		}

		private string PathToDatabase()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Sparrow.db");
			return pathToDatabase;
		}
	}


}

