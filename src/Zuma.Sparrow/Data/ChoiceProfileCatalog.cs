using System;
using System.IO;
using SQLite;

namespace Zuma.Sparrow
{
	public class ChoiceProfileCatalog
	{
		public ChoiceProfile Find(string name)
		{
			ChoiceProfileData choiceProfileData;

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Sparrow.db");

			using (var db = new SQLiteConnection(pathToDatabase))
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
	}


}

