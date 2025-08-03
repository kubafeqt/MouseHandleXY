using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY
{
   public static class InputBox
   {
      public static string Show(string prompt, string title, string defaultValue = "", bool nullable = false)
      {
         Form form = new Form();
         Label label = new Label();
         TextBox textBox = new TextBox();
         Button buttonOk = new Button();
         Button buttonCancel = new Button();

         form.Text = title;
         label.Text = prompt;
         textBox.Text = defaultValue;
         textBox.MaxLength = 16; // Limit input to 16 characters

         buttonOk.Text = "OK";
         buttonCancel.Text = "Zrušit";

         buttonOk.DialogResult = DialogResult.OK;
         buttonCancel.DialogResult = DialogResult.Cancel;

         int marginLeft = 11;
         label.SetBounds(9, 20, 372, 13);
         textBox.SetBounds(marginLeft + 12, 50, 160, 26);
         buttonOk.SetBounds(marginLeft + 16, 80, 75, 23);
         buttonCancel.SetBounds(marginLeft + 97, 80, 75, 23);

         label.AutoSize = true;
         form.ClientSize = new Size(214, 123);
         form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
         //form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
         form.FormBorderStyle = FormBorderStyle.FixedDialog;
         form.StartPosition = FormStartPosition.CenterScreen;
         form.MinimizeBox = false;
         form.MaximizeBox = false;
         form.AcceptButton = buttonOk;
         form.CancelButton = buttonCancel;

         DialogResult dialogResult = form.ShowDialog();
         if (nullable && dialogResult == DialogResult.Cancel)
         {
            return null;
         }
         return dialogResult == DialogResult.OK ? textBox.Text : string.Empty;
      }
   }
}
