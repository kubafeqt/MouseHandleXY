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
         lbBaseKeysAlternative = new Label();
         lbBaseKeysMain = new Label();
         tbAltMiddleMouseWheelUp = new TextBox();
         tbAltMiddleMouseWheelDown = new TextBox();
         tbAltMiddleMouseClick = new TextBox();
         tbAltRightMouseClick = new TextBox();
         tbAltLeftMouseClick = new TextBox();
         tbAltMoveRight = new TextBox();
         tbAltMoveLeft = new TextBox();
         tbAltMoveDown = new TextBox();
         tbAltMoveUp = new TextBox();
         btnDeleteBaseKeysSetname = new Button();
         btnCreateBaseKeysSetname = new Button();
         lbBaseKeysSetName = new Label();
         tbBaseKeysSetName = new TextBox();
         cboxMiddleMouseWheelUp = new CheckBox();
         cboxMiddleMouseWheelDown = new CheckBox();
         cboxMiddleMouseClick = new CheckBox();
         cboxRightMouseClick = new CheckBox();
         cboxLeftMouseClick = new CheckBox();
         cboxMoveLeft = new CheckBox();
         cboxMoveRight = new CheckBox();
         cboxMoveDown = new CheckBox();
         cboxMoveUp = new CheckBox();
         cmbSelectSettingsType = new ComboBox();
         btnSaveBaseKeySet = new Button();
         btnSelectBaseKeySet = new Button();
         cmbBaseKeysSets = new ComboBox();
         tbMiddleMouseWheelUp = new TextBox();
         tbMiddleMouseWheelDown = new TextBox();
         tbMiddleMouseClick = new TextBox();
         tbRightMouseClick = new TextBox();
         tbLeftMouseClick = new TextBox();
         tbMoveRight = new TextBox();
         tbMoveLeft = new TextBox();
         tbMoveDown = new TextBox();
         tbMoveUp = new TextBox();
         lbMiddleMouseWheelUp = new Label();
         lbMiddleMouseWheelDown = new Label();
         lbMiddleMouseClick = new Label();
         lbRightMouseClick = new Label();
         lbLeftMouseClick = new Label();
         lbMoveRight = new Label();
         lbMoveLeft = new Label();
         lbMoveDown = new Label();
         lbMoveUp = new Label();
         lbSettingsType = new Label();
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
         lbKeyPos.Size = new Size(34, 17);
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
         lbPosX.Size = new Size(21, 17);
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
         lbPosY.Size = new Size(20, 17);
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
         lbSetname.Size = new Size(67, 17);
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
         lbShowedSetname.Size = new Size(117, 17);
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
         lbSelectedSetname.Size = new Size(120, 17);
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
         panelMain.Location = new Point(916, 5);
         panelMain.Name = "panelMain";
         panelMain.Size = new Size(186, 131);
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
         panelPreviewImport.Location = new Point(796, 8);
         panelPreviewImport.Name = "panelPreviewImport";
         panelPreviewImport.Size = new Size(114, 92);
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
         lbFileName_Preview.Size = new Size(74, 17);
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
         lbShowedSetName_Preview.Size = new Size(117, 17);
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
         lbSetName_Preview.Size = new Size(67, 17);
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
         lbKeyPos_Preview.Size = new Size(34, 17);
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
         lbPosY_Preview.Size = new Size(20, 17);
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
         lbPosX_Preview.Size = new Size(21, 17);
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
         panelSettings.Controls.Add(lbSettingsType);
         panelSettings.Controls.Add(lbBaseKeysAlternative);
         panelSettings.Controls.Add(lbBaseKeysMain);
         panelSettings.Controls.Add(tbAltMiddleMouseWheelUp);
         panelSettings.Controls.Add(tbAltMiddleMouseWheelDown);
         panelSettings.Controls.Add(tbAltMiddleMouseClick);
         panelSettings.Controls.Add(tbAltRightMouseClick);
         panelSettings.Controls.Add(tbAltLeftMouseClick);
         panelSettings.Controls.Add(tbAltMoveRight);
         panelSettings.Controls.Add(tbAltMoveLeft);
         panelSettings.Controls.Add(tbAltMoveDown);
         panelSettings.Controls.Add(tbAltMoveUp);
         panelSettings.Controls.Add(btnDeleteBaseKeysSetname);
         panelSettings.Controls.Add(btnCreateBaseKeysSetname);
         panelSettings.Controls.Add(lbBaseKeysSetName);
         panelSettings.Controls.Add(tbBaseKeysSetName);
         panelSettings.Controls.Add(cboxMiddleMouseWheelUp);
         panelSettings.Controls.Add(cboxMiddleMouseWheelDown);
         panelSettings.Controls.Add(cboxMiddleMouseClick);
         panelSettings.Controls.Add(cboxRightMouseClick);
         panelSettings.Controls.Add(cboxLeftMouseClick);
         panelSettings.Controls.Add(cboxMoveLeft);
         panelSettings.Controls.Add(cboxMoveRight);
         panelSettings.Controls.Add(cboxMoveDown);
         panelSettings.Controls.Add(cboxMoveUp);
         panelSettings.Controls.Add(cmbSelectSettingsType);
         panelSettings.Controls.Add(btnSaveBaseKeySet);
         panelSettings.Controls.Add(btnSelectBaseKeySet);
         panelSettings.Controls.Add(cmbBaseKeysSets);
         panelSettings.Controls.Add(tbMiddleMouseWheelUp);
         panelSettings.Controls.Add(tbMiddleMouseWheelDown);
         panelSettings.Controls.Add(tbMiddleMouseClick);
         panelSettings.Controls.Add(tbRightMouseClick);
         panelSettings.Controls.Add(tbLeftMouseClick);
         panelSettings.Controls.Add(tbMoveRight);
         panelSettings.Controls.Add(tbMoveLeft);
         panelSettings.Controls.Add(tbMoveDown);
         panelSettings.Controls.Add(tbMoveUp);
         panelSettings.Controls.Add(lbMiddleMouseWheelUp);
         panelSettings.Controls.Add(lbMiddleMouseWheelDown);
         panelSettings.Controls.Add(lbMiddleMouseClick);
         panelSettings.Controls.Add(lbRightMouseClick);
         panelSettings.Controls.Add(lbLeftMouseClick);
         panelSettings.Controls.Add(lbMoveRight);
         panelSettings.Controls.Add(lbMoveLeft);
         panelSettings.Controls.Add(lbMoveDown);
         panelSettings.Controls.Add(lbMoveUp);
         panelSettings.Location = new Point(25, 52);
         panelSettings.Name = "panelSettings";
         panelSettings.Size = new Size(825, 496);
         panelSettings.TabIndex = 34;
         panelSettings.Tag = "bigPanels";
         panelSettings.Visible = false;
         // 
         // lbBaseKeysAlternative
         // 
         lbBaseKeysAlternative.AutoSize = true;
         lbBaseKeysAlternative.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbBaseKeysAlternative.Location = new Point(353, 25);
         lbBaseKeysAlternative.Name = "lbBaseKeysAlternative";
         lbBaseKeysAlternative.Size = new Size(80, 17);
         lbBaseKeysAlternative.TabIndex = 104;
         lbBaseKeysAlternative.Tag = "";
         lbBaseKeysAlternative.Text = "Alternative:";
         // 
         // lbBaseKeysMain
         // 
         lbBaseKeysMain.AutoSize = true;
         lbBaseKeysMain.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbBaseKeysMain.Location = new Point(221, 26);
         lbBaseKeysMain.Name = "lbBaseKeysMain";
         lbBaseKeysMain.Size = new Size(43, 17);
         lbBaseKeysMain.TabIndex = 61;
         lbBaseKeysMain.Tag = "";
         lbBaseKeysMain.Text = "Main:";
         // 
         // tbAltMiddleMouseWheelUp
         // 
         tbAltMiddleMouseWheelUp.Enabled = false;
         tbAltMiddleMouseWheelUp.Location = new Point(353, 286);
         tbAltMiddleMouseWheelUp.Name = "tbAltMiddleMouseWheelUp";
         tbAltMiddleMouseWheelUp.Size = new Size(143, 23);
         tbAltMiddleMouseWheelUp.TabIndex = 103;
         tbAltMiddleMouseWheelUp.Tag = "BaseKeysSettingsTbs";
         tbAltMiddleMouseWheelUp.TextAlign = HorizontalAlignment.Center;
         // 
         // tbAltMiddleMouseWheelDown
         // 
         tbAltMiddleMouseWheelDown.Enabled = false;
         tbAltMiddleMouseWheelDown.Location = new Point(353, 257);
         tbAltMiddleMouseWheelDown.Name = "tbAltMiddleMouseWheelDown";
         tbAltMiddleMouseWheelDown.Size = new Size(143, 23);
         tbAltMiddleMouseWheelDown.TabIndex = 102;
         tbAltMiddleMouseWheelDown.Tag = "BaseKeysSettingsTbs";
         tbAltMiddleMouseWheelDown.TextAlign = HorizontalAlignment.Center;
         // 
         // tbAltMiddleMouseClick
         // 
         tbAltMiddleMouseClick.Enabled = false;
         tbAltMiddleMouseClick.Location = new Point(353, 228);
         tbAltMiddleMouseClick.Name = "tbAltMiddleMouseClick";
         tbAltMiddleMouseClick.Size = new Size(143, 23);
         tbAltMiddleMouseClick.TabIndex = 101;
         tbAltMiddleMouseClick.Tag = "BaseKeysSettingsTbs";
         tbAltMiddleMouseClick.TextAlign = HorizontalAlignment.Center;
         // 
         // tbAltRightMouseClick
         // 
         tbAltRightMouseClick.Enabled = false;
         tbAltRightMouseClick.Location = new Point(353, 196);
         tbAltRightMouseClick.Name = "tbAltRightMouseClick";
         tbAltRightMouseClick.Size = new Size(143, 23);
         tbAltRightMouseClick.TabIndex = 100;
         tbAltRightMouseClick.Tag = "BaseKeysSettingsTbs";
         tbAltRightMouseClick.TextAlign = HorizontalAlignment.Center;
         // 
         // tbAltLeftMouseClick
         // 
         tbAltLeftMouseClick.Enabled = false;
         tbAltLeftMouseClick.Location = new Point(353, 162);
         tbAltLeftMouseClick.Name = "tbAltLeftMouseClick";
         tbAltLeftMouseClick.Size = new Size(143, 23);
         tbAltLeftMouseClick.TabIndex = 99;
         tbAltLeftMouseClick.Tag = "BaseKeysSettingsTbs";
         tbAltLeftMouseClick.TextAlign = HorizontalAlignment.Center;
         // 
         // tbAltMoveRight
         // 
         tbAltMoveRight.Enabled = false;
         tbAltMoveRight.Location = new Point(353, 133);
         tbAltMoveRight.Name = "tbAltMoveRight";
         tbAltMoveRight.Size = new Size(143, 23);
         tbAltMoveRight.TabIndex = 98;
         tbAltMoveRight.Tag = "BaseKeysSettingsTbs";
         tbAltMoveRight.TextAlign = HorizontalAlignment.Center;
         // 
         // tbAltMoveLeft
         // 
         tbAltMoveLeft.Enabled = false;
         tbAltMoveLeft.Location = new Point(353, 103);
         tbAltMoveLeft.Name = "tbAltMoveLeft";
         tbAltMoveLeft.Size = new Size(143, 23);
         tbAltMoveLeft.TabIndex = 97;
         tbAltMoveLeft.Tag = "BaseKeysSettingsTbs";
         tbAltMoveLeft.TextAlign = HorizontalAlignment.Center;
         // 
         // tbAltMoveDown
         // 
         tbAltMoveDown.Enabled = false;
         tbAltMoveDown.Location = new Point(353, 77);
         tbAltMoveDown.Name = "tbAltMoveDown";
         tbAltMoveDown.Size = new Size(143, 23);
         tbAltMoveDown.TabIndex = 96;
         tbAltMoveDown.Tag = "BaseKeysSettingsTbs";
         tbAltMoveDown.TextAlign = HorizontalAlignment.Center;
         // 
         // tbAltMoveUp
         // 
         tbAltMoveUp.Enabled = false;
         tbAltMoveUp.Location = new Point(353, 50);
         tbAltMoveUp.Name = "tbAltMoveUp";
         tbAltMoveUp.Size = new Size(143, 23);
         tbAltMoveUp.TabIndex = 95;
         tbAltMoveUp.Tag = "BaseKeysSettingsTbs";
         tbAltMoveUp.TextAlign = HorizontalAlignment.Center;
         // 
         // btnDeleteBaseKeysSetname
         // 
         btnDeleteBaseKeysSetname.Location = new Point(695, 225);
         btnDeleteBaseKeysSetname.Name = "btnDeleteBaseKeysSetname";
         btnDeleteBaseKeysSetname.Size = new Size(77, 24);
         btnDeleteBaseKeysSetname.TabIndex = 94;
         btnDeleteBaseKeysSetname.Text = "delete";
         btnDeleteBaseKeysSetname.UseVisualStyleBackColor = true;
         // 
         // btnCreateBaseKeysSetname
         // 
         btnCreateBaseKeysSetname.Location = new Point(612, 225);
         btnCreateBaseKeysSetname.Name = "btnCreateBaseKeysSetname";
         btnCreateBaseKeysSetname.Size = new Size(77, 24);
         btnCreateBaseKeysSetname.TabIndex = 93;
         btnCreateBaseKeysSetname.Text = "create";
         btnCreateBaseKeysSetname.UseVisualStyleBackColor = true;
         // 
         // lbBaseKeysSetName
         // 
         lbBaseKeysSetName.AutoSize = true;
         lbBaseKeysSetName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbBaseKeysSetName.Location = new Point(612, 174);
         lbBaseKeysSetName.Name = "lbBaseKeysSetName";
         lbBaseKeysSetName.Size = new Size(65, 17);
         lbBaseKeysSetName.TabIndex = 92;
         lbBaseKeysSetName.Tag = "";
         lbBaseKeysSetName.Text = "Setname:";
         // 
         // tbBaseKeysSetName
         // 
         tbBaseKeysSetName.Location = new Point(612, 196);
         tbBaseKeysSetName.Name = "tbBaseKeysSetName";
         tbBaseKeysSetName.Size = new Size(160, 23);
         tbBaseKeysSetName.TabIndex = 91;
         // 
         // cboxMiddleMouseWheelUp
         // 
         cboxMiddleMouseWheelUp.AutoSize = true;
         cboxMiddleMouseWheelUp.Checked = true;
         cboxMiddleMouseWheelUp.CheckState = CheckState.Checked;
         cboxMiddleMouseWheelUp.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
         cboxMiddleMouseWheelUp.Location = new Point(502, 290);
         cboxMiddleMouseWheelUp.Name = "cboxMiddleMouseWheelUp";
         cboxMiddleMouseWheelUp.Size = new Size(67, 21);
         cboxMiddleMouseWheelUp.TabIndex = 90;
         cboxMiddleMouseWheelUp.Tag = "baseKeysCheckbox";
         cboxMiddleMouseWheelUp.Text = "enable";
         cboxMiddleMouseWheelUp.UseVisualStyleBackColor = true;
         // 
         // cboxMiddleMouseWheelDown
         // 
         cboxMiddleMouseWheelDown.AutoSize = true;
         cboxMiddleMouseWheelDown.Checked = true;
         cboxMiddleMouseWheelDown.CheckState = CheckState.Checked;
         cboxMiddleMouseWheelDown.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
         cboxMiddleMouseWheelDown.Location = new Point(502, 260);
         cboxMiddleMouseWheelDown.Name = "cboxMiddleMouseWheelDown";
         cboxMiddleMouseWheelDown.Size = new Size(67, 21);
         cboxMiddleMouseWheelDown.TabIndex = 89;
         cboxMiddleMouseWheelDown.Tag = "baseKeysCheckbox";
         cboxMiddleMouseWheelDown.Text = "enable";
         cboxMiddleMouseWheelDown.UseVisualStyleBackColor = true;
         // 
         // cboxMiddleMouseClick
         // 
         cboxMiddleMouseClick.AutoSize = true;
         cboxMiddleMouseClick.Checked = true;
         cboxMiddleMouseClick.CheckState = CheckState.Checked;
         cboxMiddleMouseClick.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
         cboxMiddleMouseClick.Location = new Point(502, 232);
         cboxMiddleMouseClick.Name = "cboxMiddleMouseClick";
         cboxMiddleMouseClick.Size = new Size(67, 21);
         cboxMiddleMouseClick.TabIndex = 88;
         cboxMiddleMouseClick.Tag = "baseKeysCheckbox";
         cboxMiddleMouseClick.Text = "enable";
         cboxMiddleMouseClick.UseVisualStyleBackColor = true;
         // 
         // cboxRightMouseClick
         // 
         cboxRightMouseClick.AutoSize = true;
         cboxRightMouseClick.Checked = true;
         cboxRightMouseClick.CheckState = CheckState.Checked;
         cboxRightMouseClick.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
         cboxRightMouseClick.Location = new Point(502, 198);
         cboxRightMouseClick.Name = "cboxRightMouseClick";
         cboxRightMouseClick.Size = new Size(67, 21);
         cboxRightMouseClick.TabIndex = 87;
         cboxRightMouseClick.Tag = "baseKeysCheckbox";
         cboxRightMouseClick.Text = "enable";
         cboxRightMouseClick.UseVisualStyleBackColor = true;
         // 
         // cboxLeftMouseClick
         // 
         cboxLeftMouseClick.AutoSize = true;
         cboxLeftMouseClick.Checked = true;
         cboxLeftMouseClick.CheckState = CheckState.Checked;
         cboxLeftMouseClick.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
         cboxLeftMouseClick.Location = new Point(502, 167);
         cboxLeftMouseClick.Name = "cboxLeftMouseClick";
         cboxLeftMouseClick.Size = new Size(67, 21);
         cboxLeftMouseClick.TabIndex = 86;
         cboxLeftMouseClick.Tag = "baseKeysCheckbox";
         cboxLeftMouseClick.Text = "enable";
         cboxLeftMouseClick.UseVisualStyleBackColor = true;
         // 
         // cboxMoveLeft
         // 
         cboxMoveLeft.AutoSize = true;
         cboxMoveLeft.Checked = true;
         cboxMoveLeft.CheckState = CheckState.Checked;
         cboxMoveLeft.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
         cboxMoveLeft.Location = new Point(502, 108);
         cboxMoveLeft.Name = "cboxMoveLeft";
         cboxMoveLeft.Size = new Size(67, 21);
         cboxMoveLeft.TabIndex = 85;
         cboxMoveLeft.Tag = "baseKeysCheckbox";
         cboxMoveLeft.Text = "enable";
         cboxMoveLeft.UseVisualStyleBackColor = true;
         // 
         // cboxMoveRight
         // 
         cboxMoveRight.AutoSize = true;
         cboxMoveRight.Checked = true;
         cboxMoveRight.CheckState = CheckState.Checked;
         cboxMoveRight.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
         cboxMoveRight.Location = new Point(502, 137);
         cboxMoveRight.Name = "cboxMoveRight";
         cboxMoveRight.Size = new Size(67, 21);
         cboxMoveRight.TabIndex = 84;
         cboxMoveRight.Tag = "baseKeysCheckbox";
         cboxMoveRight.Text = "enable";
         cboxMoveRight.UseVisualStyleBackColor = true;
         // 
         // cboxMoveDown
         // 
         cboxMoveDown.AutoSize = true;
         cboxMoveDown.Checked = true;
         cboxMoveDown.CheckState = CheckState.Checked;
         cboxMoveDown.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
         cboxMoveDown.Location = new Point(502, 80);
         cboxMoveDown.Name = "cboxMoveDown";
         cboxMoveDown.Size = new Size(67, 21);
         cboxMoveDown.TabIndex = 83;
         cboxMoveDown.Tag = "baseKeysCheckbox";
         cboxMoveDown.Text = "enable";
         cboxMoveDown.UseVisualStyleBackColor = true;
         // 
         // cboxMoveUp
         // 
         cboxMoveUp.AutoSize = true;
         cboxMoveUp.Checked = true;
         cboxMoveUp.CheckState = CheckState.Checked;
         cboxMoveUp.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
         cboxMoveUp.Location = new Point(502, 54);
         cboxMoveUp.Name = "cboxMoveUp";
         cboxMoveUp.Size = new Size(67, 21);
         cboxMoveUp.TabIndex = 82;
         cboxMoveUp.Tag = "baseKeysCheckbox";
         cboxMoveUp.Text = "enable";
         cboxMoveUp.UseVisualStyleBackColor = true;
         // 
         // cmbSelectSettingsType
         // 
         cmbSelectSettingsType.DropDownStyle = ComboBoxStyle.DropDownList;
         cmbSelectSettingsType.FormattingEnabled = true;
         cmbSelectSettingsType.Location = new Point(681, 13);
         cmbSelectSettingsType.Name = "cmbSelectSettingsType";
         cmbSelectSettingsType.Size = new Size(148, 23);
         cmbSelectSettingsType.TabIndex = 81;
         // 
         // btnSaveBaseKeySet
         // 
         btnSaveBaseKeySet.Location = new Point(695, 139);
         btnSaveBaseKeySet.Name = "btnSaveBaseKeySet";
         btnSaveBaseKeySet.Size = new Size(77, 24);
         btnSaveBaseKeySet.TabIndex = 80;
         btnSaveBaseKeySet.Text = "save set";
         btnSaveBaseKeySet.UseVisualStyleBackColor = true;
         // 
         // btnSelectBaseKeySet
         // 
         btnSelectBaseKeySet.Location = new Point(612, 139);
         btnSelectBaseKeySet.Name = "btnSelectBaseKeySet";
         btnSelectBaseKeySet.Size = new Size(77, 24);
         btnSelectBaseKeySet.TabIndex = 79;
         btnSelectBaseKeySet.Text = "select";
         btnSelectBaseKeySet.UseVisualStyleBackColor = true;
         // 
         // cmbBaseKeysSets
         // 
         cmbBaseKeysSets.DropDownStyle = ComboBoxStyle.DropDownList;
         cmbBaseKeysSets.FormattingEnabled = true;
         cmbBaseKeysSets.Location = new Point(612, 103);
         cmbBaseKeysSets.Name = "cmbBaseKeysSets";
         cmbBaseKeysSets.Size = new Size(160, 23);
         cmbBaseKeysSets.TabIndex = 78;
         // 
         // tbMiddleMouseWheelUp
         // 
         tbMiddleMouseWheelUp.Enabled = false;
         tbMiddleMouseWheelUp.Location = new Point(204, 285);
         tbMiddleMouseWheelUp.Name = "tbMiddleMouseWheelUp";
         tbMiddleMouseWheelUp.Size = new Size(143, 23);
         tbMiddleMouseWheelUp.TabIndex = 77;
         tbMiddleMouseWheelUp.Tag = "BaseKeysSettingsTbs";
         tbMiddleMouseWheelUp.TextAlign = HorizontalAlignment.Center;
         // 
         // tbMiddleMouseWheelDown
         // 
         tbMiddleMouseWheelDown.Enabled = false;
         tbMiddleMouseWheelDown.Location = new Point(204, 256);
         tbMiddleMouseWheelDown.Name = "tbMiddleMouseWheelDown";
         tbMiddleMouseWheelDown.Size = new Size(143, 23);
         tbMiddleMouseWheelDown.TabIndex = 76;
         tbMiddleMouseWheelDown.Tag = "BaseKeysSettingsTbs";
         tbMiddleMouseWheelDown.TextAlign = HorizontalAlignment.Center;
         // 
         // tbMiddleMouseClick
         // 
         tbMiddleMouseClick.Enabled = false;
         tbMiddleMouseClick.Location = new Point(204, 227);
         tbMiddleMouseClick.Name = "tbMiddleMouseClick";
         tbMiddleMouseClick.Size = new Size(143, 23);
         tbMiddleMouseClick.TabIndex = 75;
         tbMiddleMouseClick.Tag = "BaseKeysSettingsTbs";
         tbMiddleMouseClick.TextAlign = HorizontalAlignment.Center;
         // 
         // tbRightMouseClick
         // 
         tbRightMouseClick.Enabled = false;
         tbRightMouseClick.Location = new Point(204, 196);
         tbRightMouseClick.Name = "tbRightMouseClick";
         tbRightMouseClick.Size = new Size(143, 23);
         tbRightMouseClick.TabIndex = 74;
         tbRightMouseClick.Tag = "BaseKeysSettingsTbs";
         tbRightMouseClick.TextAlign = HorizontalAlignment.Center;
         // 
         // tbLeftMouseClick
         // 
         tbLeftMouseClick.Enabled = false;
         tbLeftMouseClick.Location = new Point(204, 161);
         tbLeftMouseClick.Name = "tbLeftMouseClick";
         tbLeftMouseClick.Size = new Size(143, 23);
         tbLeftMouseClick.TabIndex = 73;
         tbLeftMouseClick.Tag = "BaseKeysSettingsTbs";
         tbLeftMouseClick.TextAlign = HorizontalAlignment.Center;
         // 
         // tbMoveRight
         // 
         tbMoveRight.Enabled = false;
         tbMoveRight.Location = new Point(204, 132);
         tbMoveRight.Name = "tbMoveRight";
         tbMoveRight.Size = new Size(143, 23);
         tbMoveRight.TabIndex = 72;
         tbMoveRight.Tag = "BaseKeysSettingsTbs";
         tbMoveRight.TextAlign = HorizontalAlignment.Center;
         // 
         // tbMoveLeft
         // 
         tbMoveLeft.Enabled = false;
         tbMoveLeft.Location = new Point(204, 102);
         tbMoveLeft.Name = "tbMoveLeft";
         tbMoveLeft.Size = new Size(143, 23);
         tbMoveLeft.TabIndex = 71;
         tbMoveLeft.Tag = "BaseKeysSettingsTbs";
         tbMoveLeft.TextAlign = HorizontalAlignment.Center;
         // 
         // tbMoveDown
         // 
         tbMoveDown.Enabled = false;
         tbMoveDown.Location = new Point(204, 76);
         tbMoveDown.Name = "tbMoveDown";
         tbMoveDown.Size = new Size(143, 23);
         tbMoveDown.TabIndex = 70;
         tbMoveDown.Tag = "BaseKeysSettingsTbs";
         tbMoveDown.TextAlign = HorizontalAlignment.Center;
         // 
         // tbMoveUp
         // 
         tbMoveUp.Enabled = false;
         tbMoveUp.Location = new Point(204, 49);
         tbMoveUp.Name = "tbMoveUp";
         tbMoveUp.Size = new Size(143, 23);
         tbMoveUp.TabIndex = 69;
         tbMoveUp.Tag = "BaseKeysSettingsTbs";
         tbMoveUp.TextAlign = HorizontalAlignment.Center;
         // 
         // lbMiddleMouseWheelUp
         // 
         lbMiddleMouseWheelUp.AutoSize = true;
         lbMiddleMouseWheelUp.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbMiddleMouseWheelUp.Location = new Point(29, 285);
         lbMiddleMouseWheelUp.Name = "lbMiddleMouseWheelUp";
         lbMiddleMouseWheelUp.Size = new Size(160, 17);
         lbMiddleMouseWheelUp.TabIndex = 68;
         lbMiddleMouseWheelUp.Tag = "";
         lbMiddleMouseWheelUp.Text = "Middle mouse wheel up:";
         // 
         // lbMiddleMouseWheelDown
         // 
         lbMiddleMouseWheelDown.AutoSize = true;
         lbMiddleMouseWheelDown.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbMiddleMouseWheelDown.Location = new Point(9, 255);
         lbMiddleMouseWheelDown.Name = "lbMiddleMouseWheelDown";
         lbMiddleMouseWheelDown.Size = new Size(178, 17);
         lbMiddleMouseWheelDown.TabIndex = 67;
         lbMiddleMouseWheelDown.Tag = "";
         lbMiddleMouseWheelDown.Text = "Middle mouse wheel down:";
         // 
         // lbMiddleMouseClick
         // 
         lbMiddleMouseClick.AutoSize = true;
         lbMiddleMouseClick.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbMiddleMouseClick.Location = new Point(58, 227);
         lbMiddleMouseClick.Name = "lbMiddleMouseClick";
         lbMiddleMouseClick.Size = new Size(132, 17);
         lbMiddleMouseClick.TabIndex = 66;
         lbMiddleMouseClick.Tag = "";
         lbMiddleMouseClick.Text = "Middle mouse click:";
         // 
         // lbRightMouseClick
         // 
         lbRightMouseClick.AutoSize = true;
         lbRightMouseClick.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbRightMouseClick.Location = new Point(70, 192);
         lbRightMouseClick.Name = "lbRightMouseClick";
         lbRightMouseClick.Size = new Size(122, 17);
         lbRightMouseClick.TabIndex = 65;
         lbRightMouseClick.Tag = "";
         lbRightMouseClick.Text = "Right mouse click:";
         // 
         // lbLeftMouseClick
         // 
         lbLeftMouseClick.AutoSize = true;
         lbLeftMouseClick.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbLeftMouseClick.Location = new Point(80, 160);
         lbLeftMouseClick.Name = "lbLeftMouseClick";
         lbLeftMouseClick.Size = new Size(113, 17);
         lbLeftMouseClick.TabIndex = 64;
         lbLeftMouseClick.Tag = "";
         lbLeftMouseClick.Text = "Left mouse click:";
         // 
         // lbMoveRight
         // 
         lbMoveRight.AutoSize = true;
         lbMoveRight.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbMoveRight.Location = new Point(114, 133);
         lbMoveRight.Name = "lbMoveRight";
         lbMoveRight.Size = new Size(80, 17);
         lbMoveRight.TabIndex = 63;
         lbMoveRight.Tag = "";
         lbMoveRight.Text = "Move right:";
         // 
         // lbMoveLeft
         // 
         lbMoveLeft.AutoSize = true;
         lbMoveLeft.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbMoveLeft.Location = new Point(124, 102);
         lbMoveLeft.Name = "lbMoveLeft";
         lbMoveLeft.Size = new Size(71, 17);
         lbMoveLeft.TabIndex = 62;
         lbMoveLeft.Tag = "";
         lbMoveLeft.Text = "Move left:";
         // 
         // lbMoveDown
         // 
         lbMoveDown.AutoSize = true;
         lbMoveDown.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbMoveDown.Location = new Point(111, 76);
         lbMoveDown.Name = "lbMoveDown";
         lbMoveDown.Size = new Size(84, 17);
         lbMoveDown.TabIndex = 61;
         lbMoveDown.Tag = "";
         lbMoveDown.Text = "Move down:";
         // 
         // lbMoveUp
         // 
         lbMoveUp.AutoSize = true;
         lbMoveUp.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbMoveUp.Location = new Point(129, 49);
         lbMoveUp.Name = "lbMoveUp";
         lbMoveUp.Size = new Size(66, 17);
         lbMoveUp.TabIndex = 60;
         lbMoveUp.Tag = "";
         lbMoveUp.Text = "Move up:";
         // 
         // lbSettingsType
         // 
         lbSettingsType.AutoSize = true;
         lbSettingsType.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbSettingsType.Location = new Point(612, 15);
         lbSettingsType.Name = "lbSettingsType";
         lbSettingsType.Size = new Size(62, 17);
         lbSettingsType.TabIndex = 105;
         lbSettingsType.Tag = "";
         lbSettingsType.Text = "Settings:";
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
      private Label lbMiddleMouseWheelDown;
      private Label lbMiddleMouseClick;
      private Label lbRightMouseClick;
      private Label lbLeftMouseClick;
      private Label lbMoveRight;
      private Label lbMoveLeft;
      private Label lbMoveDown;
      private Label lbMoveUp;
      private TextBox tbMiddleMouseWheelUp;
      private TextBox tbMiddleMouseWheelDown;
      private TextBox tbMiddleMouseClick;
      private TextBox tbRightMouseClick;
      private TextBox tbLeftMouseClick;
      private TextBox tbMoveRight;
      private TextBox tbMoveLeft;
      private TextBox tbMoveDown;
      private TextBox tbMoveUp;
      private Label lbMiddleMouseWheelUp;
      private ComboBox cmbSelectSettingsType;
      private Button btnSaveBaseKeySet;
      private Button btnSelectBaseKeySet;
      private ComboBox cmbBaseKeysSets;
      private CheckBox cboxMiddleMouseWheelUp;
      private CheckBox cboxMiddleMouseWheelDown;
      private CheckBox cboxMiddleMouseClick;
      private CheckBox cboxRightMouseClick;
      private CheckBox cboxLeftMouseClick;
      private CheckBox cboxMoveLeft;
      private CheckBox cboxMoveRight;
      private CheckBox cboxMoveDown;
      private CheckBox cboxMoveUp;
      private Label lbBaseKeysSetName;
      private TextBox tbBaseKeysSetName;
      private Button btnDeleteBaseKeysSetname;
      private Button btnCreateBaseKeysSetname;
      private Label lbBaseKeysAlternative;
      private Label lbBaseKeysMain;
      private TextBox tbAltMiddleMouseWheelUp;
      private TextBox tbAltMiddleMouseWheelDown;
      private TextBox tbAltMiddleMouseClick;
      private TextBox tbAltRightMouseClick;
      private TextBox tbAltLeftMouseClick;
      private TextBox tbAltMoveRight;
      private TextBox tbAltMoveLeft;
      private TextBox tbAltMoveDown;
      private TextBox tbAltMoveUp;
      private Label lbSettingsType;
   }
}
