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

		public TableSource (string[] items, DetailViewController detailProfileMenu, MasterViewController masterViewController)
		{
			tableItems = items;
			this.detailProfileMenu = detailProfileMenu;
			this.masterViewController = masterViewController;
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
			detailProfileMenu.RefreshDetialView (indexPath.Row);
			detailProfileMenu.SetBackCreateNewProfile ();
			masterViewController.RefreshProfileTable ();
//			masterViewController.FillTableWithProfiles ();
		}

		private DetailViewController detailProfileMenu;
		private MasterViewController masterViewController;
	}
}