
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
         Button btnPreview = new();

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
         label.SetBounds(12, 10, 260, 20);
         listBox.SetBounds(12, 35, 260, 130);
         btnPreview.SetBounds(20, 180, 75, 23);      // úplně vlevo
         buttonOk.SetBounds(105, 180, 75, 23);        // uprostřed
         buttonCancel.SetBounds(190, 180, 75, 23);   // vpravo

         label.AutoSize = true;
         form.ClientSize = new Size(290, 220);
         form.Controls.AddRange(new Control[] { label, listBox, buttonOk, buttonCancel, btnPreview });
         form.FormBorderStyle = FormBorderStyle.FixedDialog;
         form.StartPosition = FormStartPosition.CenterScreen;
         form.MinimizeBox = false;
         form.MaximizeBox = false;
         form.AcceptButton = buttonOk;
         form.CancelButton = buttonCancel;

         btnPreview.Text = "Preview file";
         btnPreview.Click += (s, e) =>
         {
            if (listBox.SelectedItem != null)
            {
               KeyPos_Preview.selectedFileName = listBox.SelectedItem.ToString();
               ExportImport.InitPreview(listBox.SelectedItem.ToString());
               form.Close();
               return;
            }
            MessageBox.Show("Vyber soubor, který chceš previewnout.", "Vybrat soubor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
         };

      Restart:
         DialogResult result = form.ShowDialog();

         if (result == DialogResult.OK)
         {
            if (listBox.SelectedItem != null)
            {
               return listBox.SelectedItem.ToString();
            }
            MessageBox.Show("Vyber soubor, který chceš importnout.", "Vybrat soubor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            goto Restart;
         }

         return null;
      }

   }
}
