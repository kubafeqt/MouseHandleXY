using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MouseXY
{
   public static class ExportImport
   {
      private static string exportFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "exports");

      public static void ExportToJson()
      {
         var data = new ExpImpDataContainer
         {
            KeyPositionsList = KeyPos.KeyPositionsList.ToList(),
            SetNamesDict = KeyPos.SetNamesDict.ToDictionary()
         };

         var options = new JsonSerializerOptions { WriteIndented = true };
         string json = JsonSerializer.Serialize(data, options);


         if (!Directory.Exists(exportFolder))
         {
            Directory.CreateDirectory(exportFolder);
         }

         string fileName = ExportBox.ShowJsonFileSelector(exportFolder);
         if (!string.IsNullOrWhiteSpace(Path.GetFileNameWithoutExtension(fileName)))
         {
            string finalPath = Path.Combine(exportFolder, fileName);
            File.WriteAllText(finalPath, json);
            MessageBox.Show($"Data byla úspěšně uložena pod názvem {fileName} .", "Uloženo", MessageBoxButtons.OK, MessageBoxIcon.Information);
         }
      }

      public static Action? OnFileImport;
      public static bool import = false; //for KeyPos constructor - do not add to KeyPositions list
      public static void ImportFromJson()
      {
         string fileName = ImportBox.ShowJsonFileSelector(exportFolder);
         ImportFromJson(fileName);
      }

      public static void ImportFromJson(string fileName)
      {
         if (!string.IsNullOrWhiteSpace(fileName))
         {
            string fullPath = Path.Combine(exportFolder, fileName);
            try
            {
               import = true; //do not add to KeyPos.KeyPosition list
               string json = File.ReadAllText(fullPath);
               var data = JsonSerializer.Deserialize<ExpImpDataContainer>(json);
               import = false;

               if (data != null && data.SetNamesDict != null && data.KeyPositionsList != null)
               {
                  var importedSetNamesDict = data.SetNamesDict;
                  var importedKeyPositions = data.KeyPositionsList;

                  //var prepImportedSetNames = importedSetNamesDict.ToDictionary();
                  //var prepImportedKeyPositions = importedKeyPositions.ToList();

                  importedSetNamesDict.Add(0, "default");
                  importedSetNamesDict = importedSetNamesDict.OrderBy(x => x.Key).ToDictionary();

                  //oveření importovaných setNames - příprava dat
                  foreach (var kvp in importedSetNamesDict.ToDictionary())
                  {
                     string setName = kvp.Value;
                     string defSetName = setName;

                     if (KeyPos.SetNamesDict.ContainsValue(setName) || setName == "default") //kolize - aktuální (KeyPos) setName již existuje
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
                           importedSetNamesDict[kvp.Key] = setName; //přiřazení nového setName
                           if (setName != kvp.Value || importedKeyPositions.Any(x => x.SetName == setName)) //setName je jiný (nový)
                           {
                              ChangeSetNamesInImportedKeyPositions(importedKeyPositions, kvp.Value, setName); //přepsat všechny na tento setName
                                                                                                              //prepImportedKeyPositions.RemoveAll(x => x.SetName == setName); //smazat všechny value se stejným setName
                           }
                           continue; //přepsat, ponecháme
                        }
                        else if (result == DialogResult.No) //přejmenovat
                        {
                           string prevMsg = string.Empty;
                        PromptForNewSetName:
                           string? newSetName = PromptForNewSetName(setName, prevMsg);
                           if (!string.IsNullOrWhiteSpace(newSetName) && (!importedSetNamesDict.ContainsValue(newSetName) || newSetName == defSetName))
                           {
                              if (KeyPos.SetNamesDict.ContainsValue(newSetName))
                              {
                                 setName = newSetName;
                                 goto setNameExist;
                              }
                              importedSetNamesDict[kvp.Key] = newSetName;
                              if (newSetName != kvp.Value) //setName je jiný (nový)
                              {
                                 ChangeSetNamesInImportedKeyPositions(importedKeyPositions, kvp.Value, newSetName); //přepsat všechny na tento setName  
                              }
                           }
                           else //zadán prázdný název nebo již obsažený v importu
                           {
                              if (newSetName == null) //zrušeno
                              {
                                 importedSetNamesDict.Remove(kvp.Key);
                                 continue; //přeskočit
                              }
                              prevMsg = string.IsNullOrWhiteSpace(newSetName) ? "Zadán prázdný název, zkus to znovu.\n" : $"{newSetName} je již obsažený v importu.\n";
                              goto PromptForNewSetName; //znovu vyzvat k zadání
                           }
                        }
                        else if (result == DialogResult.Cancel) //přeskočit
                        {
                           importedSetNamesDict.Remove(kvp.Key);
                        }
                     }
                  }

                  //import setnames a keypositons - přepsání dat
                  foreach (var kvp in importedSetNamesDict.ToDictionary())
                  {
                     string setname = kvp.Value;
                     if (KeyPos.SetNamesDict.ContainsValue(setname)) //setname je obsažen
                     {
                        int defId = KeyPos.SetNamesDict.FirstOrDefault(x => x.Value == setname).Key;
                        KeyPos.SetNamesDict[defId] = setname;
                     }
                     else if (setname != "default")
                     {
                        int newId = KeyPos.PossibleFreeIdInDictKeys(KeyPos.SetNamesDict);
                        KeyPos.SetNamesDict[newId] = setname;
                     }
                     KeyPos.KeyPositionsList.RemoveAll(x => x.SetName == setname); //smazat všechny se stejným setName
                     var newPositions = importedKeyPositions.Where(x => x.SetName == setname);
                     KeyPos.KeyPositionsList.AddRange(newPositions);
                  }

                  MessageBox.Show($"Import dokončen z \"{fileName}\".", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  OnFileImport.Invoke();
               }
               else //data == null || data.SetNamesDict == null || data.KeyPositionsList == null
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

      private static string PromptForNewSetName(string oldName, string prevMsg)
      {
         return InputBox.Show($"{prevMsg}Zadej nový název pro set \"{oldName}\":", "Přejmenovat", nullable: true);
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

      private static void ChangeSetNamesInImportedKeyPositions(List<KeyPos_Preview> importedKeyPositions, string setName, string newSetName)
      {
         foreach (var pos in importedKeyPositions)
         {
            if (pos.SetName == setName)
            {
               pos.SetName = newSetName;
            }
         }
      }


      //test it
      private static void ChangeSetNamesInImportedKeyPositions(List<KeyPos> importedKeyPositions, List<KeyPos> prepImportedKeyPositions, string setName, string newSetName)
      {
         foreach (var pos in importedKeyPositions)
         {

            var setname = prepImportedKeyPositions.Where(x => pos.SetName == x.SetName).ToString();
            if (setname == setName)
            {
               setname = newSetName;
            }

         }
      }

      public static Action? OnPreview;
      public static void InitPreview(string fileName)
      {
         if (!string.IsNullOrWhiteSpace(fileName))
         {
            string fullPath = Path.Combine(exportFolder, fileName);

            try
            {
               KeyPos_Preview.ClearData();
               string json = File.ReadAllText(fullPath);
               var data = JsonSerializer.Deserialize<PreviewDataContainer>(json);

               var importedSetNamesDict = data.SetNamesDict;
               importedSetNamesDict.Add(0, "default");
               importedSetNamesDict = importedSetNamesDict.OrderBy(x => x.Key).ToDictionary();
               KeyPos_Preview.SetNamesDict = importedSetNamesDict;
               KeyPos_Preview.showedSetName = "default"; //pro zobrazení v UI, aby se neukazoval default setName

               OnPreview.Invoke();
            }
            catch (Exception ex)
            {
               MessageBox.Show("Chyba při načítání souboru:\n" + ex.Message, "Chyba importu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }
      }

      public static void ImportSet()
      {
         string setName = KeyPos_Preview.showedSetName;
      GoAgain:
         if (KeyPos.SetNamesDict.ContainsValue(setName) || setName == "default") //kolize - aktuální (KeyPos) setName již existuje
         {
            DialogResult result = MessageBox.Show(
                $"SetName \"{setName}\" už existuje.\nChceš ho přepsat?\n\nAno = přepsat\nNe = přejmenovat\nZrušit = přeskočit",
                "Kolize názvu",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes) //přepsat
            {
               KeyPos.KeyPositionsList.RemoveAll(x => x.SetName == setName); //smazat všechny se stejným setName
               ImportSet(setName);
            }
            else if (result == DialogResult.No) //přejmenovat
            {
               string prevMsg = string.Empty;
            PromptForNewSetName:
               string? newSetName = PromptForNewSetName(setName, prevMsg);
               if (!string.IsNullOrWhiteSpace(newSetName) && !KeyPos.SetNamesDict.ContainsValue(newSetName) && newSetName != "default")
               {
                  KeyPos.SetNamesDict.Add(KeyPos.PossibleFreeIdInDictKeys(KeyPos.SetNamesDict), newSetName);
                  ImportSet(newSetName);
               }
               else
               {
                  if (newSetName == null) //zrušeno
                  {
                     return; //přeskočit
                  }
                  else if (string.IsNullOrWhiteSpace(newSetName))
                  {
                     prevMsg = "Zadán prázdný název, zkus to znovu.\n";
                     goto PromptForNewSetName; //znovu vyzvat k zadání
                  }
                  else
                  {
                     setName = newSetName;
                     goto GoAgain; //znovu k dialogovému oknu
                  }
               }
            }
         }
         else //setname je free
         {
            KeyPos.SetNamesDict.Add(KeyPos.PossibleFreeIdInDictKeys(KeyPos.SetNamesDict), setName);
            ImportSet(setName);
         }
      }

      private static void ImportSet(string setName)
      {
         if (setName != KeyPos_Preview.showedSetName) //změnit název setName u všech Keypos, pokud je přejmenovaný
         {
            ChangeSetNamesInImportedKeyPositions(KeyPos_Preview.KeyPositionsList, KeyPos_Preview.showedSetName, setName);
         }
         if (setName == KeyPos.selectedSetName)
         {
            KeyPos.UpdateKeyPosDict(); //pokud je setName stejný jako aktuálně vybraný, aktualizovat KeyPos.SetNamesDict
         }
         KeyPos_Preview.KeyPositionsList.Where(preview => preview.SetName == setName).ToList().ForEach(preview => KeyPos.FromPreview(preview));
         OnFileImport?.Invoke();
         MessageBox.Show($"Set \"{setName}\" byl úspěšně importován.", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
   }
}