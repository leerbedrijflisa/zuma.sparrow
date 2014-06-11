using System;
using System.Collections.Generic;
using MonoTouch.UIKit;


namespace Zuma.Sparrow
{
	public class ProfileTableSource : UITableViewSource
	{
		public ProfileTableSource(ProfileMenuViewController profileMenu)
		{
			this.profileMenu = profileMenu; 
			profiles.Add("Joost");
			profiles.Add("Stephan");
			profiles.Add("Josja");
		}

		public override int RowsInSection(UITableView tableview, int section)
		{
			return profiles.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var cell = new UITableViewCell();
			cell.TextLabel.Text = profiles[indexPath.Row];
			return cell;
		}

		public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			profileMenu.OnTabelRowSelected (profiles[indexPath.Row]);
		}

		private List<string> profiles = new List<string>();
		private ProfileMenuViewController profileMenu = new ProfileMenuViewController();
	}
}

