using System.Media;

namespace MouseXY
{
   class Sounds
   {
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
