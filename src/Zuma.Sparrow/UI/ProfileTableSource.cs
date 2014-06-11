using System;
using System.Collections.Generic;
using MonoTouch.UIKit;


namespace Zuma.Sparrow
{
	public class ProfileTableSource : UITableViewSource
	{
		public ProfileTableSource()
		{
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

		private List<string> profiles = new List<string>();
	}

	public class ProfileEventArgs : EventArgs
	{
		public string Profile
		{
			get;
			set; 
		}
	}
}

