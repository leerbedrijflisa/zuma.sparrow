using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AVFoundation;
using System.Diagnostics;
using System.IO;
using MonoTouch.AudioToolbox;
using Lisa.Zuma;

namespace ZumaKeuzesContrast2
{
	public class RecordSound
	{
		public RecordSound ()
		{

		}

		bool PrepareAudioRecording ()
		{
			//Declare string for application temp path and tack on the file extension
			string fileName = string.Format ("Myfile{0}.aac", DateTime.Now.ToString ("yyyyMMddHHmmss"));
			string tempRecording = NSBundle.MainBundle.BundlePath + "/../tmp/" + fileName;
			this.audioFilePath = NSUrl.FromFilename(tempRecording);

			//set up the NSObject Array of values that will be combined with the keys to make the NSDictionary
			NSObject[] values = new NSObject[]
			{    
				NSNumber.FromFloat(44100.0f),
				NSNumber.FromInt32((int)MonoTouch.AudioToolbox.AudioFormatType.MPEG4AAC),
				NSNumber.FromInt32(2),
				NSNumber.FromInt32((int)AVAudioQuality.High)
			};
			//Set up the NSObject Array of keys that will be combined with the values to make the NSDictionary
			NSObject[] keys = new NSObject[]
			{
				AVAudioSettings.AVSampleRateKey,
				AVAudioSettings.AVFormatIDKey,
				AVAudioSettings.AVNumberOfChannelsKey,
				AVAudioSettings.AVEncoderAudioQualityKey
			};			
			//Set Settings with the Values and Keys to create the NSDictionary
			settings = NSDictionary.FromObjectsAndKeys (values, keys);

			//Set recorder parameters
			NSError error;
			recorder = AVAudioRecorder.ToUrl(this.audioFilePath, settings, out error);

			//Set Recorder to Prepare To Record
			if (!recorder.PrepareToRecord ()) {
				recorder.Dispose ();
				recorder = null;
				return false;
			}

			recorder.FinishedRecording += delegate (object sender, AVStatusEventArgs e) {
				recorder.Dispose ();
				recorder = null;
			};
			return true;
		}

		public void StartRecording()
		{
			PrepareAudioRecording ();
			recorder.Record ();
		}

		public void StopRecording()
		{
			this.recorder.Stop ();
		}

		public void PlayTempAudio()
		{
			Sound snd = new Sound();
			snd.Play (audioFilePath.ToString());
		}
			
		public AVAudioRecorder recorder;
		NSError error = new NSError(new NSString("com.xamarin"), 1);
		NSUrl audioFilePath;
		NSDictionary settings;

	}
}

