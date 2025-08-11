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
         if (!string.IsNullOrWhiteSpace(fileName))
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
                  var importedSetNames = data.SetNamesDict;
                  var importedKeyPositions = data.KeyPositionsList;

                  var prepImportedSetNames = importedSetNames.ToDictionary();
                  var prepImportedKeyPositions = importedKeyPositions.ToList();

                  prepImportedSetNames.Add(0, "default");
                  prepImportedSetNames = prepImportedSetNames.OrderBy(x => x.Key).ToDictionary();

                  //oveření importovaných setNames - příprava dat
                  foreach (var kvp in prepImportedSetNames.ToDictionary())
                  {
                     string setName = kvp.Value;
                     string importedSetName = string.Empty;

                     if (KeyPos.SetNamesDict.ContainsValue(setName) || setName == "default") //kolize - aktuální (KeyPos) setName již existuje
                     {

                     setNameExist:
                        DialogResult result = MessageBox.Show(
                            $"SetName \"{setName}\" už existuje {importedSetName}.\nChceš ho přepsat?\n\nAno = přepsat\nNe = přejmenovat\nZrušit = přeskočit",
                            "Kolize názvu",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question
                        );
                        importedSetName = string.Empty;

                        if (result == DialogResult.Yes) //potvrzení přepsání
                        {
                           prepImportedSetNames[kvp.Key] = setName;
                           if (setName != kvp.Value)
                           {
                              ChangeSetNamesInImportedKeyPositions(prepImportedKeyPositions, kvp.Value, setName);
                           }
                           continue; //přepsat = ponecháme
                        }
                        else if (result == DialogResult.No) //přejmenovat
                        {
                           string newSetName = PromptForNewSetName(setName);
                           if (!string.IsNullOrEmpty(newSetName))
                           {
                              if (prepImportedSetNames.ContainsValue(newSetName) && newSetName != kvp.Value)
                              {
                                 setName = newSetName;
                                 importedSetName = "v importovaných setnames";
                                 goto setNameExist;
                              }
                              else if (KeyPos.SetNamesDict.ContainsValue(newSetName))
                              {
                                 setName = newSetName;
                                 goto setNameExist;
                              }
                              prepImportedSetNames[kvp.Key] = newSetName;
                              ChangeSetNamesInImportedKeyPositions(prepImportedKeyPositions, kvp.Value, newSetName);
                           }
                           else //zadán prázdný název
                           {
                              prepImportedSetNames.Remove(kvp.Key); // zrušeno
                           }
                        }
                        else if (result == DialogResult.Cancel)
                        {
                           prepImportedSetNames.Remove(kvp.Key); // Přeskočit
                        }
                     }
                  }

                  //import setnames a keypositons - přepsání dat
                  foreach (var kvp in prepImportedSetNames.ToDictionary())
                  {
                     string setname = kvp.Value;
                     if (KeyPos.SetNamesDict.ContainsValue(setname))
                     {
                        int defId = KeyPos.SetNamesDict.FirstOrDefault(x => x.Value == setname).Key;
                        KeyPos.SetNamesDict[defId] = setname;
                        KeyPos.KeyPositionsList.RemoveAll(x => x.SetName == setname); //smazat všechny se stejným setName
                     }
                     else if (setname != "default")
                     {
                        int newId = KeyPos.PossibleFreeIdInDictKeys(KeyPos.SetNamesDict);
                        KeyPos.SetNamesDict[newId] = setname;
                     }
                     var newPositions = importedKeyPositions.Where(x => x.SetName == setname);
                     KeyPos.KeyPositionsList.AddRange(newPositions);
                  }



                  //PrepDefaultSetName(ref prepImportedSetNames, ref prepImportedKeyPositions, importedSetNames);

                  //   //default setname:
                  //   string defSetName = "default";

                  //newSetNameExist:
                  //   string msg = $"Chceš přepsat \"{defSetName}\" setname?\nChceš ho přepsat?\n\nAno = přepsat\nNe = přejmenovat\nZrušit = nepřepisovat";
                  //   DialogResult defResult = MessageBox.Show(
                  //            msg,
                  //            "Kolize názvu",
                  //            MessageBoxButtons.YesNoCancel,
                  //            MessageBoxIcon.Question
                  //        );


                  //   if (defResult == DialogResult.Yes) //potvrzení přepsání
                  //   {
                  //      if (defSetName != "default")
                  //      {
                  //         if (!prepImportedSetNames.Any(x => x.Value == defSetName)) //přidat defSetName do prepImportedSetNames, pokud tam není
                  //         {
                  //            prepImportedSetNames.Add(KeyPos.PossibleFreeIdInDictKeys(prepImportedSetNames), defSetName);
                  //         }
                  //         else //smazat všechny se stejným setName v imported, pokud defSetName již existuje
                  //         {
                  //            prepImportedKeyPositions.RemoveAll(x => x.SetName == defSetName);
                  //         }
                  //         ChangeSetNamesInImportedKeyPositions(prepImportedKeyPositions, "default", defSetName);
                  //      }
                  //      else //pokud je setName default, tak to zpracuj do KeyPos.KeyPositionList
                  //      {
                  //         KeyPos.KeyPositionsList.RemoveAll(x => x.SetName == defSetName); //smazat všechny se stejným setName v aktual
                  //         var newPositions = prepImportedKeyPositions.Where(x => x.SetName == defSetName);
                  //         KeyPos.KeyPositionsList.AddRange(newPositions);
                  //      }
                  //   }
                  //   else if (defResult == DialogResult.No) //rename imported default setname
                  //   {
                  //      string newSetName = PromptForNewSetName("default");
                  //      if (!string.IsNullOrEmpty(newSetName))
                  //      {
                  //         if (KeyPos.SetNamesDict.ContainsValue(newSetName))
                  //         {
                  //            defSetName = newSetName;
                  //            goto newSetNameExist;
                  //         }
                  //         prepImportedSetNames.Add(KeyPos.PossibleFreeIdInDictKeys(importedSetNames), newSetName);
                  //         prepImportedKeyPositions.RemoveAll(x => x.SetName == newSetName);
                  //         ChangeSetNamesInImportedKeyPositions(prepImportedKeyPositions, defSetName, newSetName);
                  //      }
                  //      else
                  //      {
                  //         goto newSetNameExist;
                  //      }
                  //   }

                  ////oveření importovaných setNames
                  //foreach (var kvp in importedSetNames.ToDictionary())
                  //{
                  //   string setName = kvp.Value;

                  //   if (KeyPos.SetNamesDict.ContainsValue(setName)) //kolize - aktuální (KeyPos) setName již existuje
                  //   {

                  //   setNameExist:
                  //      DialogResult result = MessageBox.Show(
                  //          $"SetName \"{setName}\" už existuje.\nChceš ho přepsat?\n\nAno = přepsat\nNe = přejmenovat\nZrušit = přeskočit",
                  //          "Kolize názvu",
                  //          MessageBoxButtons.YesNoCancel,
                  //          MessageBoxIcon.Question
                  //      );

                  //      if (result == DialogResult.Yes) //potvrzení přepsání
                  //      {
                  //         importedSetNames[kvp.Key] = setName;
                  //         if (setName != kvp.Value)
                  //         {
                  //            ChangeSetNamesInImportedKeyPositions(importedKeyPositions, kvp.Value, setName);
                  //         }
                  //         continue; // Přepsat = ponecháme
                  //      }
                  //      else if (result == DialogResult.No)
                  //      {
                  //         // Přejmenovat
                  //         string newSetName = PromptForNewSetName(setName);
                  //         if (!string.IsNullOrEmpty(newSetName))
                  //         {
                  //            if (KeyPos.SetNamesDict.ContainsValue(newSetName))
                  //            {
                  //               setName = newSetName;
                  //               goto setNameExist;
                  //            }
                  //            importedSetNames[kvp.Key] = newSetName;
                  //            ChangeSetNamesInImportedKeyPositions(importedKeyPositions, kvp.Value, newSetName);
                  //         }
                  //         else //zadán prázdný název
                  //         {
                  //            //goto setNameExist;
                  //            prepImportedSetNames.Remove(kvp.Key); // zrušeno
                  //         }
                  //      }
                  //      else if (result == DialogResult.Cancel)
                  //      {
                  //         prepImportedSetNames.Remove(kvp.Key); // Přeskočit
                  //      }
                  //   }
                  //}

                  ////pokud setname existuje -> zachovej ID, pokud setname neexistuje -> přidej nový setname s novým ID
                  //// Import setnames a keypositons pouze, kde je setname
                  //foreach (var kvp in prepImportedSetNames.ToDictionary())
                  //{
                  //   string setname = kvp.Value;
                  //   if (KeyPos.SetNamesDict.ContainsValue(setname))
                  //   {
                  //      int defId = KeyPos.SetNamesDict.FirstOrDefault(x => x.Value == setname).Key;
                  //      KeyPos.SetNamesDict[defId] = setname;
                  //      KeyPos.KeyPositionsList.RemoveAll(x => x.SetName == setname); //smazat všechny se stejným setName
                  //   }
                  //   else
                  //   {
                  //      int newId = KeyPos.PossibleFreeIdInDictKeys(KeyPos.SetNamesDict);
                  //      KeyPos.SetNamesDict[newId] = setname;
                  //   }
                  //   var newPositions = importedKeyPositions.Where(x => x.SetName == setname);
                  //   KeyPos.KeyPositionsList.AddRange(newPositions);
                  //}

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
      
      private static void PrepDefaultSetName(ref Dictionary<int, string> prepImportedSetNames, ref List<KeyPos> prepImportedKeyPositions, Dictionary<int, string> importedSetNames)
      {
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


         if (defResult == DialogResult.Yes) //potvrzení přepsání
         {
            if (defSetName != "default")
            {
               if (!prepImportedSetNames.Any(x => x.Value == defSetName)) //přidat defSetName do prepImportedSetNames, pokud tam není
               {
                  prepImportedSetNames.Add(KeyPos.PossibleFreeIdInDictKeys(prepImportedSetNames), defSetName);
               }
               else //smazat všechny se stejným setName v imported, pokud defSetName již existuje
               {
                  prepImportedKeyPositions.RemoveAll(x => x.SetName == defSetName);
               }
               ChangeSetNamesInImportedKeyPositions(prepImportedKeyPositions, "default", defSetName);
            }
            else //pokud je setName default, tak to zpracuj do KeyPos.KeyPositionList
            {
               KeyPos.KeyPositionsList.RemoveAll(x => x.SetName == defSetName); //smazat všechny se stejným setName v aktual
               var newPositions = prepImportedKeyPositions.Where(x => x.SetName == defSetName);
               KeyPos.KeyPositionsList.AddRange(newPositions);
            }
         }
         else if (defResult == DialogResult.No) //rename imported default setname
         {
            string newSetName = PromptForNewSetName("default");
            if (!string.IsNullOrEmpty(newSetName))
            {
               if (KeyPos.SetNamesDict.ContainsValue(newSetName))
               {
                  defSetName = newSetName;
                  goto newSetNameExist;
               }
               prepImportedSetNames.Add(KeyPos.PossibleFreeIdInDictKeys(importedSetNames), newSetName);
               prepImportedKeyPositions.RemoveAll(x => x.SetName == newSetName);
               ChangeSetNamesInImportedKeyPositions(prepImportedKeyPositions, defSetName, newSetName);
            }
            else
            {
               goto newSetNameExist;
            }
         }
      }

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