using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using Mono.Data.Sqlite;


namespace ZumaKeuzesContrast2
{
	public class TableSource : UITableViewSource
	{
		string[] tableItems;
		string cellIdentifier = "TableCell";

		public TableSource (string[] items)
		{
			tableItems = items;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return tableItems.Length;
		}

		public override UITableViewCell GetCell (UITableView tableview, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableview.DequeueReusableCell (cellIdentifier);
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
			}
			cell.TextLabel.Text = tableItems[indexPath.Row];
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{

			Console.WriteLine (indexPath.Item);
			int selectedRow = indexPath.Item;

			DatabaseRequests.SetSelectedRow (selectedRow);
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			throw new NotImplementedException ();
		}

	}
}