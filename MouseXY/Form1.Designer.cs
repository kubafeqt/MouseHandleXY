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
         nmDelayMs = new NumericUpDown();
         lbDelayMs = new Label();
         lbDelayMsDescription = new Label();
         btnAcceptDelayMs = new Button();
         btnSetKeyPos = new Button();
         btnShowKeysPositions = new Button();
         dgvShowKeysPositions = new DataGridView();
         ((System.ComponentModel.ISupportInitialize)nmDelayMs).BeginInit();
         ((System.ComponentModel.ISupportInitialize)dgvShowKeysPositions).BeginInit();
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
         // nmDelayMs
         // 
         nmDelayMs.Location = new Point(88, 67);
         nmDelayMs.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
         nmDelayMs.Minimum = new decimal(new int[] { 150, 0, 0, 0 });
         nmDelayMs.Name = "nmDelayMs";
         nmDelayMs.Size = new Size(67, 23);
         nmDelayMs.TabIndex = 1;
         nmDelayMs.Value = new decimal(new int[] { 250, 0, 0, 0 });
         // 
         // lbDelayMs
         // 
         lbDelayMs.AutoSize = true;
         lbDelayMs.Location = new Point(25, 69);
         lbDelayMs.Name = "lbDelayMs";
         lbDelayMs.Size = new Size(57, 15);
         lbDelayMs.TabIndex = 2;
         lbDelayMs.Text = "delay ms:";
         // 
         // lbDelayMsDescription
         // 
         lbDelayMsDescription.AutoSize = true;
         lbDelayMsDescription.Font = new Font("Segoe UI Semibold", 10.181818F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbDelayMsDescription.Location = new Point(25, 97);
         lbDelayMsDescription.Name = "lbDelayMsDescription";
         lbDelayMsDescription.Size = new Size(79, 19);
         lbDelayMsDescription.TabIndex = 3;
         lbDelayMsDescription.Text = "description";
         // 
         // btnAcceptDelayMs
         // 
         btnAcceptDelayMs.Location = new Point(161, 67);
         btnAcceptDelayMs.Name = "btnAcceptDelayMs";
         btnAcceptDelayMs.Size = new Size(77, 24);
         btnAcceptDelayMs.TabIndex = 4;
         btnAcceptDelayMs.Text = "accept";
         btnAcceptDelayMs.UseVisualStyleBackColor = true;
         btnAcceptDelayMs.Click += btnAcceptDelayMs_Click;
         // 
         // btnSetKeyPos
         // 
         btnSetKeyPos.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnSetKeyPos.Location = new Point(25, 149);
         btnSetKeyPos.Name = "btnSetKeyPos";
         btnSetKeyPos.Size = new Size(79, 24);
         btnSetKeyPos.TabIndex = 5;
         btnSetKeyPos.Text = "setKeyPos";
         btnSetKeyPos.UseVisualStyleBackColor = true;
         btnSetKeyPos.Click += btnSetKeyPos_Click;
         // 
         // btnShowKeysPositions
         // 
         btnShowKeysPositions.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnShowKeysPositions.Location = new Point(121, 149);
         btnShowKeysPositions.Name = "btnShowKeysPositions";
         btnShowKeysPositions.Size = new Size(143, 24);
         btnShowKeysPositions.TabIndex = 6;
         btnShowKeysPositions.Text = "show keys positions";
         btnShowKeysPositions.UseVisualStyleBackColor = true;
         btnShowKeysPositions.Click += btnShowKeysPositions_Click;
         // 
         // dgvShowKeysPositions
         // 
         dgvShowKeysPositions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         dgvShowKeysPositions.Location = new Point(32, 190);
         dgvShowKeysPositions.Name = "dgvShowKeysPositions";
         dgvShowKeysPositions.Size = new Size(791, 441);
         dgvShowKeysPositions.TabIndex = 7;
         dgvShowKeysPositions.Visible = false;
         // 
         // Form1
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(854, 656);
         Controls.Add(dgvShowKeysPositions);
         Controls.Add(btnShowKeysPositions);
         Controls.Add(btnSetKeyPos);
         Controls.Add(btnAcceptDelayMs);
         Controls.Add(lbDelayMsDescription);
         Controls.Add(lbDelayMs);
         Controls.Add(nmDelayMs);
         Controls.Add(cboxOnStartup);
         FormBorderStyle = FormBorderStyle.FixedSingle;
         MaximizeBox = false;
         Name = "Form1";
         Text = "MouseControl";
         Load += Form1_Load;
         ((System.ComponentModel.ISupportInitialize)nmDelayMs).EndInit();
         ((System.ComponentModel.ISupportInitialize)dgvShowKeysPositions).EndInit();
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private CheckBox cboxOnStartup;
      private NumericUpDown nmDelayMs;
      private Label lbDelayMs;
      private Label lbDelayMsDescription;
      private Button btnAcceptDelayMs;
      private Button btnSetKeyPos;
      private Button btnShowKeysPositions;
      private DataGridView dgvShowKeysPositions;
   }
}
