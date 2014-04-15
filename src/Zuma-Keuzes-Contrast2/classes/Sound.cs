using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AVFoundation;

namespace Lisa.Zuma
{
	public class Sound
	{
		AVAudioPlayer player;

		public void Play (string sound,int repeat = 0)
		{
			NSUrl assets = NSUrl.FromString (sound);
			player = AVAudioPlayer.FromUrl(assets);

			player.NumberOfLoops = repeat;
			player.PrepareToPlay ();
			player.Play();
		
		}

		public void Play(string sound, Action onSoundEnd)
		{
			Play (sound);

			player.FinishedPlaying += (sender, e) => {
				onSoundEnd (); 
			};


		}

		public void Stop(){
			if (player != null) {
				player.Stop ();
				player.Dispose ();
			}
		}

		public void setRate(float Rate){
			try{
			player.Rate = Rate;
			}catch(Exception e) {
				Console.WriteLine (e.Message);
			}
		}
	}
}

