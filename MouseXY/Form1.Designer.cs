namespace MouseXY
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cboxOnStartup = new CheckBox();
            SuspendLayout();
            // 
            // cboxOnStartup
            // 
            cboxOnStartup.AutoSize = true;
            cboxOnStartup.Location = new Point(25, 25);
            cboxOnStartup.Name = "cboxOnStartup";
            cboxOnStartup.Size = new Size(130, 19);
            cboxOnStartup.TabIndex = 0;
            cboxOnStartup.Text = "on windows startup";
            cboxOnStartup.UseVisualStyleBackColor = true;
            cboxOnStartup.CheckedChanged += cboxOnStartup_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 294);
            Controls.Add(cboxOnStartup);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "MouseControl";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox cboxOnStartup;
    }
}
