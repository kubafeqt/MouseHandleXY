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
         lbDelayMsDescription = new Label();
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
         panelPreviewImport = new Panel();
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
         ((System.ComponentModel.ISupportInitialize)nmDelayMs).BeginInit();
         ((System.ComponentModel.ISupportInitialize)dgvShowKeysPositions).BeginInit();
         panelMain.SuspendLayout();
         panelPreviewImport.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)dgvShowKeysPositions_Preview).BeginInit();
         SuspendLayout();
         // 
         // cboxOnStartup
         // 
         cboxOnStartup.AutoSize = true;
         cboxOnStartup.Location = new Point(23, 24);
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
         // lbDelayMsDescription
         // 
         lbDelayMsDescription.AutoSize = true;
         lbDelayMsDescription.Font = new Font("Segoe UI Semibold", 10.181818F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbDelayMsDescription.Location = new Point(23, 96);
         lbDelayMsDescription.Name = "lbDelayMsDescription";
         lbDelayMsDescription.Size = new Size(79, 19);
         lbDelayMsDescription.TabIndex = 3;
         lbDelayMsDescription.Text = "description";
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
         btnSetKeyPos.Location = new Point(23, 152);
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
         btnShowKeysPositions.Location = new Point(119, 152);
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
         dgvShowKeysPositions.Location = new Point(33, 196);
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
         dgvShowKeysPositions.KeyDown += dgvShowKeysPositions_KeyDown;
         // 
         // lbSetKeyPos
         // 
         lbSetKeyPos.AutoSize = true;
         lbSetKeyPos.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbSetKeyPos.Location = new Point(268, 155);
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
         cboxShowSetKeyPos.Location = new Point(159, 24);
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
         btnDeleteKey.Location = new Point(458, 151);
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
         btnEditPosition.Location = new Point(749, 151);
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
         lbKeyPos.Location = new Point(615, 131);
         lbKeyPos.Name = "lbKeyPos";
         lbKeyPos.Size = new Size(34, 17);
         lbKeyPos.TabIndex = 12;
         lbKeyPos.Tag = "EditPos";
         lbKeyPos.Text = "Key:";
         lbKeyPos.Visible = false;
         // 
         // tbPosX
         // 
         tbPosX.Location = new Point(638, 152);
         tbPosX.Name = "tbPosX";
         tbPosX.Size = new Size(42, 23);
         tbPosX.TabIndex = 13;
         tbPosX.Tag = "EditPos";
         tbPosX.Visible = false;
         // 
         // tbPosY
         // 
         tbPosY.Location = new Point(703, 152);
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
         lbPosX.Location = new Point(615, 155);
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
         lbPosY.Location = new Point(681, 155);
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
         panelMain.Controls.Add(cboxOnStartup);
         panelMain.Controls.Add(btnImport);
         panelMain.Controls.Add(nmDelayMs);
         panelMain.Controls.Add(btnExport);
         panelMain.Controls.Add(lbDelayMs);
         panelMain.Controls.Add(lbMouseControl);
         panelMain.Controls.Add(lbDelayMsDescription);
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
         panelMain.Location = new Point(12, 12);
         panelMain.Name = "panelMain";
         panelMain.Size = new Size(1003, 504);
         panelMain.TabIndex = 30;
         panelMain.Tag = "bigPanels";
         // 
         // panelPreviewImport
         // 
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
         panelPreviewImport.Location = new Point(116, 568);
         panelPreviewImport.Name = "panelPreviewImport";
         panelPreviewImport.Size = new Size(839, 639);
         panelPreviewImport.TabIndex = 31;
         panelPreviewImport.Tag = "bigPanels";
         panelPreviewImport.Visible = false;
         // 
         // btnImportSet_Preview
         // 
         btnImportSet_Preview.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnImportSet_Preview.Location = new Point(442, 74);
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
         btnBackToJsonSelect_Preview.Location = new Point(644, 74);
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
         btnImportAll_Preview.Location = new Point(543, 74);
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
         lbShowedSetName_Preview.Location = new Point(480, 48);
         lbShowedSetName_Preview.Name = "lbShowedSetName_Preview";
         lbShowedSetName_Preview.Size = new Size(117, 17);
         lbShowedSetName_Preview.TabIndex = 54;
         lbShowedSetName_Preview.Tag = "";
         lbShowedSetName_Preview.Text = "ShowedSetname: ";
         // 
         // btnShowSetName_Preview
         // 
         btnShowSetName_Preview.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
         btnShowSetName_Preview.Location = new Point(728, 14);
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
         dgvShowKeysPositions_Preview.Location = new Point(20, 115);
         dgvShowKeysPositions_Preview.MultiSelect = false;
         dgvShowKeysPositions_Preview.Name = "dgvShowKeysPositions_Preview";
         dgvShowKeysPositions_Preview.RowHeadersWidth = 42;
         dgvShowKeysPositions_Preview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
         dgvShowKeysPositions_Preview.Size = new Size(791, 508);
         dgvShowKeysPositions_Preview.TabIndex = 37;
         dgvShowKeysPositions_Preview.SelectionChanged += dgvShowKeysPositions_Preview_SelectionChanged;
         // 
         // cmbSelectSetName_Preview
         // 
         cmbSelectSetName_Preview.DropDownStyle = ComboBoxStyle.DropDownList;
         cmbSelectSetName_Preview.FormattingEnabled = true;
         cmbSelectSetName_Preview.Location = new Point(560, 14);
         cmbSelectSetName_Preview.Name = "cmbSelectSetName_Preview";
         cmbSelectSetName_Preview.Size = new Size(162, 23);
         cmbSelectSetName_Preview.TabIndex = 50;
         cmbSelectSetName_Preview.Tag = "";
         // 
         // lbSetName_Preview
         // 
         lbSetName_Preview.AutoSize = true;
         lbSetName_Preview.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
         lbSetName_Preview.Location = new Point(480, 16);
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
         // MainForm
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(1244, 819);
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
         ResumeLayout(false);
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
      private CheckBox checkBox1;
      private NumericUpDown numericUpDown1;
      private Label label1;
      private Label label2;
      private Label label3;
      private Label label4;
      private Button button3;
      private Label lbShowedSetName_Preview;
      private Button button4;
      private Button btnShowSetName_Preview;
      private Button button6;
      private Button button7;
      private DataGridView dgvShowKeysPositions_Preview;
      private Button button8;
      private Label label6;
      private ComboBox cmbSelectSetName_Preview;
      private CheckBox checkBox2;
      private Button button9;
      private Button button10;
      private TextBox textBox1;
      private Button button11;
      private Label lbSetName_Preview;
      private Label lbKeyPos_Preview;
      private Label lbPosY_Preview;
      private TextBox tbPosX_Preview;
      private Label lbPosX_Preview;
      private TextBox tbPosY_Preview;
      private Button btnImportAll_Preview;
      private Button btnImportSet_Preview;
      private Button btnBackToJsonSelect_Preview;
   }
}
