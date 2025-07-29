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
         lbSetKeyPos = new Label();
         cboxShowSetKeyPos = new CheckBox();
         btnDeleteKeyPosition = new Button();
         btnEditPosition = new Button();
         lbKeyPos = new Label();
         tbPosX = new TextBox();
         tbPosY = new TextBox();
         lbPosX = new Label();
         lbPosY = new Label();
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
         dgvShowKeysPositions.MultiSelect = false;
         dgvShowKeysPositions.Name = "dgvShowKeysPositions";
         dgvShowKeysPositions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
         dgvShowKeysPositions.Size = new Size(791, 441);
         dgvShowKeysPositions.TabIndex = 7;
         dgvShowKeysPositions.Visible = false;
         dgvShowKeysPositions.SelectionChanged += dgvShowKeysPositions_SelectionChanged;
         // 
         // lbSetKeyPos
         // 
         lbSetKeyPos.AutoSize = true;
         lbSetKeyPos.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbSetKeyPos.Location = new Point(270, 152);
         lbSetKeyPos.Name = "lbSetKeyPos";
         lbSetKeyPos.Size = new Size(155, 17);
         lbSetKeyPos.TabIndex = 8;
         lbSetKeyPos.Text = "setKeyPos open - X: , Y:";
         lbSetKeyPos.Visible = false;
         // 
         // cboxShowSetKeyPos
         // 
         cboxShowSetKeyPos.AutoSize = true;
         cboxShowSetKeyPos.Checked = true;
         cboxShowSetKeyPos.CheckState = CheckState.Checked;
         cboxShowSetKeyPos.Location = new Point(161, 25);
         cboxShowSetKeyPos.Name = "cboxShowSetKeyPos";
         cboxShowSetKeyPos.Size = new Size(214, 19);
         cboxShowSetKeyPos.TabIndex = 9;
         cboxShowSetKeyPos.Text = "show keys positions after setKeyPos";
         cboxShowSetKeyPos.UseVisualStyleBackColor = true;
         cboxShowSetKeyPos.CheckedChanged += cboxShowSetKeyPos_CheckedChanged;
         // 
         // btnDeleteKeyPosition
         // 
         btnDeleteKeyPosition.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnDeleteKeyPosition.Location = new Point(428, 149);
         btnDeleteKeyPosition.Name = "btnDeleteKeyPosition";
         btnDeleteKeyPosition.Size = new Size(143, 24);
         btnDeleteKeyPosition.TabIndex = 10;
         btnDeleteKeyPosition.Tag = "EditPos";
         btnDeleteKeyPosition.Text = "delete key position";
         btnDeleteKeyPosition.UseVisualStyleBackColor = true;
         btnDeleteKeyPosition.Visible = false;
         btnDeleteKeyPosition.Click += btnDeleteKeyPosition_Click;
         // 
         // btnEditPosition
         // 
         btnEditPosition.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnEditPosition.Location = new Point(749, 148);
         btnEditPosition.Name = "btnEditPosition";
         btnEditPosition.Size = new Size(75, 25);
         btnEditPosition.TabIndex = 11;
         btnEditPosition.Tag = "EditPos";
         btnEditPosition.Text = "Edit";
         btnEditPosition.UseVisualStyleBackColor = true;
         btnEditPosition.Click += btnEditPosition_Click;
         // 
         // lbKeyPos
         // 
         lbKeyPos.AutoSize = true;
         lbKeyPos.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbKeyPos.Location = new Point(591, 152);
         lbKeyPos.Name = "lbKeyPos";
         lbKeyPos.Size = new Size(34, 17);
         lbKeyPos.TabIndex = 12;
         lbKeyPos.Tag = "EditPos";
         lbKeyPos.Text = "Key:";
         // 
         // tbPosX
         // 
         tbPosX.Location = new Point(650, 149);
         tbPosX.Name = "tbPosX";
         tbPosX.Size = new Size(42, 23);
         tbPosX.TabIndex = 13;
         tbPosX.Tag = "EditPos";
         // 
         // tbPosY
         // 
         tbPosY.Location = new Point(698, 149);
         tbPosY.Name = "tbPosY";
         tbPosY.Size = new Size(42, 23);
         tbPosY.TabIndex = 14;
         tbPosY.Tag = "EditPos";
         // 
         // lbPosX
         // 
         lbPosX.AutoSize = true;
         lbPosX.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbPosX.Location = new Point(662, 129);
         lbPosX.Name = "lbPosX";
         lbPosX.Size = new Size(21, 17);
         lbPosX.TabIndex = 15;
         lbPosX.Tag = "EditPos";
         lbPosX.Text = "X:";
         // 
         // lbPosY
         // 
         lbPosY.AutoSize = true;
         lbPosY.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbPosY.Location = new Point(707, 129);
         lbPosY.Name = "lbPosY";
         lbPosY.Size = new Size(20, 17);
         lbPosY.TabIndex = 16;
         lbPosY.Tag = "EditPos";
         lbPosY.Text = "Y:";
         // 
         // Form1
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(856, 656);
         Controls.Add(lbPosY);
         Controls.Add(lbPosX);
         Controls.Add(tbPosY);
         Controls.Add(tbPosX);
         Controls.Add(lbKeyPos);
         Controls.Add(btnEditPosition);
         Controls.Add(btnDeleteKeyPosition);
         Controls.Add(cboxShowSetKeyPos);
         Controls.Add(lbSetKeyPos);
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
      private Label lbSetKeyPos;
      private CheckBox cboxShowSetKeyPos;
      private Button btnDeleteKeyPosition;
      private Button btnEditPosition;
      private Label lbKeyPos;
      private TextBox tbPosX;
      private TextBox tbPosY;
      private Label lbPosX;
      private Label lbPosY;
   }
}
