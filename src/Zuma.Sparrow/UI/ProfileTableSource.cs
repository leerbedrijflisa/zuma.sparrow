using System;
using System.Collections.Generic;

using MonoTouch.UIKit;
using MonoTouch.Foundation;


namespace Zuma.Sparrow
{
	public class ProfileTableSource : UITableViewSource
	{
		public ProfileTableSource()
		{
			profiles = choiceProfileCatalog.ReturnProfiles();
		}

		public override int RowsInSection(UITableView tableview, int section)
		{
			return profiles.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = new UITableViewCell();
			var profile = new ChoiceProfile();
			profile = profiles[indexPath.Row];
			cell.TextLabel.Text = profile.Name;
			return cell;
		}

		public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var eventArgs = new ProfileEventArgs();
			eventArgs.Profile = profiles[indexPath.Row];
			OnProfileSelected(tableView, eventArgs);
		}

		public event EventHandler<ProfileEventArgs> ProfileSelected;

		protected void OnProfileSelected(UITableView tableView, ProfileEventArgs eventArgs)
		{
			if (ProfileSelected != null)
			{
				ProfileSelected(tableView, eventArgs);
			}
		}

		private List<ChoiceProfile> profiles = new List<ChoiceProfile>();
		private ChoiceProfileCatalog choiceProfileCatalog = new ChoiceProfileCatalog(); 
	}

	public class ProfileEventArgs : EventArgs
	{
		public ChoiceProfile Profile
		{
			get;
			set; 
		}
	}
}

