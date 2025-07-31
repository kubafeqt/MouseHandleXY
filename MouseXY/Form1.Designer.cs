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
         lbSetname = new Label();
         tbSetname = new TextBox();
         btnAddSetname = new Button();
         cmbSelectSetname = new ComboBox();
         btnSelectSetname = new Button();
         btnAddKeyToSetname = new Button();
         btnShowSetname = new Button();
         btnRemoveKeyFromSetname = new Button();
         lbShowedSetname = new Label();
         lbSelectedSetname = new Label();
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
         btnSetKeyPos.Location = new Point(25, 153);
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
         btnShowKeysPositions.Location = new Point(121, 153);
         btnShowKeysPositions.Name = "btnShowKeysPositions";
         btnShowKeysPositions.Size = new Size(143, 24);
         btnShowKeysPositions.TabIndex = 6;
         btnShowKeysPositions.Text = "show keys positions";
         btnShowKeysPositions.UseVisualStyleBackColor = true;
         btnShowKeysPositions.Click += btnShowKeysPositions_Click;
         // 
         // dgvShowKeysPositions
         // 
         dgvShowKeysPositions.AllowUserToAddRows = false;
         dgvShowKeysPositions.AllowUserToDeleteRows = false;
         dgvShowKeysPositions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
         dgvShowKeysPositions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         dgvShowKeysPositions.Location = new Point(32, 190);
         dgvShowKeysPositions.MultiSelect = false;
         dgvShowKeysPositions.Name = "dgvShowKeysPositions";
         dgvShowKeysPositions.RowHeadersWidth = 42;
         dgvShowKeysPositions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
         dgvShowKeysPositions.Size = new Size(791, 441);
         dgvShowKeysPositions.TabIndex = 7;
         dgvShowKeysPositions.Visible = false;
         dgvShowKeysPositions.CellValueChanged += dgvShowKeysPositions_CellValueChanged;
         dgvShowKeysPositions.CurrentCellDirtyStateChanged += dgvShowKeysPositions_CurrentCellDirtyStateChanged;
         dgvShowKeysPositions.SelectionChanged += dgvShowKeysPositions_SelectionChanged;
         // 
         // lbSetKeyPos
         // 
         lbSetKeyPos.AutoSize = true;
         lbSetKeyPos.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbSetKeyPos.Location = new Point(270, 156);
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
         btnDeleteKeyPosition.Location = new Point(438, 153);
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
         btnEditPosition.Location = new Point(749, 152);
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
         lbKeyPos.Location = new Point(591, 156);
         lbKeyPos.Name = "lbKeyPos";
         lbKeyPos.Size = new Size(34, 17);
         lbKeyPos.TabIndex = 12;
         lbKeyPos.Tag = "EditPos";
         lbKeyPos.Text = "Key:";
         // 
         // tbPosX
         // 
         tbPosX.Location = new Point(650, 153);
         tbPosX.Name = "tbPosX";
         tbPosX.Size = new Size(42, 23);
         tbPosX.TabIndex = 13;
         tbPosX.Tag = "EditPos";
         // 
         // tbPosY
         // 
         tbPosY.Location = new Point(698, 153);
         tbPosY.Name = "tbPosY";
         tbPosY.Size = new Size(42, 23);
         tbPosY.TabIndex = 14;
         tbPosY.Tag = "EditPos";
         // 
         // lbPosX
         // 
         lbPosX.AutoSize = true;
         lbPosX.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbPosX.Location = new Point(662, 133);
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
         lbPosY.Location = new Point(707, 133);
         lbPosY.Name = "lbPosY";
         lbPosY.Size = new Size(20, 17);
         lbPosY.TabIndex = 16;
         lbPosY.Tag = "EditPos";
         lbPosY.Text = "Y:";
         // 
         // lbSetname
         // 
         lbSetname.AutoSize = true;
         lbSetname.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbSetname.Location = new Point(498, 12);
         lbSetname.Name = "lbSetname";
         lbSetname.Size = new Size(67, 17);
         lbSetname.TabIndex = 17;
         lbSetname.Tag = "EditPos";
         lbSetname.Text = "SetName:";
         // 
         // tbSetname
         // 
         tbSetname.Font = new Font("Segoe UI Semibold", 10.181818F, FontStyle.Bold, GraphicsUnit.Point, 0);
         tbSetname.Location = new Point(578, 11);
         tbSetname.MaxLength = 16;
         tbSetname.Name = "tbSetname";
         tbSetname.Size = new Size(162, 26);
         tbSetname.TabIndex = 18;
         tbSetname.Tag = "EditPos";
         tbSetname.TextAlign = HorizontalAlignment.Center;
         tbSetname.TextChanged += tbSetname_TextChanged;
         tbSetname.KeyDown += tbSetname_KeyDown;
         // 
         // btnAddSetname
         // 
         btnAddSetname.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnAddSetname.Location = new Point(746, 12);
         btnAddSetname.Name = "btnAddSetname";
         btnAddSetname.Size = new Size(80, 24);
         btnAddSetname.TabIndex = 19;
         btnAddSetname.Tag = "EditPos";
         btnAddSetname.Text = "add";
         btnAddSetname.UseVisualStyleBackColor = true;
         btnAddSetname.Visible = false;
         btnAddSetname.Click += btnAddSetname_Click;
         // 
         // cmbSelectSetname
         // 
         cmbSelectSetname.DropDownStyle = ComboBoxStyle.DropDownList;
         cmbSelectSetname.FormattingEnabled = true;
         cmbSelectSetname.Location = new Point(578, 43);
         cmbSelectSetname.Name = "cmbSelectSetname";
         cmbSelectSetname.Size = new Size(162, 23);
         cmbSelectSetname.TabIndex = 20;
         cmbSelectSetname.Tag = "EditPos";
         cmbSelectSetname.SelectedIndexChanged += cmbSelectSetname_SelectedIndexChanged;
         // 
         // btnSelectSetname
         // 
         btnSelectSetname.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnSelectSetname.Location = new Point(746, 43);
         btnSelectSetname.Name = "btnSelectSetname";
         btnSelectSetname.Size = new Size(80, 24);
         btnSelectSetname.TabIndex = 21;
         btnSelectSetname.Tag = "EditPos";
         btnSelectSetname.Text = "select";
         btnSelectSetname.UseVisualStyleBackColor = true;
         btnSelectSetname.Visible = false;
         btnSelectSetname.Click += btnSelectSetname_Click;
         // 
         // btnAddKeyToSetname
         // 
         btnAddKeyToSetname.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnAddKeyToSetname.Location = new Point(492, 71);
         btnAddKeyToSetname.Name = "btnAddKeyToSetname";
         btnAddKeyToSetname.Size = new Size(148, 24);
         btnAddKeyToSetname.TabIndex = 22;
         btnAddKeyToSetname.Tag = "EditPos";
         btnAddKeyToSetname.Text = "add key to setname";
         btnAddKeyToSetname.UseVisualStyleBackColor = true;
         btnAddKeyToSetname.Visible = false;
         btnAddKeyToSetname.Click += btnAddKeyToSetname_Click;
         // 
         // btnShowSetname
         // 
         btnShowSetname.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnShowSetname.Location = new Point(492, 41);
         btnShowSetname.Name = "btnShowSetname";
         btnShowSetname.Size = new Size(80, 24);
         btnShowSetname.TabIndex = 23;
         btnShowSetname.Tag = "EditPos";
         btnShowSetname.Text = "show :";
         btnShowSetname.UseVisualStyleBackColor = true;
         btnShowSetname.Visible = false;
         btnShowSetname.Click += btnShowSetname_Click;
         // 
         // btnRemoveKeyFromSetname
         // 
         btnRemoveKeyFromSetname.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnRemoveKeyFromSetname.Location = new Point(646, 71);
         btnRemoveKeyFromSetname.Name = "btnRemoveKeyFromSetname";
         btnRemoveKeyFromSetname.Size = new Size(180, 24);
         btnRemoveKeyFromSetname.TabIndex = 24;
         btnRemoveKeyFromSetname.Tag = "EditPos";
         btnRemoveKeyFromSetname.Text = "remove key from setname";
         btnRemoveKeyFromSetname.UseVisualStyleBackColor = true;
         btnRemoveKeyFromSetname.Visible = false;
         btnRemoveKeyFromSetname.Click += btnRemoveKeyFromSetname_Click;
         // 
         // lbShowedSetname
         // 
         lbShowedSetname.AutoSize = true;
         lbShowedSetname.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbShowedSetname.Location = new Point(492, 99);
         lbShowedSetname.Name = "lbShowedSetname";
         lbShowedSetname.Size = new Size(117, 17);
         lbShowedSetname.TabIndex = 25;
         lbShowedSetname.Tag = "EditPos";
         lbShowedSetname.Text = "ShowedSetname: ";
         // 
         // lbSelectedSetname
         // 
         lbSelectedSetname.AutoSize = true;
         lbSelectedSetname.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbSelectedSetname.Location = new Point(492, 116);
         lbSelectedSetname.Name = "lbSelectedSetname";
         lbSelectedSetname.Size = new Size(120, 17);
         lbSelectedSetname.TabIndex = 26;
         lbSelectedSetname.Tag = "EditPos";
         lbSelectedSetname.Text = "SelectedSetname: ";
         // 
         // Form1
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(856, 656);
         Controls.Add(lbSelectedSetname);
         Controls.Add(lbShowedSetname);
         Controls.Add(btnRemoveKeyFromSetname);
         Controls.Add(btnShowSetname);
         Controls.Add(btnAddKeyToSetname);
         Controls.Add(btnSelectSetname);
         Controls.Add(cmbSelectSetname);
         Controls.Add(btnAddSetname);
         Controls.Add(tbSetname);
         Controls.Add(lbSetname);
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
      private Label lbSetname;
      private TextBox tbSetname;
      private Button btnAddSetname;
      private ComboBox cmbSelectSetname;
      private Button btnSelectSetname;
      private Button btnAddKeyToSetname;
      private Button btnShowSetname;
      private Button btnRemoveKeyFromSetname;
      private Label lbShowedSetname;
      private Label lbSelectedSetname;
   }
}
