using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MouseXY
{
   class ExportImport
   {
      private static string exportFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "exports");

      public static void ExportToJson()
      {
         var data = new ExpImpDataContainer
         {
            KeyPositions = KeyPos.KeyPositions,
            setNames = KeyPos.setNames
         };

         var options = new JsonSerializerOptions { WriteIndented = true };
         string json = JsonSerializer.Serialize(data, options);


         if (!Directory.Exists(exportFolder))
         {
            Directory.CreateDirectory(exportFolder);
         }

         string fileName = ExportBox.ShowJsonFileSelector(exportFolder);
         if (!string.IsNullOrWhiteSpace(fileName))
         {
            string finalPath = Path.Combine(exportFolder, fileName);
            File.WriteAllText(finalPath, json);
            MessageBox.Show($"Data byla úspěšně uložena pod názvem {fileName} .", "Uloženo", MessageBoxButtons.OK, MessageBoxIcon.Information);
         }
      }

      public static void ImportFromJson()
      {
         string fileName = ImportBox.ShowJsonFileSelector(exportFolder);

         if (!string.IsNullOrWhiteSpace(fileName))
         {
            string fullPath = Path.Combine(exportFolder, fileName);

            try
            {
               string json = File.ReadAllText(fullPath);
               var data = JsonSerializer.Deserialize<ExpImpDataContainer>(json);

               if (data != null)
               {
                  //var importedSetNames = data.setNames;

                  //foreach (var kvp in importedSetNames)
                  //{
                  //   string setName = kvp.Value;

                  //   // Kolize
                  //   if (KeyPos.setNames.ContainsValue(setName))
                  //   {
                  //      DialogResult result = MessageBox.Show(
                  //          $"SetName \"{setName}\" už existuje.\nChceš ho přepsat?\n\nAno = přepsat\nNe = přejmenovat\nZrušit = přeskočit",
                  //          "Kolize názvu",
                  //          MessageBoxButtons.YesNoCancel,
                  //          MessageBoxIcon.Question
                  //      );

                  //      if (result == DialogResult.Yes)
                  //      {
                  //         // Přepsat = ponecháme
                  //         continue;
                  //      }
                  //      else if (result == DialogResult.No)
                  //      {
                  //         // Přejmenovat
                  //         string newName = PromptForNewSetName(setName);
                  //         if (!string.IsNullOrEmpty(newName))
                  //         {
                  //            var value = kvp.Value;
                  //            importedSetNames.Remove(kvp.Key);
                  //            importedSetNames[kvp.Key] = value;
                  //         }
                  //         else
                  //         {
                  //            importedSetNames.Remove(setName); // zrušeno
                  //         }
                  //      }
                  //      else if (result == DialogResult.Cancel)
                  //      {
                  //         importedSetNames.Remove(setName); // Přeskočit
                  //      }
                  //   }
                  //}

                  //// Import
                  //foreach (var kvp in importedSetNames)
                  //{
                  //   KeyPos.setNames[kvp.Key] = kvp.Value;
                  //}

                  //// Předpokládáme, že KeyPositions se importují bez kolizí
                  //KeyPos.KeyPositions = data.KeyPositions;

                  //   MessageBox.Show($"Import dokončen z \"{fileName}\".", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
               else
               {
                  MessageBox.Show("Soubor je prázdný nebo chybný.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               }
            }
            catch (Exception ex)
            {
               MessageBox.Show("Chyba při načítání souboru:\n" + ex.Message, "Chyba importu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }
      }

      private static string PromptForNewSetName(string oldName)
      {
         return InputBox.Show($"Zadej nový název pro set \"{oldName}\":", "Přejmenovat", oldName + "_kopie");
      }
   }
}