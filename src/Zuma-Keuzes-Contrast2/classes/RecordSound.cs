using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AVFoundation;
using System.Diagnostics;
using System.IO;
using MonoTouch.AudioToolbox;

namespace ZumaKeuzesContrast2
{
	public class RecordSound
	{
		public RecordSound ()
		{
			//Declare string for application temp path and tack on the file extension
			string fileName = string.Format ("Myfile{0}.wav", DateTime.Now.ToString ("yyyyMMddHHmmss"));
			string tmpdir = NSBundle.MainBundle.BundlePath + "/tmp";
			string audioFilePath = Path.Combine(tmpdir, fileName);

			Console.WriteLine("Audio File Path: " + audioFilePath);

			url = NSUrl.FromFilename(audioFilePath);
			//set up the NSObject Array of values that will be combined with the keys to make the NSDictionary
			NSObject[] values = new NSObject[]
			{
				NSNumber.FromFloat (44100.0f), //Sample Rate
				NSNumber.FromInt32 ((int)MonoTouch.AudioToolbox.AudioFormatType.LinearPCM), //AVFormat
				NSNumber.FromInt32 (2), //Channels
				NSNumber.FromInt32 (16), //PCMBitDepth
				NSNumber.FromBoolean (false), //IsBigEndianKey
				NSNumber.FromBoolean (false) //IsFloatKey
			};

			//Set up the NSObject Array of keys that will be combined with the values to make the NSDictionary
			NSObject[] keys = new NSObject[]
			{
				AVAudioSettings.AVSampleRateKey,
				AVAudioSettings.AVFormatIDKey,
				AVAudioSettings.AVNumberOfChannelsKey,
				AVAudioSettings.AVLinearPCMBitDepthKey,
				AVAudioSettings.AVLinearPCMIsBigEndianKey,
				AVAudioSettings.AVLinearPCMIsFloatKey
			};

			//Set Settings with the Values and Keys to create the NSDictionary
			settings = NSDictionary.FromObjectsAndKeys (values, keys);

			//Set recorder parameters
			recorder = AVAudioRecorder.ToUrl(url, settings, out error);

			//Set Recorder to Prepare To Record
			recorder.PrepareToRecord();
		}

		public void StartRecording()
		{
			recorder.Record ();
		}

		public void StopRecording()
		{
			this.recorder.Stop ();
		}

		public void PlayTempAudio()
		{
			var player = new AVPlayer (url);
			Console.WriteLine (url.ToString ());
			player.Play ();
		}
			
		public AVAudioRecorder recorder;
		NSError error = new NSError(new NSString("com.xamarin"), 1);
		NSUrl url;
		NSDictionary settings;

	}
}

