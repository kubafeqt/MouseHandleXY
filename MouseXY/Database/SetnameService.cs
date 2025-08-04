using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY.Database
{
   class SetNameService
   {
      public static void SaveOrUpdateSetName(int setId, string setName, string oldSetName = "")
      {
         DBAccess.SaveOrUpdateSetName(setId, setName, oldSetName);
         KeyPos.UpdateKeysSetName(setName, oldSetName); // Aktualizuje název sady v objektu KeyPos
      }

      public static void DeleteSetNameAndItKeysById(int setId)
      {
         string setName = KeyPos.setNames[setId];
         DBAccess.DeleteSetNameAndItKeysById(setId, setName);
         KeyPos.DeleteKeysBySetName(setName); // Smazání všech kláves spojených s tímto setName z objektu KeyPos v listu KeyPositions
      }
   }
}
