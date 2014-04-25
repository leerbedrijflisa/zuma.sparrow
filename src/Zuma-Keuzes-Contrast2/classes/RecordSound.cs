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

		bool PrepareAudioRecording (bool isLeft)
		{
			//Declare string for application temp path and tack on the file extension
			string fileName = string.Format("Myfile{0}.aac", DateTime.Now.ToString("yyyyMMddHHmmss"));
			string tempRecording = NSBundle.MainBundle.BundlePath + "/../tmp/" + fileName;

			Console.WriteLine(tempRecording);
			this.audioFilePath = NSUrl.FromFilename(tempRecording);

			if (isLeft)
			{
				audioLeft = this.audioFilePath.ToString ();
//				detailViewController.NewSndPath (audioLeft, true);
			} 
			else 
			{
				audioRight = this.audioFilePath.ToString ();
//				detailViewController.NewSndPath (audioRight, false);
			}

			var audioSettings = new AudioSettings() {
				SampleRate = 44100.0f, 
				Format = MonoTouch.AudioToolbox.AudioFormatType.MPEG4AAC,
				NumberChannels = 1,
				AudioQuality = AVAudioQuality.High
			};

			//Set recorder parameters
			NSError error;
			recorder = AVAudioRecorder.Create(this.audioFilePath, audioSettings, out error);
			if((recorder == null) || (error != null))
			{
				Console.WriteLine(error);
				return false;
			}

			//Set Recorder to Prepare To Record
			if(!recorder.PrepareToRecord())
			{
				recorder.Dispose();
				recorder = null;
				return false;
			}

			recorder.FinishedRecording += delegate (object sender, AVStatusEventArgs e) {
				recorder.Dispose();
				recorder = null;
				Console.WriteLine("Done Recording (status: {0})", e.Status);
			};

			return true;
		}

		public void StartRecording(bool isLeft)
		{
			NSError error;
			var session = AVAudioSession.SharedInstance();
			session.SetCategory (AVAudioSession.CategoryRecord, out error);
			PrepareAudioRecording (isLeft);
			recorder.Record ();
		}

		public void StopRecording()
		{
			this.recorder.Stop ();
		}

		public void PlayTempAudio(bool isLeft)
		{
			Sound snd = new Sound();
			if (isLeft)
			{
				snd.Play (audioLeft);
			}
			else 
			{
				snd.Play (audioRight);
			}
		}
			
		public AVAudioRecorder recorder;
		NSError error = null;
		NSUrl audioFilePath;
		NSDictionary settings;
		string audioLeft, audioRight;

//		DetailViewController detailViewController = new DetailViewController();

	}
}

