using System;
using MonoTouch.UIKit;

namespace Zuma.Sparrow
{
	/// <summary>
	/// Use this navigation controller to force the app to start in landscape mode.
	/// </summary>
	public class RotationNavigationController : UINavigationController
	{
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return this.VisibleViewController.GetSupportedInterfaceOrientations();
		}
	}
}