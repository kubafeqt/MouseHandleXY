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
            KeyPositions = KeyPos.KeyPositions.ToList(),
            setNames = KeyPos.setNames.ToDictionary()
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

      public static bool import = false;
      public static void ImportFromJson()
      {
         string fileName = ImportBox.ShowJsonFileSelector(exportFolder);

         if (!string.IsNullOrWhiteSpace(fileName))
         {
            string fullPath = Path.Combine(exportFolder, fileName);

            try
            {

            }
            catch (Exception)
            {

               throw;
            }
            {
               import = true; //do not add to KeyPos.KeyPosition list
               string json = File.ReadAllText(fullPath);
               var data = JsonSerializer.Deserialize<ExpImpDataContainer>(json);
               import = false;

               if (data != null)
               {
                  var importedSetNames = data.setNames;
                  var importedKeyPositions = data.KeyPositions;

                  //default setname:
                  string defSetName = "default";
               newSetNameExist:
                  string msg = $"Chceš přepsat \"{defSetName}\" setname?\nChceš ho přepsat?\n\nAno = přepsat\nNe = přejmenovat\nZrušit = nepřepisovat";
                  DialogResult defResult = MessageBox.Show(
                           msg,
                           "Kolize názvu",
                           MessageBoxButtons.YesNoCancel,
                           MessageBoxIcon.Question
                       );

                  
                  if (defResult == DialogResult.Yes) //replace default setname and its saved keypositions
                  {
                     KeyPos.KeyPositions.RemoveAll(x => x.SetName == defSetName); //smazat všechny se stejným setName v aktual
                     if (defSetName != "default")
                     {
                        importedKeyPositions.RemoveAll(x => x.SetName == defSetName); //smazat všechny se stejným setName v imported
                        ChangeSetNamesInImportedKeyPositions(importedKeyPositions, "default", defSetName);
                     }
                     var newPositions = importedKeyPositions.Where(x => x.SetName == defSetName);
                     KeyPos.KeyPositions.AddRange(newPositions);
                  }
                  else if (defResult == DialogResult.No) //rename imported default setname
                  {
                     string newSetName = PromptForNewSetName("default");
                     if (!string.IsNullOrEmpty(newSetName))
                     {
                        if (KeyPos.setNames.ContainsValue(newSetName))
                        {
                           defSetName = newSetName;
                           goto newSetNameExist;
                        }
                        //importedSetNames[kvp.Key] = newSetName;
                        importedSetNames.Add(KeyPos.PossibleFreeIdInDictKeys(importedSetNames), newSetName);
                        ChangeSetNamesInImportedKeyPositions(importedKeyPositions, defSetName, newSetName);
                     }
                  }


                  //pokud setname existuje -> zachovej ID
                  //pokud setname neexistuje -> přidej nový setname s novým ID

                  // oveření importovaných setNames
                  foreach (var kvp in importedSetNames.ToDictionary())
                  {
                     string setName = kvp.Value;

                     // Kolize
                     if (KeyPos.setNames.ContainsValue(setName))
                     {
                     setNameExist:
                        DialogResult result = MessageBox.Show(
                            $"SetName \"{setName}\" už existuje.\nChceš ho přepsat?\n\nAno = přepsat\nNe = přejmenovat\nZrušit = přeskočit",
                            "Kolize názvu",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question
                        );

                        if (result == DialogResult.Yes)
                        {
                           importedSetNames[kvp.Key] = setName;
                           continue; // Přepsat = ponecháme
                        }
                        else if (result == DialogResult.No)
                        {
                           // Přejmenovat
                           string newSetName = PromptForNewSetName(setName);
                           if (!string.IsNullOrEmpty(newSetName))
                           {
                              if (KeyPos.setNames.ContainsValue(newSetName))
                              {
                                 setName = newSetName;
                                 goto setNameExist;
                              }
                              //importedSetNames.Remove(kvp.Key);
                              importedSetNames[kvp.Key] = newSetName;
                              ChangeSetNamesInImportedKeyPositions(importedKeyPositions, setName, newSetName);
                           }
                           else
                           {
                              //goto setNameExist;
                              importedSetNames.Remove(kvp.Key); // zrušeno
                           }
                        }
                        else if (result == DialogResult.Cancel)
                        {
                           importedSetNames.Remove(kvp.Key); // Přeskočit
                        }
                     }
                  }

                  // Import setnames a keypositons pouze, kde je setname
                  foreach (var kvp in importedSetNames.ToDictionary())
                  {
                     string setname = kvp.Value;
                     if (KeyPos.setNames.ContainsValue(setname))
                     {
                        int defId = KeyPos.setNames.FirstOrDefault(x => x.Value == setname).Key;
                        KeyPos.setNames[defId] = setname;
                        KeyPos.KeyPositions.RemoveAll(x => x.SetName == setname); //smazat všechny se stejným setName
                     }
                     else
                     {
                        int newId = KeyPos.PossibleFreeIdInDictKeys(KeyPos.setNames);
                        KeyPos.setNames[newId] = setname;
                     }
                     var newPositions = data.KeyPositions.Where(x => x.SetName == setname);
                     KeyPos.KeyPositions.AddRange(newPositions);
                  }

                  ////importovat pouze keypos kde je setname
                  //KeyPos.KeyPositions = data.KeyPositions;

                  MessageBox.Show($"Import dokončen z \"{fileName}\".", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                  OnFileImport.Invoke();
               }
               else //data == null
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
      public static Action? OnFileImport;

      private static string PromptForNewSetName(string oldName)
      {
         return InputBox.Show($"Zadej nový název pro set \"{oldName}\":", "Přejmenovat", $"{oldName}_");
      }

      private static void ChangeSetNamesInImportedKeyPositions(List<KeyPos> importedKeyPositions, string setName, string newSetName)
      {
         foreach (var pos in importedKeyPositions)
         {
            if (pos.SetName == setName)
            {
               pos.SetName = newSetName;
            }
         }
      }

   }
}