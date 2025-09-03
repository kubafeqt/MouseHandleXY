namespace MouseXY
{
    partial class MainForm
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
         lbDescriptionControl = new Label();
         btnAcceptDelayMs = new Button();
         btnSetKeyPos = new Button();
         btnShowKeysPositions = new Button();
         dgvShowKeysPositions = new DataGridView();
         lbSetKeyPos = new Label();
         cboxShowSetKeyPos = new CheckBox();
         btnDeleteKey = new Button();
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
         btnAddKeyToSelectedSetname = new Button();
         btnShowSetname = new Button();
         lbShowedSetname = new Label();
         lbSelectedSetname = new Label();
         lbMouseControl = new Label();
         btnExport = new Button();
         btnImport = new Button();
         panelMain = new Panel();
         btnBackToPreview = new Button();
         panelPreviewImport = new Panel();
         lbFileName_Preview = new Label();
         btnExit_Preview = new Button();
         btnImportSet_Preview = new Button();
         btnBackToJsonSelect_Preview = new Button();
         btnImportAll_Preview = new Button();
         lbShowedSetName_Preview = new Label();
         btnShowSetName_Preview = new Button();
         dgvShowKeysPositions_Preview = new DataGridView();
         cmbSelectSetName_Preview = new ComboBox();
         lbSetName_Preview = new Label();
         lbKeyPos_Preview = new Label();
         lbPosY_Preview = new Label();
         tbPosX_Preview = new TextBox();
         lbPosX_Preview = new Label();
         tbPosY_Preview = new TextBox();
         btnMainPanels = new Button();
         btnSettings = new Button();
         panelSettings = new Panel();
         label1 = new Label();
         label2 = new Label();
         label3 = new Label();
         label4 = new Label();
         label5 = new Label();
         label6 = new Label();
         label7 = new Label();
         label8 = new Label();
         label9 = new Label();
         textBox1 = new TextBox();
         textBox2 = new TextBox();
         textBox3 = new TextBox();
         textBox4 = new TextBox();
         textBox5 = new TextBox();
         textBox6 = new TextBox();
         textBox7 = new TextBox();
         textBox8 = new TextBox();
         textBox9 = new TextBox();
         comboBox1 = new ComboBox();
         button1 = new Button();
         button2 = new Button();
         comboBox2 = new ComboBox();
         ((System.ComponentModel.ISupportInitialize)nmDelayMs).BeginInit();
         ((System.ComponentModel.ISupportInitialize)dgvShowKeysPositions).BeginInit();
         panelMain.SuspendLayout();
         panelPreviewImport.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)dgvShowKeysPositions_Preview).BeginInit();
         panelSettings.SuspendLayout();
         SuspendLayout();
         // 
         // cboxOnStartup
         // 
         cboxOnStartup.AutoSize = true;
         cboxOnStartup.Location = new Point(23, 14);
         cboxOnStartup.Name = "cboxOnStartup";
         cboxOnStartup.Size = new Size(130, 19);
         cboxOnStartup.TabIndex = 0;
         cboxOnStartup.Text = "on windows startup";
         cboxOnStartup.UseVisualStyleBackColor = true;
         cboxOnStartup.CheckedChanged += cboxOnStartup_CheckedChanged;
         // 
         // nmDelayMs
         // 
         nmDelayMs.Location = new Point(86, 66);
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
         lbDelayMs.Location = new Point(23, 68);
         lbDelayMs.Name = "lbDelayMs";
         lbDelayMs.Size = new Size(57, 15);
         lbDelayMs.TabIndex = 2;
         lbDelayMs.Text = "delay ms:";
         // 
         // lbDescriptionControl
         // 
         lbDescriptionControl.AutoSize = true;
         lbDescriptionControl.Font = new Font("Segoe UI Semibold", 10.181818F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbDescriptionControl.Location = new Point(20, 93);
         lbDescriptionControl.Name = "lbDescriptionControl";
         lbDescriptionControl.Size = new Size(79, 19);
         lbDescriptionControl.TabIndex = 3;
         lbDescriptionControl.Text = "description";
         // 
         // btnAcceptDelayMs
         // 
         btnAcceptDelayMs.Location = new Point(159, 66);
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
         btnSetKeyPos.Location = new Point(23, 159);
         btnSetKeyPos.Name = "btnSetKeyPos";
         btnSetKeyPos.Size = new Size(79, 24);
         btnSetKeyPos.TabIndex = 5;
         btnSetKeyPos.Tag = "MouseControlDisable";
         btnSetKeyPos.Text = "SetKeyPos";
         btnSetKeyPos.UseVisualStyleBackColor = true;
         btnSetKeyPos.Click += btnSetKeyPos_Click;
         // 
         // btnShowKeysPositions
         // 
         btnShowKeysPositions.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnShowKeysPositions.Location = new Point(119, 159);
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
         dgvShowKeysPositions.Location = new Point(24, 196);
         dgvShowKeysPositions.MultiSelect = false;
         dgvShowKeysPositions.Name = "dgvShowKeysPositions";
         dgvShowKeysPositions.RowHeadersWidth = 42;
         dgvShowKeysPositions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
         dgvShowKeysPositions.Size = new Size(801, 441);
         dgvShowKeysPositions.TabIndex = 7;
         dgvShowKeysPositions.Visible = false;
         dgvShowKeysPositions.CellValueChanged += dgvShowKeysPositions_CellValueChanged;
         dgvShowKeysPositions.CurrentCellDirtyStateChanged += dgvShowKeysPositions_CurrentCellDirtyStateChanged;
         dgvShowKeysPositions.SelectionChanged += dgvShowKeysPositions_SelectionChanged;
         dgvShowKeysPositions.KeyDown += dgvShowKeysPositions_KeyDown;
         // 
         // lbSetKeyPos
         // 
         lbSetKeyPos.AutoSize = true;
         lbSetKeyPos.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbSetKeyPos.Location = new Point(268, 162);
         lbSetKeyPos.Name = "lbSetKeyPos";
         lbSetKeyPos.Size = new Size(167, 19);
         lbSetKeyPos.TabIndex = 8;
         lbSetKeyPos.Text = "setKeyPos open - X: , Y:";
         lbSetKeyPos.Visible = false;
         // 
         // cboxShowSetKeyPos
         // 
         cboxShowSetKeyPos.AutoSize = true;
         cboxShowSetKeyPos.Checked = true;
         cboxShowSetKeyPos.CheckState = CheckState.Checked;
         cboxShowSetKeyPos.Location = new Point(23, 36);
         cboxShowSetKeyPos.Name = "cboxShowSetKeyPos";
         cboxShowSetKeyPos.Size = new Size(214, 19);
         cboxShowSetKeyPos.TabIndex = 9;
         cboxShowSetKeyPos.Text = "show keys positions after setKeyPos";
         cboxShowSetKeyPos.UseVisualStyleBackColor = true;
         cboxShowSetKeyPos.CheckedChanged += cboxShowSetKeyPos_CheckedChanged;
         // 
         // btnDeleteKey
         // 
         btnDeleteKey.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnDeleteKey.Location = new Point(458, 158);
         btnDeleteKey.Name = "btnDeleteKey";
         btnDeleteKey.Size = new Size(152, 24);
         btnDeleteKey.TabIndex = 10;
         btnDeleteKey.Tag = "EditPos";
         btnDeleteKey.Text = "delete key from setname";
         btnDeleteKey.UseVisualStyleBackColor = true;
         btnDeleteKey.Visible = false;
         btnDeleteKey.Click += btnDeleteKey_Click;
         // 
         // btnEditPosition
         // 
         btnEditPosition.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnEditPosition.Location = new Point(749, 158);
         btnEditPosition.Name = "btnEditPosition";
         btnEditPosition.Size = new Size(75, 25);
         btnEditPosition.TabIndex = 11;
         btnEditPosition.Tag = "EditPos";
         btnEditPosition.Text = "Edit";
         btnEditPosition.UseVisualStyleBackColor = true;
         btnEditPosition.Visible = false;
         btnEditPosition.Click += btnEditPosition_Click;
         // 
         // lbKeyPos
         // 
         lbKeyPos.AutoSize = true;
         lbKeyPos.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbKeyPos.Location = new Point(615, 138);
         lbKeyPos.Name = "lbKeyPos";
         lbKeyPos.Size = new Size(38, 19);
         lbKeyPos.TabIndex = 12;
         lbKeyPos.Tag = "EditPos";
         lbKeyPos.Text = "Key:";
         lbKeyPos.Visible = false;
         // 
         // tbPosX
         // 
         tbPosX.Location = new Point(638, 159);
         tbPosX.Name = "tbPosX";
         tbPosX.Size = new Size(42, 23);
         tbPosX.TabIndex = 13;
         tbPosX.Tag = "EditPos";
         tbPosX.Visible = false;
         // 
         // tbPosY
         // 
         tbPosY.Location = new Point(703, 159);
         tbPosY.Name = "tbPosY";
         tbPosY.Size = new Size(42, 23);
         tbPosY.TabIndex = 14;
         tbPosY.Tag = "EditPos";
         tbPosY.Visible = false;
         // 
         // lbPosX
         // 
         lbPosX.AutoSize = true;
         lbPosX.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbPosX.Location = new Point(615, 162);
         lbPosX.Name = "lbPosX";
         lbPosX.Size = new Size(22, 19);
         lbPosX.TabIndex = 15;
         lbPosX.Tag = "EditPos";
         lbPosX.Text = "X:";
         lbPosX.Visible = false;
         // 
         // lbPosY
         // 
         lbPosY.AutoSize = true;
         lbPosY.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbPosY.Location = new Point(681, 162);
         lbPosY.Name = "lbPosY";
         lbPosY.Size = new Size(22, 19);
         lbPosY.TabIndex = 16;
         lbPosY.Tag = "EditPos";
         lbPosY.Text = "Y:";
         lbPosY.Visible = false;
         // 
         // lbSetname
         // 
         lbSetname.AutoSize = true;
         lbSetname.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbSetname.Location = new Point(503, 14);
         lbSetname.Name = "lbSetname";
         lbSetname.Size = new Size(74, 19);
         lbSetname.TabIndex = 17;
         lbSetname.Tag = "EditPos";
         lbSetname.Text = "SetName:";
         lbSetname.Visible = false;
         // 
         // tbSetname
         // 
         tbSetname.Font = new Font("Segoe UI Semibold", 10.181818F, FontStyle.Bold, GraphicsUnit.Point, 0);
         tbSetname.Location = new Point(576, 10);
         tbSetname.MaxLength = 16;
         tbSetname.Name = "tbSetname";
         tbSetname.Size = new Size(162, 26);
         tbSetname.TabIndex = 18;
         tbSetname.Tag = "EditPos;MouseControlDisable";
         tbSetname.TextAlign = HorizontalAlignment.Center;
         tbSetname.Visible = false;
         tbSetname.TextChanged += tbSetname_TextChanged;
         tbSetname.KeyDown += tbSetname_KeyDown;
         // 
         // btnAddSetname
         // 
         btnAddSetname.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnAddSetname.Location = new Point(744, 11);
         btnAddSetname.Name = "btnAddSetname";
         btnAddSetname.Size = new Size(80, 24);
         btnAddSetname.TabIndex = 19;
         btnAddSetname.Tag = "EditPos;MouseControlDisable";
         btnAddSetname.Text = "add";
         btnAddSetname.UseVisualStyleBackColor = true;
         btnAddSetname.Visible = false;
         btnAddSetname.Click += btnAddSetname_Click;
         // 
         // cmbSelectSetname
         // 
         cmbSelectSetname.DropDownStyle = ComboBoxStyle.DropDownList;
         cmbSelectSetname.FormattingEnabled = true;
         cmbSelectSetname.Location = new Point(490, 42);
         cmbSelectSetname.Name = "cmbSelectSetname";
         cmbSelectSetname.Size = new Size(162, 23);
         cmbSelectSetname.TabIndex = 20;
         cmbSelectSetname.Tag = "EditPos";
         cmbSelectSetname.Visible = false;
         cmbSelectSetname.SelectedIndexChanged += cmbSelectSetname_SelectedIndexChanged;
         // 
         // btnSelectSetname
         // 
         btnSelectSetname.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnSelectSetname.Location = new Point(744, 42);
         btnSelectSetname.Name = "btnSelectSetname";
         btnSelectSetname.Size = new Size(80, 24);
         btnSelectSetname.TabIndex = 21;
         btnSelectSetname.Tag = "EditPos";
         btnSelectSetname.Text = "select";
         btnSelectSetname.UseVisualStyleBackColor = true;
         btnSelectSetname.Visible = false;
         btnSelectSetname.Click += btnSelectSetname_Click;
         // 
         // btnAddKeyToSelectedSetname
         // 
         btnAddKeyToSelectedSetname.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnAddKeyToSelectedSetname.Location = new Point(490, 70);
         btnAddKeyToSelectedSetname.Name = "btnAddKeyToSelectedSetname";
         btnAddKeyToSelectedSetname.Size = new Size(334, 25);
         btnAddKeyToSelectedSetname.TabIndex = 22;
         btnAddKeyToSelectedSetname.Tag = "EditPos";
         btnAddKeyToSelectedSetname.Text = "add selected key to selected setname";
         btnAddKeyToSelectedSetname.UseVisualStyleBackColor = true;
         btnAddKeyToSelectedSetname.Visible = false;
         btnAddKeyToSelectedSetname.Click += btnAddKeyToSelectedSetname_Click;
         // 
         // btnShowSetname
         // 
         btnShowSetname.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnShowSetname.Location = new Point(658, 42);
         btnShowSetname.Name = "btnShowSetname";
         btnShowSetname.Size = new Size(80, 24);
         btnShowSetname.TabIndex = 23;
         btnShowSetname.Tag = "EditPos";
         btnShowSetname.Text = "show";
         btnShowSetname.UseVisualStyleBackColor = true;
         btnShowSetname.Visible = false;
         btnShowSetname.Click += btnShowSetname_Click;
         // 
         // lbShowedSetname
         // 
         lbShowedSetname.AutoSize = true;
         lbShowedSetname.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbShowedSetname.Location = new Point(490, 98);
         lbShowedSetname.Name = "lbShowedSetname";
         lbShowedSetname.Size = new Size(128, 19);
         lbShowedSetname.TabIndex = 25;
         lbShowedSetname.Tag = "EditPos";
         lbShowedSetname.Text = "ShowedSetname: ";
         lbShowedSetname.Visible = false;
         // 
         // lbSelectedSetname
         // 
         lbSelectedSetname.AutoSize = true;
         lbSelectedSetname.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbSelectedSetname.Location = new Point(490, 115);
         lbSelectedSetname.Name = "lbSelectedSetname";
         lbSelectedSetname.Size = new Size(132, 19);
         lbSelectedSetname.TabIndex = 26;
         lbSelectedSetname.Tag = "EditPos";
         lbSelectedSetname.Text = "SelectedSetname: ";
         lbSelectedSetname.Visible = false;
         // 
         // lbMouseControl
         // 
         lbMouseControl.AutoSize = true;
         lbMouseControl.Font = new Font("Segoe UI Semibold", 10.181818F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbMouseControl.ForeColor = Color.DarkGreen;
         lbMouseControl.Location = new Point(251, 66);
         lbMouseControl.Name = "lbMouseControl";
         lbMouseControl.Size = new Size(99, 19);
         lbMouseControl.TabIndex = 27;
         lbMouseControl.Text = "mouse control";
         lbMouseControl.Visible = false;
         // 
         // btnExport
         // 
         btnExport.Font = new Font("Segoe UI Semibold", 8.727273F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnExport.Location = new Point(242, 65);
         btnExport.Name = "btnExport";
         btnExport.Size = new Size(77, 24);
         btnExport.TabIndex = 28;
         btnExport.Tag = "ExpImp";
         btnExport.Text = "export";
         btnExport.UseVisualStyleBackColor = true;
         btnExport.Click += btnExport_Click;
         // 
         // btnImport
         // 
         btnImport.Font = new Font("Segoe UI Semibold", 8.727273F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnImport.Location = new Point(325, 65);
         btnImport.Name = "btnImport";
         btnImport.Size = new Size(77, 24);
         btnImport.TabIndex = 29;
         btnImport.Tag = "ExpImp";
         btnImport.Text = "import";
         btnImport.UseVisualStyleBackColor = true;
         btnImport.Click += btnImport_Click;
         // 
         // panelMain
         // 
         panelMain.Controls.Add(btnBackToPreview);
         panelMain.Controls.Add(cboxOnStartup);
         panelMain.Controls.Add(btnImport);
         panelMain.Controls.Add(nmDelayMs);
         panelMain.Controls.Add(btnExport);
         panelMain.Controls.Add(lbDelayMs);
         panelMain.Controls.Add(lbMouseControl);
         panelMain.Controls.Add(lbDescriptionControl);
         panelMain.Controls.Add(lbSelectedSetname);
         panelMain.Controls.Add(btnAcceptDelayMs);
         panelMain.Controls.Add(lbShowedSetname);
         panelMain.Controls.Add(btnSetKeyPos);
         panelMain.Controls.Add(btnShowSetname);
         panelMain.Controls.Add(btnShowKeysPositions);
         panelMain.Controls.Add(btnAddKeyToSelectedSetname);
         panelMain.Controls.Add(dgvShowKeysPositions);
         panelMain.Controls.Add(btnSelectSetname);
         panelMain.Controls.Add(lbSetKeyPos);
         panelMain.Controls.Add(cmbSelectSetname);
         panelMain.Controls.Add(cboxShowSetKeyPos);
         panelMain.Controls.Add(btnAddSetname);
         panelMain.Controls.Add(btnDeleteKey);
         panelMain.Controls.Add(tbSetname);
         panelMain.Controls.Add(btnEditPosition);
         panelMain.Controls.Add(lbSetname);
         panelMain.Controls.Add(lbKeyPos);
         panelMain.Controls.Add(lbPosY);
         panelMain.Controls.Add(tbPosX);
         panelMain.Controls.Add(lbPosX);
         panelMain.Controls.Add(tbPosY);
         panelMain.Location = new Point(923, 35);
         panelMain.Name = "panelMain";
         panelMain.Size = new Size(154, 87);
         panelMain.TabIndex = 30;
         panelMain.Tag = "bigPanels";
         // 
         // btnBackToPreview
         // 
         btnBackToPreview.Font = new Font("Segoe UI Semibold", 8.727273F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnBackToPreview.Location = new Point(325, 14);
         btnBackToPreview.Name = "btnBackToPreview";
         btnBackToPreview.Size = new Size(77, 41);
         btnBackToPreview.TabIndex = 30;
         btnBackToPreview.Tag = "BackToPreview";
         btnBackToPreview.Text = "back to preview";
         btnBackToPreview.UseVisualStyleBackColor = true;
         btnBackToPreview.Visible = false;
         btnBackToPreview.Click += btnBackToPreview_Click;
         // 
         // panelPreviewImport
         // 
         panelPreviewImport.Controls.Add(lbFileName_Preview);
         panelPreviewImport.Controls.Add(btnExit_Preview);
         panelPreviewImport.Controls.Add(btnImportSet_Preview);
         panelPreviewImport.Controls.Add(btnBackToJsonSelect_Preview);
         panelPreviewImport.Controls.Add(btnImportAll_Preview);
         panelPreviewImport.Controls.Add(lbShowedSetName_Preview);
         panelPreviewImport.Controls.Add(btnShowSetName_Preview);
         panelPreviewImport.Controls.Add(dgvShowKeysPositions_Preview);
         panelPreviewImport.Controls.Add(cmbSelectSetName_Preview);
         panelPreviewImport.Controls.Add(lbSetName_Preview);
         panelPreviewImport.Controls.Add(lbKeyPos_Preview);
         panelPreviewImport.Controls.Add(lbPosY_Preview);
         panelPreviewImport.Controls.Add(tbPosX_Preview);
         panelPreviewImport.Controls.Add(lbPosX_Preview);
         panelPreviewImport.Controls.Add(tbPosY_Preview);
         panelPreviewImport.Location = new Point(927, 141);
         panelPreviewImport.Name = "panelPreviewImport";
         panelPreviewImport.Size = new Size(150, 106);
         panelPreviewImport.TabIndex = 31;
         panelPreviewImport.Tag = "bigPanels";
         panelPreviewImport.Visible = false;
         // 
         // lbFileName_Preview
         // 
         lbFileName_Preview.AutoSize = true;
         lbFileName_Preview.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbFileName_Preview.Location = new Point(59, 31);
         lbFileName_Preview.Name = "lbFileName_Preview";
         lbFileName_Preview.Size = new Size(80, 19);
         lbFileName_Preview.TabIndex = 59;
         lbFileName_Preview.Tag = "";
         lbFileName_Preview.Text = "FileName: ";
         // 
         // btnExit_Preview
         // 
         btnExit_Preview.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnExit_Preview.Location = new Point(749, 52);
         btnExit_Preview.Name = "btnExit_Preview";
         btnExit_Preview.Size = new Size(71, 24);
         btnExit_Preview.TabIndex = 58;
         btnExit_Preview.Tag = "";
         btnExit_Preview.Text = "Exit";
         btnExit_Preview.UseVisualStyleBackColor = true;
         btnExit_Preview.Click += btnExit_Preview_Click;
         // 
         // btnImportSet_Preview
         // 
         btnImportSet_Preview.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnImportSet_Preview.Location = new Point(454, 80);
         btnImportSet_Preview.Name = "btnImportSet_Preview";
         btnImportSet_Preview.Size = new Size(95, 24);
         btnImportSet_Preview.TabIndex = 57;
         btnImportSet_Preview.Tag = "";
         btnImportSet_Preview.Text = "Import set";
         btnImportSet_Preview.UseVisualStyleBackColor = true;
         btnImportSet_Preview.Click += btnImportSet_Preview_Click;
         // 
         // btnBackToJsonSelect_Preview
         // 
         btnBackToJsonSelect_Preview.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnBackToJsonSelect_Preview.Location = new Point(656, 80);
         btnBackToJsonSelect_Preview.Name = "btnBackToJsonSelect_Preview";
         btnBackToJsonSelect_Preview.Size = new Size(164, 24);
         btnBackToJsonSelect_Preview.TabIndex = 56;
         btnBackToJsonSelect_Preview.Tag = "";
         btnBackToJsonSelect_Preview.Text = "Back to JSON selection";
         btnBackToJsonSelect_Preview.UseVisualStyleBackColor = true;
         btnBackToJsonSelect_Preview.Click += btnBackToJsonSelect_Preview_Click;
         // 
         // btnImportAll_Preview
         // 
         btnImportAll_Preview.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnImportAll_Preview.Location = new Point(555, 80);
         btnImportAll_Preview.Name = "btnImportAll_Preview";
         btnImportAll_Preview.Size = new Size(95, 24);
         btnImportAll_Preview.TabIndex = 55;
         btnImportAll_Preview.Tag = "";
         btnImportAll_Preview.Text = "Import all";
         btnImportAll_Preview.UseVisualStyleBackColor = true;
         btnImportAll_Preview.Click += btnImportAll_Preview_Click;
         // 
         // lbShowedSetName_Preview
         // 
         lbShowedSetName_Preview.AutoSize = true;
         lbShowedSetName_Preview.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbShowedSetName_Preview.Location = new Point(492, 54);
         lbShowedSetName_Preview.Name = "lbShowedSetName_Preview";
         lbShowedSetName_Preview.Size = new Size(128, 19);
         lbShowedSetName_Preview.TabIndex = 54;
         lbShowedSetName_Preview.Tag = "";
         lbShowedSetName_Preview.Text = "ShowedSetname: ";
         // 
         // btnShowSetName_Preview
         // 
         btnShowSetName_Preview.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnShowSetName_Preview.Location = new Point(740, 20);
         btnShowSetName_Preview.Name = "btnShowSetName_Preview";
         btnShowSetName_Preview.Size = new Size(80, 24);
         btnShowSetName_Preview.TabIndex = 53;
         btnShowSetName_Preview.Tag = "";
         btnShowSetName_Preview.Text = "show";
         btnShowSetName_Preview.UseVisualStyleBackColor = true;
         btnShowSetName_Preview.Click += btnShowSetName_Preview_Click;
         // 
         // dgvShowKeysPositions_Preview
         // 
         dgvShowKeysPositions_Preview.AllowUserToAddRows = false;
         dgvShowKeysPositions_Preview.AllowUserToDeleteRows = false;
         dgvShowKeysPositions_Preview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
         dgvShowKeysPositions_Preview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         dgvShowKeysPositions_Preview.Location = new Point(25, 115);
         dgvShowKeysPositions_Preview.MultiSelect = false;
         dgvShowKeysPositions_Preview.Name = "dgvShowKeysPositions_Preview";
         dgvShowKeysPositions_Preview.RowHeadersWidth = 42;
         dgvShowKeysPositions_Preview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
         dgvShowKeysPositions_Preview.Size = new Size(801, 522);
         dgvShowKeysPositions_Preview.TabIndex = 37;
         dgvShowKeysPositions_Preview.SelectionChanged += dgvShowKeysPositions_Preview_SelectionChanged;
         // 
         // cmbSelectSetName_Preview
         // 
         cmbSelectSetName_Preview.DropDownStyle = ComboBoxStyle.DropDownList;
         cmbSelectSetName_Preview.FormattingEnabled = true;
         cmbSelectSetName_Preview.Location = new Point(572, 20);
         cmbSelectSetName_Preview.Name = "cmbSelectSetName_Preview";
         cmbSelectSetName_Preview.Size = new Size(162, 23);
         cmbSelectSetName_Preview.TabIndex = 50;
         cmbSelectSetName_Preview.Tag = "";
         // 
         // lbSetName_Preview
         // 
         lbSetName_Preview.AutoSize = true;
         lbSetName_Preview.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbSetName_Preview.Location = new Point(492, 22);
         lbSetName_Preview.Name = "lbSetName_Preview";
         lbSetName_Preview.Size = new Size(74, 19);
         lbSetName_Preview.TabIndex = 47;
         lbSetName_Preview.Tag = "";
         lbSetName_Preview.Text = "SetName:";
         // 
         // lbKeyPos_Preview
         // 
         lbKeyPos_Preview.AutoSize = true;
         lbKeyPos_Preview.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbKeyPos_Preview.Location = new Point(20, 83);
         lbKeyPos_Preview.Name = "lbKeyPos_Preview";
         lbKeyPos_Preview.Size = new Size(38, 19);
         lbKeyPos_Preview.TabIndex = 42;
         lbKeyPos_Preview.Tag = "";
         lbKeyPos_Preview.Text = "Key:";
         // 
         // lbPosY_Preview
         // 
         lbPosY_Preview.AutoSize = true;
         lbPosY_Preview.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbPosY_Preview.Location = new Point(199, 84);
         lbPosY_Preview.Name = "lbPosY_Preview";
         lbPosY_Preview.Size = new Size(22, 19);
         lbPosY_Preview.TabIndex = 46;
         lbPosY_Preview.Tag = "";
         lbPosY_Preview.Text = "Y:";
         // 
         // tbPosX_Preview
         // 
         tbPosX_Preview.Enabled = false;
         tbPosX_Preview.Location = new Point(156, 81);
         tbPosX_Preview.Name = "tbPosX_Preview";
         tbPosX_Preview.Size = new Size(42, 23);
         tbPosX_Preview.TabIndex = 43;
         tbPosX_Preview.Tag = "";
         // 
         // lbPosX_Preview
         // 
         lbPosX_Preview.AutoSize = true;
         lbPosX_Preview.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbPosX_Preview.Location = new Point(133, 84);
         lbPosX_Preview.Name = "lbPosX_Preview";
         lbPosX_Preview.Size = new Size(22, 19);
         lbPosX_Preview.TabIndex = 45;
         lbPosX_Preview.Tag = "";
         lbPosX_Preview.Text = "X:";
         // 
         // tbPosY_Preview
         // 
         tbPosY_Preview.Enabled = false;
         tbPosY_Preview.Location = new Point(221, 81);
         tbPosY_Preview.Name = "tbPosY_Preview";
         tbPosY_Preview.Size = new Size(42, 23);
         tbPosY_Preview.TabIndex = 44;
         tbPosY_Preview.Tag = "";
         // 
         // btnMainPanels
         // 
         btnMainPanels.Location = new Point(25, 5);
         btnMainPanels.Name = "btnMainPanels";
         btnMainPanels.Size = new Size(75, 23);
         btnMainPanels.TabIndex = 32;
         btnMainPanels.Text = "main";
         btnMainPanels.UseVisualStyleBackColor = true;
         btnMainPanels.Click += btnMainPanels_Click;
         // 
         // btnSettings
         // 
         btnSettings.Location = new Point(118, 5);
         btnSettings.Name = "btnSettings";
         btnSettings.Size = new Size(75, 23);
         btnSettings.TabIndex = 33;
         btnSettings.Text = "settings";
         btnSettings.UseVisualStyleBackColor = true;
         btnSettings.Click += btnSettings_Click;
         // 
         // panelSettings
         // 
         panelSettings.Controls.Add(comboBox2);
         panelSettings.Controls.Add(button2);
         panelSettings.Controls.Add(button1);
         panelSettings.Controls.Add(comboBox1);
         panelSettings.Controls.Add(textBox9);
         panelSettings.Controls.Add(textBox8);
         panelSettings.Controls.Add(textBox7);
         panelSettings.Controls.Add(textBox6);
         panelSettings.Controls.Add(textBox5);
         panelSettings.Controls.Add(textBox4);
         panelSettings.Controls.Add(textBox3);
         panelSettings.Controls.Add(textBox2);
         panelSettings.Controls.Add(textBox1);
         panelSettings.Controls.Add(label9);
         panelSettings.Controls.Add(label8);
         panelSettings.Controls.Add(label7);
         panelSettings.Controls.Add(label6);
         panelSettings.Controls.Add(label5);
         panelSettings.Controls.Add(label4);
         panelSettings.Controls.Add(label3);
         panelSettings.Controls.Add(label2);
         panelSettings.Controls.Add(label1);
         panelSettings.Location = new Point(25, 52);
         panelSettings.Name = "panelSettings";
         panelSettings.Size = new Size(864, 576);
         panelSettings.TabIndex = 34;
         panelSettings.Tag = "bigPanels";
         panelSettings.Visible = false;
         // 
         // label1
         // 
         label1.AutoSize = true;
         label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         label1.Location = new Point(129, 49);
         label1.Name = "label1";
         label1.Size = new Size(72, 19);
         label1.TabIndex = 60;
         label1.Tag = "";
         label1.Text = "Move up:";
         // 
         // label2
         // 
         label2.AutoSize = true;
         label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         label2.Location = new Point(109, 75);
         label2.Name = "label2";
         label2.Size = new Size(92, 19);
         label2.TabIndex = 61;
         label2.Tag = "";
         label2.Text = "Move down:";
         // 
         // label3
         // 
         label3.AutoSize = true;
         label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         label3.Location = new Point(124, 102);
         label3.Name = "label3";
         label3.Size = new Size(77, 19);
         label3.TabIndex = 62;
         label3.Tag = "";
         label3.Text = "Move left:";
         // 
         // label4
         // 
         label4.AutoSize = true;
         label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         label4.Location = new Point(114, 133);
         label4.Name = "label4";
         label4.Size = new Size(87, 19);
         label4.TabIndex = 63;
         label4.Tag = "";
         label4.Text = "Move right:";
         // 
         // label5
         // 
         label5.AutoSize = true;
         label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         label5.Location = new Point(80, 160);
         label5.Name = "label5";
         label5.Size = new Size(121, 19);
         label5.TabIndex = 64;
         label5.Tag = "";
         label5.Text = "Left mouse click:";
         // 
         // label6
         // 
         label6.AutoSize = true;
         label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         label6.Location = new Point(70, 192);
         label6.Name = "label6";
         label6.Size = new Size(131, 19);
         label6.TabIndex = 65;
         label6.Tag = "";
         label6.Text = "Right mouse click:";
         // 
         // label7
         // 
         label7.AutoSize = true;
         label7.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         label7.Location = new Point(58, 227);
         label7.Name = "label7";
         label7.Size = new Size(143, 19);
         label7.TabIndex = 66;
         label7.Tag = "";
         label7.Text = "Middle mouse click:";
         // 
         // label8
         // 
         label8.AutoSize = true;
         label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         label8.Location = new Point(9, 255);
         label8.Name = "label8";
         label8.Size = new Size(192, 19);
         label8.TabIndex = 67;
         label8.Tag = "";
         label8.Text = "Middle mouse wheel down:";
         // 
         // label9
         // 
         label9.AutoSize = true;
         label9.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         label9.Location = new Point(29, 285);
         label9.Name = "label9";
         label9.Size = new Size(172, 19);
         label9.TabIndex = 68;
         label9.Tag = "";
         label9.Text = "Middle mouse wheel up:";
         // 
         // textBox1
         // 
         textBox1.Location = new Point(207, 49);
         textBox1.Name = "textBox1";
         textBox1.Size = new Size(37, 23);
         textBox1.TabIndex = 69;
         // 
         // textBox2
         // 
         textBox2.Location = new Point(207, 76);
         textBox2.Name = "textBox2";
         textBox2.Size = new Size(37, 23);
         textBox2.TabIndex = 70;
         // 
         // textBox3
         // 
         textBox3.Location = new Point(207, 102);
         textBox3.Name = "textBox3";
         textBox3.Size = new Size(37, 23);
         textBox3.TabIndex = 71;
         // 
         // textBox4
         // 
         textBox4.Location = new Point(207, 132);
         textBox4.Name = "textBox4";
         textBox4.Size = new Size(37, 23);
         textBox4.TabIndex = 72;
         // 
         // textBox5
         // 
         textBox5.Location = new Point(207, 161);
         textBox5.Name = "textBox5";
         textBox5.Size = new Size(37, 23);
         textBox5.TabIndex = 73;
         // 
         // textBox6
         // 
         textBox6.Location = new Point(207, 192);
         textBox6.Name = "textBox6";
         textBox6.Size = new Size(37, 23);
         textBox6.TabIndex = 74;
         // 
         // textBox7
         // 
         textBox7.Location = new Point(207, 227);
         textBox7.Name = "textBox7";
         textBox7.Size = new Size(37, 23);
         textBox7.TabIndex = 75;
         // 
         // textBox8
         // 
         textBox8.Location = new Point(207, 256);
         textBox8.Name = "textBox8";
         textBox8.Size = new Size(37, 23);
         textBox8.TabIndex = 76;
         // 
         // textBox9
         // 
         textBox9.Location = new Point(207, 285);
         textBox9.Name = "textBox9";
         textBox9.Size = new Size(37, 23);
         textBox9.TabIndex = 77;
         // 
         // comboBox1
         // 
         comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
         comboBox1.FormattingEnabled = true;
         comboBox1.Location = new Point(307, 51);
         comboBox1.Name = "comboBox1";
         comboBox1.Size = new Size(160, 23);
         comboBox1.TabIndex = 78;
         // 
         // button1
         // 
         button1.Location = new Point(307, 97);
         button1.Name = "button1";
         button1.Size = new Size(77, 24);
         button1.TabIndex = 79;
         button1.Text = "button1";
         button1.UseVisualStyleBackColor = true;
         // 
         // button2
         // 
         button2.Location = new Point(390, 97);
         button2.Name = "button2";
         button2.Size = new Size(77, 24);
         button2.TabIndex = 80;
         button2.Text = "button2";
         button2.UseVisualStyleBackColor = true;
         // 
         // comboBox2
         // 
         comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
         comboBox2.FormattingEnabled = true;
         comboBox2.Location = new Point(695, 27);
         comboBox2.Name = "comboBox2";
         comboBox2.Size = new Size(125, 23);
         comboBox2.TabIndex = 81;
         // 
         // MainForm
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(1113, 738);
         Controls.Add(panelSettings);
         Controls.Add(btnSettings);
         Controls.Add(btnMainPanels);
         Controls.Add(panelPreviewImport);
         Controls.Add(panelMain);
         FormBorderStyle = FormBorderStyle.FixedSingle;
         MaximizeBox = false;
         Name = "MainForm";
         Text = "MouseControl";
         Load += MainForm_Load;
         ((System.ComponentModel.ISupportInitialize)nmDelayMs).EndInit();
         ((System.ComponentModel.ISupportInitialize)dgvShowKeysPositions).EndInit();
         panelMain.ResumeLayout(false);
         panelMain.PerformLayout();
         panelPreviewImport.ResumeLayout(false);
         panelPreviewImport.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)dgvShowKeysPositions_Preview).EndInit();
         panelSettings.ResumeLayout(false);
         panelSettings.PerformLayout();
         ResumeLayout(false);
      }

      #endregion

      private CheckBox cboxOnStartup;
      private NumericUpDown nmDelayMs;
      private Label lbDelayMs;
      private Label lbDescriptionControl;
      private Button btnAcceptDelayMs;
      private Button btnSetKeyPos;
      private Button btnShowKeysPositions;
      private DataGridView dgvShowKeysPositions;
      private Label lbSetKeyPos;
      private CheckBox cboxShowSetKeyPos;
      private Button btnDeleteKey;
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
      private Button btnAddKeyToSelectedSetname;
      private Button btnShowSetname;
      private Label lbShowedSetname;
      private Label lbSelectedSetname;
      private Label lbMouseControl;
      private Button btnExport;
      private Button btnImport;
      private Panel panelMain;
      private Panel panelPreviewImport;
      private NumericUpDown numericUpDown1;
      private Label lbShowedSetName_Preview;
      private Button btnShowSetName_Preview;
      private DataGridView dgvShowKeysPositions_Preview;
      private ComboBox cmbSelectSetName_Preview;
      private Label lbSetName_Preview;
      private Label lbKeyPos_Preview;
      private Label lbPosY_Preview;
      private TextBox tbPosX_Preview;
      private Label lbPosX_Preview;
      private TextBox tbPosY_Preview;
      private Button btnImportAll_Preview;
      private Button btnImportSet_Preview;
      private Button btnBackToJsonSelect_Preview;
      private Button btnExit_Preview;
      private Button btnBackToPreview;
      private Label lbFileName_Preview;
      private Button btnMainPanels;
      private Button btnSettings;
      private Panel panelSettings;
      private Label label8;
      private Label label7;
      private Label label6;
      private Label label5;
      private Label label4;
      private Label label3;
      private Label label2;
      private Label label1;
      private TextBox textBox9;
      private TextBox textBox8;
      private TextBox textBox7;
      private TextBox textBox6;
      private TextBox textBox5;
      private TextBox textBox4;
      private TextBox textBox3;
      private TextBox textBox2;
      private TextBox textBox1;
      private Label label9;
      private ComboBox comboBox2;
      private Button button2;
      private Button button1;
      private ComboBox comboBox1;
   }
}
