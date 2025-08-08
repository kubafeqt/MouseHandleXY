using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY
{
    class ExportBox
    {
      public static string ShowJsonFileSelector(string folderPath, string defaultValue = "", bool nullable = false)
      {
         Form form = new Form();
         Label label = new Label();
         ListBox listBox = new ListBox();
         TextBox textBox = new TextBox();
         Button buttonOk = new Button();
         Button buttonCancel = new Button();
         Button buttonRename = new Button();
         Button buttonDelete = new Button();

         form.Text = "Vybrat název pro uložení.";
         label.Text = "Uložený názvy:";

         

         // Naplní listbox .json soubory
         if (Directory.Exists(folderPath))
         {
            var jsonFiles = Directory.GetFiles(folderPath, "*.json")
                                     .Select(Path.GetFileName)
                                     .ToArray();
            listBox.Items.AddRange(jsonFiles);
         }

         textBox.Text = defaultValue;
         textBox.MaxLength = 16;

         buttonOk.Text = "OK";
         buttonCancel.Text = "Zrušit";

         buttonOk.DialogResult = DialogResult.OK;
         buttonCancel.DialogResult = DialogResult.Cancel;

         // Umístění
         label.SetBounds(10, 10, 300, 20);
         listBox.SetBounds(10, 35, 260, 100);
         buttonDelete.SetBounds(10, 140, 125, 25);
         buttonRename.SetBounds(145, 140, 125, 25);
         textBox.SetBounds(10, 170, 260, 25);
         buttonOk.SetBounds(60, 205, 75, 23);
         buttonCancel.SetBounds(145, 205, 75, 23);



         label.AutoSize = true;
         form.ClientSize = new Size(280, 240);

         form.Controls.AddRange(new Control[] { label, listBox, buttonDelete, buttonRename, textBox, buttonOk, buttonCancel });
         form.FormBorderStyle = FormBorderStyle.FixedDialog;
         form.StartPosition = FormStartPosition.CenterScreen;
         form.MinimizeBox = false;
         form.MaximizeBox = false;
         form.AcceptButton = buttonOk;
         form.CancelButton = buttonCancel;


         void RefreshListBox()
         {
            listBox.Items.Clear();
            if (Directory.Exists(folderPath))
            {
               var jsonFiles = Directory.GetFiles(folderPath, "*.json")
                                        .Select(Path.GetFileName)
                                        .ToArray();
               listBox.Items.AddRange(jsonFiles);
            }
         }

         // Událost při kliknutí na položku – vyplní TextBox
         listBox.SelectedIndexChanged += (sender, e) =>
         {
            if (listBox.SelectedItem != null && (string.IsNullOrWhiteSpace(textBox.Text) || listBox.Items.Contains(textBox.Text) || listBox.Items.Contains($"{textBox.Text}.json")))
            {
               textBox.Text = Path.GetFileNameWithoutExtension(listBox.SelectedItem.ToString());
            }
         };

         // RENAME tlačítko - přejmenuje soubor
         buttonRename.Text = "Přejmenovat";
         buttonRename.Click += (sender, e) =>
         {
            if (listBox.SelectedItem != null)
            {
               string oldFile = listBox.SelectedItem.ToString();
               string oldPath = Path.Combine(folderPath, oldFile);

               string oldNameWithoutExt = Path.GetFileNameWithoutExtension(oldFile);

               string newName = InputBox.Show(
                   "Zadej nový název (bez přípony .json):",
                   "Přejmenovat soubor",
                   oldNameWithoutExt);

               if (!string.IsNullOrWhiteSpace(newName))
               {
                  string newFile = newName.Trim() + ".json";
                  string newPath = Path.Combine(folderPath, newFile);

                  if (File.Exists(newPath))
                  {
                     MessageBox.Show($"Soubor s názvem už {newFile} existuje!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                  }

                  try
                  {
                     File.Move(oldPath, newPath);
                     RefreshListBox();
                     textBox.Text = newFile;
                  }
                  catch (Exception ex)
                  {
                     MessageBox.Show("Chyba při přejmenování:\n" + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
               }
            }
         };

         // DELETE tlačítko – smaže označený soubor
         buttonDelete.Text = "Smazat";
         buttonDelete.Click += (sender, e) =>
         {
            if (listBox.SelectedItem != null)
            {
               string selectedFile = listBox.SelectedItem.ToString();
               string fullPath = Path.Combine(folderPath, selectedFile);

               var result = MessageBox.Show($"Opravdu chceš smazat soubor \"{selectedFile}\"?",
                   "Smazat soubor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

               if (result == DialogResult.Yes)
               {
                  try
                  {
                     File.Delete(fullPath);
                     RefreshListBox();
                     textBox.Clear();
                  }
                  catch (Exception ex)
                  {
                     MessageBox.Show("Chyba při mazání souboru:\n" + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
               }
            }
         };

         DialogResult dialogResult = form.ShowDialog();

         if (nullable && dialogResult == DialogResult.Cancel)
         {
            return null;
         }

         string selectedName = Path.GetFileNameWithoutExtension(textBox.Text.Trim());
         string selectedFile = selectedName + ".json";

         // Porovnání bez přípony
         bool fileExists = Directory.GetFiles(folderPath, "*.json")
             .Select(f => Path.GetFileNameWithoutExtension(f))
             .Any(f => string.Equals(f, selectedName, StringComparison.OrdinalIgnoreCase));

         if (dialogResult == DialogResult.OK)
         {
            //string fullPath = Path.Combine(folderPath, selectedFile);

            if (fileExists)
            {
               var result = MessageBox.Show($"Soubor \"{selectedFile}\" už existuje.\nChceš ho přepsat?", "Přepsat soubor?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
               if (result == DialogResult.No)
               {
                  return string.Empty;
               }
            }

            return selectedFile;
         }

         return string.Empty;
      }
   }
}
