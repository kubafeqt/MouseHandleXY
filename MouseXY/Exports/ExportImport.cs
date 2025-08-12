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
                  var importedSetNamesDict = data.SetNamesDict;
                  var importedKeyPositions = data.KeyPositionsList;

                  var prepImportedSetNames = importedSetNamesDict.ToDictionary();
                  var prepImportedKeyPositions = importedKeyPositions.ToList();

                  importedSetNamesDict.Add(0, "default");
                  importedSetNamesDict = importedSetNamesDict.OrderBy(x => x.Key).ToDictionary();

                  //oveření importovaných setNames - příprava dat
                  foreach (var kvp in importedSetNamesDict.ToDictionary())
                  {
                     string setName = kvp.Value;
                     //string importedSetName = string.Empty;

                     if (KeyPos.SetNamesDict.ContainsValue(setName) || setName == "default") //kolize - aktuální (KeyPos) setName již existuje
                     {

                     setNameExist:
                        DialogResult result = MessageBox.Show(
                            $"SetName \"{setName}\" už existuje.\nChceš ho přepsat?\n\nAno = přepsat\nNe = přejmenovat\nZrušit = přeskočit",
                            "Kolize názvu",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question
                        );
                        //importedSetName = string.Empty;

                        //potřebuju ->
                        //pokud zadá nové jméno, tak se to savne pod novým názvem
                        //
                        //aby dal z importedKeyPositions na prepImportedKeyPositions
                        //
                        //

                        if (result == DialogResult.Yes)
                        {
                           prepImportedSetNames[kvp.Key] = setName; //přiřazení nového setName
                           if (setName != kvp.Value || prepImportedKeyPositions.Any(x => x.SetName == setName)) //setName je jiný (nový)
                           { 
                              ChangeSetNamesInImportedKeyPositions(prepImportedKeyPositions, kvp.Value, setName); //přepsat všechny na tento setName
                              prepImportedKeyPositions.RemoveAll(x => x.SetName == setName); //smazat všechny value se stejným setName
                           }
                           continue; //přepsat, ponecháme
                        }
                        else if (result == DialogResult.No) //přejmenovat
                        {
                           string newSetName = PromptForNewSetName(setName);
                           if (!string.IsNullOrEmpty(newSetName))
                           {
                              if (KeyPos.SetNamesDict.ContainsValue(newSetName))
                              {
                                 setName = newSetName;
                                 goto setNameExist;
                              }
                              prepImportedSetNames[kvp.Key] = newSetName;
                              if (newSetName != kvp.Value) //setName je jiný (nový)
                              {

                                 prepImportedKeyPositions.RemoveAll(x => x.SetName == setName); //smazat všechny value se stejným setName      
                                 ChangeSetNamesInImportedKeyPositions(prepImportedKeyPositions, kvp.Value, setName); //přepsat všechny na tento setName
                                     
                              }
                           }
                           else //zadán prázdný název
                           {
                              prepImportedSetNames.Remove(kvp.Key); //zrušeno
                           }
                        }
                        else if (result == DialogResult.Cancel) //přeskočit
                        {
                           prepImportedSetNames.Remove(kvp.Key);
                        }
                     }
                  }

                  //import setnames a keypositons - přepsání dat
                  foreach (var kvp in prepImportedSetNames.ToDictionary())
                  {
                     string setname = kvp.Value;
                     if (KeyPos.SetNamesDict.ContainsValue(setname)) //setname je obsažen
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

      private static void ChangeSetNamesInImportedKeyPositions(List<KeyPos> importedKeyPositions, List<KeyPos> prepImportedKeyPositions, string setName, string newSetName)
      {
         foreach (var pos in importedKeyPositions)
         {
            
            var setname = prepImportedKeyPositions.Where(x => pos.SetName == x.SetName).ToString();
            if (setname == setName)
            {
               setname = newSetName;
            }

            //if (pos.SetName == setName)
            //{
            //   pos.SetName = newSetName;
            //}

         }
      }

   }
}