using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY
{
   class Sounds
   {
      public static void PlaySound(bool open)
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
