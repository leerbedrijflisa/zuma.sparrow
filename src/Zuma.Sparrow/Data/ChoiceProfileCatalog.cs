using System;
using System.IO;
using System.Collections.Generic;
using SQLite;

namespace Zuma.Sparrow
{
	public class ChoiceProfileCatalog
	{
		public ChoiceProfile Find(int id)
		{
			ChoiceProfileData choiceProfileData;

			using (var db = new SQLiteConnection(PathToDatabase()))
			{
				choiceProfileData = db.Table<ChoiceProfileData>().Where(profile => profile.Id == id).FirstOrDefault();
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

		public List<ChoiceProfile> ReturnProfiles()
		{
			var profiles = new List<ChoiceProfile>();

			using (var db = new SQLiteConnection(PathToDatabase()))
			{
				var choiceProfileData = db.Table<ChoiceProfileData>();
				foreach (var profileData in choiceProfileData)
				{
					var profile = new ChoiceProfile();
					profile.Id = profileData.Id;
					profile.Name = profileData.Name;
					profile.FirstOption.ImageUrl = profileData.FirstOptionImageUrl;
					profile.FirstOption.AudioUrl = profileData.FirstOptionAudioUrl;
					profile.SecondOption.ImageUrl = profileData.SecondOptionImageUrl;
					profile.SecondOption.AudioUrl = profileData.SecondOptionAudioUrl;

					profiles.Add(profile);
				}
			}

			return profiles;
		}

		public int Create(ChoiceProfile newChoiceProfile)
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

			var profiles = ReturnProfiles();
			var count = profiles.Count;
			count--;
			var newProfile = profiles[count];
			return newProfile.Id;
		}

		public void Update(ChoiceProfile choiceProfile)
		{
			var choiceProfileData = new ChoiceProfileData();
			choiceProfileData.Id = choiceProfile.Id;
			choiceProfileData.Name = choiceProfile.Name;
			choiceProfileData.FirstOptionImageUrl = choiceProfile.FirstOption.ImageUrl;
			choiceProfileData.FirstOptionAudioUrl = choiceProfile.FirstOption.AudioUrl;
			choiceProfileData.SecondOptionImageUrl = choiceProfile.SecondOption.ImageUrl;
			choiceProfileData.SecondOptionAudioUrl = choiceProfile.SecondOption.AudioUrl;

			using (var db = new SQLiteConnection(PathToDatabase()))
			{
				db.Update(choiceProfileData);
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

