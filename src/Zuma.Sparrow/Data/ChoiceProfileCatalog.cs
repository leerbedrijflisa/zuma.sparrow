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
			using (var db = new SQLiteConnection(PathToDatabase()))
			{
				profileData = db.Table<ChoiceProfileData>().Where(dbProfile => dbProfile.Id == id).FirstOrDefault();
			}

			profile.Id = profileData.Id;
			profile.Name = profileData.Name;
			profile.FirstOption.ImageUrl = profileData.FirstOptionImageUrl;
			profile.FirstOption.AudioUrl = profileData.FirstOptionAudioUrl;
			profile.SecondOption.ImageUrl = profileData.SecondOptionImageUrl;
			profile.SecondOption.AudioUrl = profileData.SecondOptionAudioUrl;
			if (profileData.ProfileType == 0)
			{
				profile.CurrentProfileType = ProfileType.Custom;
			}
			else if(profileData.ProfileType == 1)
			{
				profile.CurrentProfileType = ProfileType.Default;
			}

			return profile;
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
					if (profileData.ProfileType == 0)
					{
						profile.CurrentProfileType = ProfileType.Custom;
					}
					else if(profileData.ProfileType == 1)
					{
						profile.CurrentProfileType = ProfileType.Default;
					}

					profiles.Add(profile);
				}
			}

			return profiles;
		}

		public int Create(ChoiceProfile choiceProfile)
		{
			profileData.Name = choiceProfile.Name;
			profileData.FirstOptionImageUrl = choiceProfile.FirstOption.ImageUrl;
			profileData.FirstOptionAudioUrl = choiceProfile.FirstOption.AudioUrl;
			profileData.SecondOptionImageUrl = choiceProfile.SecondOption.ImageUrl;
			profileData.SecondOptionAudioUrl = choiceProfile.SecondOption.AudioUrl;
			if (choiceProfile.CurrentProfileType == ProfileType.Default)
			{
				profileData.ProfileType = 1;
			}
			else if (choiceProfile.CurrentProfileType == ProfileType.Custom)
			{
				profileData.ProfileType = 0;
			}

			using (var db = new SQLiteConnection(PathToDatabase()))
			{
				db.Insert(profileData);
			}

			var profiles = ReturnProfiles();
			var count = profiles.Count;
			count--;
			var newProfile = profiles[count];
			return newProfile.Id;
		}

		public void Update(ChoiceProfile choiceProfile)
		{
			profileData.Id = choiceProfile.Id;
			profileData.Name = choiceProfile.Name;
			profileData.FirstOptionImageUrl = choiceProfile.FirstOption.ImageUrl;
			profileData.FirstOptionAudioUrl = choiceProfile.FirstOption.AudioUrl;
			profileData.SecondOptionImageUrl = choiceProfile.SecondOption.ImageUrl;
			profileData.SecondOptionAudioUrl = choiceProfile.SecondOption.AudioUrl;
			if (choiceProfile.CurrentProfileType == ProfileType.Default)
			{
				profileData.ProfileType = 1;
			}
			else if (choiceProfile.CurrentProfileType == ProfileType.Custom)
			{
				profileData.ProfileType = 0;
			}

			using (var db = new SQLiteConnection(PathToDatabase()))
			{
				db.Update(profileData);
			}
		}

		public void Delete(ChoiceProfile choiceProfile)
		{
			profileData.Id = choiceProfile.Id;
			profileData.Name = choiceProfile.Name;
			profileData.FirstOptionImageUrl = choiceProfile.FirstOption.ImageUrl;
			profileData.FirstOptionAudioUrl = choiceProfile.FirstOption.AudioUrl;
			profileData.SecondOptionImageUrl = choiceProfile.SecondOption.ImageUrl;
			profileData.SecondOptionAudioUrl = choiceProfile.SecondOption.AudioUrl;
			if (choiceProfile.CurrentProfileType == ProfileType.Default)
			{
				profileData.ProfileType = 1;
			}
			else if (choiceProfile.CurrentProfileType == ProfileType.Custom)
			{
				profileData.ProfileType = 0;
			}

			using (var db = new SQLiteConnection(PathToDatabase()))
			{
				db.Delete(profileData);
			}
		}

		private string PathToDatabase()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Sparrow.db");
			return pathToDatabase;
		}

		private ChoiceProfile profile = new ChoiceProfile();
		private ChoiceProfileData profileData = new ChoiceProfileData();
	}
}

