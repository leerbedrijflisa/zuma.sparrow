using System;
using MonoTouch.CoreMotion;
using MonoTouch.Foundation;

namespace Zuma.Sparrow
{
	public class RotationHelper
	{
		public RotationHelper()
		{
			var eventArgs = new RotationEventArgs();
			_motionManager = new CMMotionManager ();
			_motionManager.StartAccelerometerUpdates (NSOperationQueue.CurrentQueue, (data, error) => {
				if (data.Acceleration.Z > 0.890) {
					OnScreenRotated(_motionManager, eventArgs);
				}
			});
		}

		public event EventHandler<RotationEventArgs> ScreenRotated;

		protected void OnScreenRotated(CMMotionManager _motionManager,  RotationEventArgs EventArgs)
		{
			if (ScreenRotated != null)
			{
				ScreenRotated(_motionManager, EventArgs);
			}
		}

		private CMMotionManager _motionManager;
	}

	public class RotationEventArgs : EventArgs
	{
	}
}

