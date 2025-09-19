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

      public static Dictionary<soundTypes, (double, double)> soundTypesTimesDict = new Dictionary<soundTypes, (double, double)>()
      {
         { soundTypes.selectedKeysOpen, (0, 0) }, //then load from db
         { soundTypes.selectedKeysClose, (0, 0) }
      };

      public enum soundTypes
      {
         selectedKeysOpen,
         selectedKeysClose
      }

      public static void PlayDefSound(bool open)
      {
         if (open)
         {
            SystemSounds.Hand.Play();
         }
         else
         {
            SystemSounds.Asterisk.Play();
         }
      }

      public static void PlaySound()
      {
         SystemSounds.Beep.Play();
      }

   }
}
