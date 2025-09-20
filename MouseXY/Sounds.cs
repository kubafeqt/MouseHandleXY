using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System.Media;

namespace MouseXY
{
   class Sounds
   {
      public static Dictionary<soundTypes, string> soundTypesNamesDict = new Dictionary<soundTypes, string>()
      {
         { soundTypes.selectedKeysOpen, "default" }, //then load from db
         { soundTypes.selectedKeysClose, "default" }
      };

      public static Dictionary<soundTypes, (double startSec, double lengthSec)> soundTypesTimesDict = new Dictionary<soundTypes, (double, double)>()
      {
         { soundTypes.selectedKeysOpen, (0, 0) }, //then load from db
         { soundTypes.selectedKeysClose, (0, 0) }
      };

      public enum soundTypes
      {
         selectedKeysOpen,
         selectedKeysClose
      }

      public static void PlaySound(bool open, bool forceDef = false)
      {
         if (open)
         {
            if (forceDef || soundTypesNamesDict[soundTypes.selectedKeysOpen] == "default")
            {
               SystemSounds.Hand.Play();
            }
            else
            {
               PlaySound(soundTypes.selectedKeysOpen);
            }
         }
         else
         {
            if (forceDef || soundTypesNamesDict[soundTypes.selectedKeysClose] == "default")
            {
               SystemSounds.Asterisk.Play();
            }
            else
            {
               PlaySound(soundTypes.selectedKeysClose);
            }
         }
      }

      private static void PlaySound(soundTypes soundType)
      {
         string soundsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sounds");
         string selectedFile = soundTypesNamesDict[soundType];
         string fullPath = Path.Combine(soundsPath, selectedFile);
         AudioFileReader audioFile = new AudioFileReader(fullPath);

         double startSec = soundTypesTimesDict[soundType].startSec; //skip over
         double lengthSec = soundTypesTimesDict[soundType].lengthSec; //take
         //offset provider: start and length of segment
         var offsetProvider = new OffsetSampleProvider(audioFile.ToSampleProvider())
         {
            SkipOver = TimeSpan.FromSeconds(startSec), //start
            Take = TimeSpan.FromSeconds(lengthSec) //length
         };

         WaveOutEvent outputDevice = new WaveOutEvent();
         outputDevice.Init(offsetProvider);
         outputDevice.Play();
      }

      public static void PlaySound()
      {
         SystemSounds.Beep.Play();
      }

   }
}
