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
			profileNames = queryProfile.ReadProfilesNames();
			queryProfile.ReadStoredInRowTest ();
			LoadItemsToTableSource ();

			NSIndexPath currentRow = tblProfileList.IndexPathForSelectedRow;

			btnCreateNewProfile.TouchUpInside += CreateNewProfile;
		}

		public void ProfileSaved()
		{
			profileIsUnsaved = false;
			Console.WriteLine (profileIsUnsaved);
		}

		private void LoadItemsToTableSource()
		{
			items = profileNames.ToArray ();
			var itemsTable = new TableSource (items, detailProfileMenu, this);
			tblProfileList.Source = itemsTable;
			tblProfileList.ReloadData ();
		}

		private void CreateNewProfile(object sender, EventArgs args)
		{
			if (!profileIsUnsaved) {
				profileIsUnsaved = true;
				profileNames.Add ("Untiteld Profile");
				LoadItemsToTableSource ();
				detailProfileMenu.CreateEmptyProfile ();
			}
		}

		private DetailViewController detailProfileMenu;
		private string[] items;
		private bool profileIsUnsaved;

		List<int> profileID = new List<int>();
		List<string> profileNames = new List<string> ();
		QueryProfile queryProfile = new QueryProfile();

	}
}