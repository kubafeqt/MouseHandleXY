using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY
{
    public static class ImportBox
    {
      public static string ShowJsonFileSelector(string folderPath)
      {
         Form form = new Form();
         Label label = new Label();
         ListBox listBox = new ListBox();
         Button buttonOk = new Button();
         Button buttonCancel = new Button();

         form.Text = "Import JSON souboru";
         label.Text = "Vyber soubor pro import:";

         // Naplní listbox .json soubory
         if (Directory.Exists(folderPath))
         {
            var jsonFiles = Directory.GetFiles(folderPath, "*.json")
                                     .Select(Path.GetFileName)
                                     .ToArray();
            listBox.Items.AddRange(jsonFiles);
         }

         // Výběr = dvojklik nebo Enter → OK
         listBox.DoubleClick += (s, e) =>
         {
            if (listBox.SelectedItem != null)
               form.DialogResult = DialogResult.OK;
         };

         listBox.KeyDown += (s, e) =>
         {
            if (e.KeyCode == Keys.Enter && listBox.SelectedItem != null)
            {
               form.DialogResult = DialogResult.OK;
               form.Close();
            }
         };

         buttonOk.Text = "OK";
         buttonCancel.Text = "Zrušit";

         buttonOk.DialogResult = DialogResult.OK;
         buttonCancel.DialogResult = DialogResult.Cancel;

         // Umístění
         label.SetBounds(10, 10, 300, 20);
         listBox.SetBounds(10, 35, 260, 130);
         buttonOk.SetBounds(60, 180, 75, 23);
         buttonCancel.SetBounds(145, 180, 75, 23);

         label.AutoSize = true;
         form.ClientSize = new Size(280, 220);
         form.Controls.AddRange(new Control[] { label, listBox, buttonOk, buttonCancel });
         form.FormBorderStyle = FormBorderStyle.FixedDialog;
         form.StartPosition = FormStartPosition.CenterScreen;
         form.MinimizeBox = false;
         form.MaximizeBox = false;
         form.AcceptButton = buttonOk;
         form.CancelButton = buttonCancel;

         DialogResult result = form.ShowDialog();

         if (result == DialogResult.OK && listBox.SelectedItem != null)
         {
            return listBox.SelectedItem.ToString();
         }

         return null;
      }

   }
}
