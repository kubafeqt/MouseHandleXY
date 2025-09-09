using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MouseXY
{
   public partial class MainForm : Form
   {
      private NotifyIcon trayIcon;
      private ContextMenuStrip trayMenu;
      string appName = "MouseHandleXY";
      System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

      #region Loading MainForm
      public MainForm()
      {
         InitializeComponent();

         Size = Settings.defaultFormSize; //this.Size

         // Vytvoření tray menu
         trayMenu = new ContextMenuStrip();
         trayMenu.Items.Add("Zobrazit", null, OnShow);
         trayMenu.Items.Add("Ukončit", null, OnExit);

         // Vytvoření tray ikony
         trayIcon = new NotifyIcon();
         trayIcon.Text = "Mouse controlled by keyboard";
         trayIcon.Icon = SystemIcons.Application; //lze nahradit vlastní ikonou
         trayIcon.ContextMenuStrip = trayMenu;
         trayIcon.Visible = true;
         trayIcon.DoubleClick += OnShow;

         // Událost pro minimalizaci
         Resize += OnResize;

         // Skryj okno po startu
         Load += (s, e) => Hide();
         FormClosing += OnFormClosing;

         timer.Interval = 50;
         timer.Tick += timer_tick;

         #region events
         // event for change button enabled state when mouse cursor is controlled by keyboard or not
         MouseHandle.OnMouseCursorHandleOpenChanged += (mouseCursorHandle) =>
         {
            EnableDisableControlsOfTagInPanel(panelMain, "MouseControlDisable", !mouseCursorHandle); // Enable/disable controls for editing positions of keys
            lbMouseControl.Visible = mouseCursorHandle; // Zobrazí nebo skryje popisek pro ovládání myši
            lbMouseControl.Text = mouseCursorHandle ? "Mouse control is ON" : "Mouse control is OFF"; // Změní text popisku podle stavu ovládání myši
            ShowControlsOfTagInPanel(panelMain, "ExpImp", !mouseCursorHandle);
            if (mouseCursorHandle)
            {
               MouseHandle.setKeyToPos = false; // reset key to position after mouse cursor is controlled by keyboard
            }
         };

         //event for set key to position of mouse cursor
         MouseHandle.OnSetKeyToPos += () =>
         {
            SwitchSetKeyPos();
            UpdateDataGridView(); // Aktualizace DataGridView s pozicemi kláves
            if (cboxShowSetKeyPos.Checked && !showKeysPositions)
            {
               ShowKeysPositions();
            }
         };

         //event after importing json file
         ExportImport.OnFileImport += () =>
         {
            cmbSelectSetname.Items.Clear();
            LoadComboBoxSetNames();
            cmbSelectSetname.SelectedIndex = 0;
            UpdateDataGridView();
            if (cboxShowSetKeyPos.Checked && !showKeysPositions)
            {
               ShowKeysPositions();
            }
            btnBackToPreview.Hide();
            DBAccess.SaveOrUpdateAllKeyPos();
            DBAccess.SaveAllSetNames();
         };

         //event for preview json file
         ExportImport.OnPreview += () =>
         {
            panelMain.Hide();
            panelPreviewImport.Show();
            Settings.latestSize = Size;
            Size = Settings.biggerFormSize;
            lbFileName_Preview.Text = $"FileName: {KeyPos_Preview.selectedFileName}";
            UpdatePreviewDataGridView();
            LoadComboBoxPreviewSetNames();
         };

         #endregion

      }

      private void MainForm_Load(object sender, EventArgs e)
      {
         cboxOnStartup.Checked = StartupManager.IsInStartup(appName); // Nastaví CheckBox podle toho jestli je aplikace zapsaná v registrech pro spouštění
         lbDescriptionControl.Text = "double left control to open/close mouse control by keyboard\nleft shift to change speed of mouse step to slower\nleft alt to change speed of mouse step to faster";

         #region DB_loading
         //DBAccess.ConnectionTest();
         DBAccess.LoadAll(); // Načte všechny klávesy a jejich pozice, setNames a Settings z databáze
         KeyPos.UpdateKeyPosDict(); // aktualizuje/načte dictionary pozic kláves z KeyPositions
         nmDelayMs.Value = Settings.delayMs;
         cboxShowSetKeyPos.Checked = Settings.showDgvAfterSetKeyPos;

         #endregion

         #region BaseKeys and Settings Panel loading
         foreach (var ctrl in panelBaseKeysSettings.Controls.OfType<CheckBox>().OfTag("baseKeysCheckbox"))
         {
            ctrl.CheckedChanged += BaseKeysCheckBoxes_CheckedChanged;
         }
         foreach (var ctrl in panelBaseKeysSettings.Controls.OfType<TextBox>().OfTag("BaseKeysSettingsTbs"))
         {
            ctrl.KeyDown += BaseKeysTextBoxes_KeyDown;
         }
         cmbBaseKeysSets.Items.Add("default");
         cmbBaseKeysSets.Items.Add("second");
         cmbBaseKeysSets.SelectedIndex = 0;
         cmbCreateSetFrom.Items.Add("none");
         cmbCreateSetFrom.Items.Add("default");
         cmbCreateSetFrom.SelectedIndex = 0;
         cmbSelectSettingsType.Items.AddRange(new string[] { "Base Keys", "Sounds", "Other Settings" });
         cmbSelectSettingsType.SelectedIndex = 0;
         new BaseKeys("default"); // inicializace default baseKeys
         new BaseKeys("second"); // inicializace second baseKeys
         BaseKeys.ChangeSelectedBaseKeys("default"); //then load this from db
         BaseKeys.ChangeShowedBaseKeys("default"); //then load this from db
         FillKeybindTextBoxes(); // basic method for fill text boxes with keybinds
         BaseKeys.LoadDefaultKeyActionsEnabledDict(); // load default enabled actions
         BaseKeys.LoadKeyActionsEnabled();
         ChangeBaseKeysCheckBoxesCheckedState(); // change checkboxes checked state according to loaded enabled actions

         #endregion

         ResizeAndLocationControlsOfTag("bigPanels", Settings.panelSize, Settings.panelLocation);
         ResizeAndLocationControlsOfTag("settingsPanels", Settings.settingsSubPanelSize, Settings.settingsSubPanelLocation, panelSettings);
         LoadComboBoxSetNames(); // Načtení názvů setNames do ComboBoxu
         dgvShowKeysPositions.AllowUserToAddRows = false;
         dgvShowKeysPositions.AllowUserToDeleteRows = false;
         UpdateDataGridView(); // Aktualizace DataGridView s pozicemi kláves
         foreach (DataGridViewColumn column in dgvShowKeysPositions.Columns)
         {
            if (column.Name != "IsActive")
            {
               column.ReadOnly = true; //nastaví všechny sloupce který se nejmenujou IsActive na readonly
            }
         }
         dgvShowKeysPositions_Preview.AllowUserToAddRows = false;
         dgvShowKeysPositions_Preview.AllowUserToDeleteRows = false;
         dgvShowKeysPositions_Preview.ReadOnly = true;
      }

      private void LoadComboBoxSetNames()
      {
         cmbSelectSetname.Items.Add("default"); // Přidání výchozího SetName
         lbShowedSetname.Text = $"ShowedSetname: {KeyPos.showedSetName}";
         lbSelectedSetname.Text = $"SelectedSetname: {KeyPos.selectedSetName}";
         cmbSelectSetname.Items.AddRange(KeyPos.SetNamesDict.Values.ToArray());
         //foreach (var setName in KeyPos.setNames.Values)
         //{
         //   cmbSelectSetname.Items.Add(setName);
         //}
         int index = cmbSelectSetname.Items.IndexOf(KeyPos.showedSetName);
         cmbSelectSetname.SelectedIndex = index;
         EnableDisableAddKeyToSetnameButton();
      }

      #endregion

      #region Updating Controls
      private void UpdateDataGridView(int selectedRowIndex = 0)
      {
         BindingSource bs = new BindingSource();
         bs.DataSource = new BindingList<KeyPos>(KeyPos.KeyPositionsList.Where(k => k.SetName == KeyPos.showedSetName).ToList());
         dgvShowKeysPositions.DataSource = bs; // Přiřazení BindingSource do DataGridView
         selectedRowIndex = selectedRowIndex > 0 && dgvShowKeysPositions.RowCount > selectedRowIndex ? selectedRowIndex : --selectedRowIndex;
         if (selectedRowIndex > 0 && selectedRowIndex <= dgvShowKeysPositions.Rows.Count)
         {
            dgvShowKeysPositions.CurrentCell = dgvShowKeysPositions.Rows[selectedRowIndex].Cells[0]; // Nastaví aktuální buňku na vybraný řádek
         }
         foreach (DataGridViewRow row in dgvShowKeysPositions.Rows)
         {
            string keyString = row.Cells["Key"].Value.ToString();
            Keys key = (Keys)Enum.Parse(typeof(Keys), keyString);
            if (BaseKeys.selected.CheckAssignedKeyEnabled(key))
            {
               row.DefaultCellStyle.BackColor = Color.FromArgb(255, 212, 2, 2);
               row.DefaultCellStyle.ForeColor = Color.White;
            }
            else
            {
               row.DefaultCellStyle.BackColor = Color.White;
               row.DefaultCellStyle.ForeColor = Color.Black;
            }
         } 
      }

      private void UpdatePreviewDataGridView()
      {
         BindingSource previewBs = new BindingSource();
         previewBs.DataSource = new BindingList<KeyPos_Preview>(KeyPos_Preview.KeyPositionsList.Where(k => k.SetName == KeyPos_Preview.showedSetName).ToList());
         dgvShowKeysPositions_Preview.DataSource = previewBs;
      }

      private void LoadComboBoxPreviewSetNames()
      {
         cmbSelectSetName_Preview.Items.Clear();
         cmbSelectSetName_Preview.Items.AddRange(KeyPos_Preview.SetNamesDict.Values.ToArray());
         cmbSelectSetName_Preview.SelectedIndex = 0;
         lbShowedSetName_Preview.Text = $"ShowedSetname: {KeyPos_Preview.showedSetName}";
      }

      private void ShowControlsOfTagInPanel(Panel parentPanel, string tag, bool show = true, bool onKeysPositions = false)
      {
         var matchingControls = parentPanel.Controls.OfType<Control>().Where(c => c.Tag is string s && s.IndexOf(tag, StringComparison.OrdinalIgnoreCase) >= 0);
         foreach (var control in matchingControls)
         {
            control.Visible = !onKeysPositions ? show : !show ? show : showKeysPositions;
         }
      }

      private void EnableDisableControlsOfTagInPanel(Panel parentPanel, string tag, bool enable = true)
      {
         var matchingControls = parentPanel.Controls.OfType<Control>().Where(c => c.Tag is string s && s.IndexOf(tag, StringComparison.OrdinalIgnoreCase) >= 0);
         foreach (var control in matchingControls)
         {
            control.Enabled = enable;
         }
      }

      private void ResizeAndLocationControlsOfTag(string tag, Size? size = null, Point? location = null, Panel? parentPanel = null)
      {
         var matchingControls = parentPanel == null ?
            Controls.OfType<Control>().Where(c => c.Tag is string s && s.IndexOf(tag, StringComparison.OrdinalIgnoreCase) >= 0) :
            parentPanel.Controls.OfType<Control>().Where(c => c.Tag is string s && s.IndexOf(tag, StringComparison.OrdinalIgnoreCase) >= 0);
         foreach (var control in matchingControls)
         {
            control.Size = size ?? control.Size;
            control.Location = location ?? control.Location;
         }
      }

      #endregion

      #region FormControl
      private void OnResize(object sender, EventArgs e)
      {
         if (this.WindowState == FormWindowState.Minimized)
         {
            this.Hide(); // Schovej okno
         }
      }

      protected override void OnShown(EventArgs e)
      {
         base.OnShown(e);
         this.Hide(); // Okno se nespustí viditelné
      }

      private void OnShow(object sender, EventArgs e)
      {
         this.Show();
         this.WindowState = FormWindowState.Normal;
         this.BringToFront();
      }

      private void OnExit(object sender, EventArgs e)
      {
         trayIcon.Visible = false;
         Application.Exit();
      }

      private void OnFormClosing(object sender, FormClosingEventArgs e)
      {
         trayIcon.Visible = false;
      }

      private void ResizeMainForm(bool bigger = true, bool latestSize = false, bool saveLatestFormSize = true, Size? definedBiggerFormSize = null)
      {
         if (bigger && !latestSize)
         {
            Size = definedBiggerFormSize ?? Settings.biggerFormSize;
         }
         else if (!latestSize)
         {
            Size = Settings.defaultFormSize;
         }
         if (latestSize)
         {
            Size = Settings.latestSize != Size.Empty ? Settings.latestSize : Settings.defaultFormSize;
         }
         if (saveLatestFormSize)
         {
            Settings.latestSize = Size;
         }
      }

      #endregion

      #region General Controls
      private void cboxOnStartup_CheckedChanged(object sender, EventArgs e)
      {
         string appPath = Application.ExecutablePath;
         StartupManager.SetStartup(cboxOnStartup.Checked, appName, appPath);
      }

      private void btnAcceptDelayMs_Click(object sender, EventArgs e)
      {
         Settings.delayMs = (int)nmDelayMs.Value;
         DBAccess.SaveSettings();
      }

      private void btnSetKeyPos_Click(object sender, EventArgs e)
      {
         SwitchSetKeyPos();
      }

      private void SwitchSetKeyPos()
      {
         MouseHandle.setKeyToPos = !MouseHandle.setKeyToPos; //then play sound when disabled
         timer.Enabled = MouseHandle.setKeyToPos; // start or stop timer
         lbSetKeyPos.Visible = MouseHandle.setKeyToPos;
         if (!MouseHandle.setKeyToPos)
         {
            Sounds.PlaySound();
         }
      }

      private void timer_tick(object sender, EventArgs e) //showing SetKey Position, enabling/disabling only in SetKeyPos method
      {
         lbSetKeyPos.Text = $"(open) X: {Cursor.Position.X}, Y: {Cursor.Position.Y}";
      }

      public static bool showKeysPositions = false;
      private void btnShowKeysPositions_Click(object sender, EventArgs e)
      {
         ShowKeysPositions();
      }

      private void ShowKeysPositions()
      {
         showKeysPositions = !showKeysPositions;
         dgvShowKeysPositions.Visible = showKeysPositions;
         ShowControlsOfTagInPanel(panelMain, "EditPos", onKeysPositions: true);
         if (showKeysPositions)
         {
            ResizeMainForm();
            UpdateDataGridView();
            btnShowKeysPositions.Text = btnShowKeysPositions.Text.Replace("Show", "hide", StringComparison.OrdinalIgnoreCase);
         }
         else
         {
            ResizeMainForm(false);
            btnShowKeysPositions.Text = btnShowKeysPositions.Text.Replace("Hide", "show", StringComparison.OrdinalIgnoreCase);
         }
      }

      private void btnDeleteKey_Click(object sender, EventArgs e)
      {
         DeleteKeyFromSetname();
      }

      private void DeleteKeyFromSetname()
      {
         if (dgvShowKeysPositions.SelectedRows.Count > 0) //selected row in DataGridView delete and update datagridview
         {
            Keys key = (Keys)Enum.Parse(typeof(Keys), dgvShowKeysPositions.SelectedRows[0].Cells["Key"].Value.ToString());

            var confirm = MessageBox.Show("Opravdu chcete smazat tento záznam?", "Potvrzení", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes) // Odstranění řádku z DataGridView
            {
               if (KeyPos.showedSetName == KeyPos.selectedSetName)
               {
                  KeyPos.KeysPositionDict.Remove(key); // Odstranění klávesy z mapy pozic
               }
               KeyPos.KeyPositionsList.RemoveAll(k => k.Key == key.ToString() && k.SetName == KeyPos.showedSetName); // Odstranění záznamu z listu KeyPositions
               DBAccess.DeleteKey(key);
               int selectedRowIndex = dgvShowKeysPositions.CurrentCell.RowIndex;
               UpdateDataGridView(selectedRowIndex); // Aktualizace DataGridView s pozicemi kláves
            }
         }
         else
         {
            MessageBox.Show("Nejprve vyberte řádek ke smazání.");
         }
      }

      private void btnEditPosition_Click(object sender, EventArgs e)
      {
         int maxX, maxY;
         MaxScreenSize(out maxX, out maxY);
         if (int.TryParse(tbPosX.Text, out int posX) && int.TryParse(tbPosY.Text, out int posY) && posX >= 0 && posY >= 0 && posX <= maxX && posY <= maxY)
         {
            if (dgvShowKeysPositions.SelectedRows.Count > 0)
            {
               string keyString = dgvShowKeysPositions.SelectedRows[0].Cells["Key"].Value.ToString();
               Keys key = (Keys)Enum.Parse(typeof(Keys), keyString);
               Point newPosition = new Point(posX, posY);
               KeyPos.CreateUpdateKeyPosition(keyString, newPosition, false); // Aktualizace pozice v KeyPos
               DBAccess.SaveOrUpdateKeyPos(key, newPosition, KeyPos.showedSetName); // Uložení změn do databáze
               KeyPos.UpdateKeyPosDict(key);
               UpdateDataGridView(); // Aktualizace DataGridView s pozicemi kláves
            }
            else
            {
               MessageBox.Show("Nejprve vyberte řádek k úpravě.");
            }
         }
         else
         {
            MessageBox.Show($"Zadejte platné nezáporné hodnotny pro souřadnice X a Y, které nejsou větší, než maximální rozsah displejů. - X: {maxX}, Y: {maxY}");
         }
      }

      private void MaxScreenSize(out int maxX, out int maxY)
      {
         maxX = 0;
         maxY = 0;
         foreach (var screen in Screen.AllScreens)
         {
            if (screen.Bounds.Right > maxX)
               maxX = screen.Bounds.Right;
            if (screen.Bounds.Bottom > maxY)
               maxY = screen.Bounds.Bottom;
         }
      }

      private void cboxShowSetKeyPos_CheckedChanged(object sender, EventArgs e)
      {
         Settings.showDgvAfterSetKeyPos = cboxShowSetKeyPos.Checked;
         DBAccess.SaveSettings();
      }

      #endregion

      #region DataGridView events
      private void dgvShowKeysPositions_SelectionChanged(object sender, EventArgs e)
      {
         if (dgvShowKeysPositions.SelectedRows.Count > 0)
         {
            lbKeyPos.Text = $"Key: {dgvShowKeysPositions.SelectedRows[0].Cells["Key"].Value} - ";
            string rawValue = dgvShowKeysPositions.SelectedRows[0].Cells["Position"].Value.ToString();
            var matches = Regex.Matches(rawValue, @"\d+");
            if (matches.Count == 2)
            {
               tbPosX.Text = matches[0].Value;
               tbPosY.Text = matches[1].Value;
            }
         }
      }

      private void dgvShowKeysPositions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
      {
         if (dgvShowKeysPositions.Columns[e.ColumnIndex].Name == "IsActive")
         {
            bool isActive = (bool)dgvShowKeysPositions.Rows[e.RowIndex].Cells["IsActive"].Value; // Získání hodnoty buňky IsActive
            Keys key = (Keys)Enum.Parse(typeof(Keys), dgvShowKeysPositions.Rows[e.RowIndex].Cells["Key"].Value.ToString()); // Získání klávesy z buňky Key
            KeyPos k = KeyPos.KeyPositionsList.Find(k => k.Key == key.ToString());
            if (k != null)
            {
               k.IsActive = isActive; // Aktualizace stavu IsActive v objektu KeyPos
               DBAccess.SaveOrUpdateKeyPos(key, k.Position, KeyPos.showedSetName, isActive); // Uložení změn do databáze
            }
         }
      }

      private void dgvShowKeysPositions_CurrentCellDirtyStateChanged(object sender, EventArgs e)
      {
         if (dgvShowKeysPositions.IsCurrentCellDirty)
         {
            dgvShowKeysPositions.CommitEdit(DataGridViewDataErrorContexts.Commit);
         }
      }

      private void dgvShowKeysPositions_KeyDown(object sender, KeyEventArgs e)
      {
         Keys k = e.KeyCode;
         if (k == Keys.Delete) // Pokud je stisknuto Delete, smaž vybranou pozici
         {
            DeleteKeyFromSetname();
         }
         else if (k == Keys.Escape) // Pokud je stisknuto Escape, zruš výběr
         {
            dgvShowKeysPositions.ClearSelection();
         }
      }

      #endregion

      #region SetNames Controls
      private void btnAddSetname_Click(object sender, EventArgs e)
      {
         AddSetName();
      }

      private void AddSetName()
      {
         if (!btnAddSetname.Text.Equals("Edit", StringComparison.OrdinalIgnoreCase))
         {
            int newId = KeyPos.PossibleFreeIdInDictKeys(KeyPos.SetNamesDict); //Získání nového ID pro SetName
            string setName = tbSetname.Text != string.Empty ? tbSetname.Text.ToLower().Trim() : InputBox.Show("Zadejte název pro nový SetName:", "Přidat nový SetName", $"SetName {newId}");
            if (!string.IsNullOrWhiteSpace(setName))
            {
               if (cmbSelectSetname.Items.Contains(setName))
               {
                  MessageBox.Show($"Setname {setName} již existuje. Zvolte jiný název.");
                  return;
               }
               KeyPos.SetNamesDict[newId] = setName; //Přidání nového názvu do slovníku setNames
               cmbSelectSetname.Items.Add(setName); //Aktualizace ComboBoxu s názvy nastavení
               cmbSelectSetname.SelectedItem = setName; //Nastaví právě přidaný název jako vybraný
               ShowSetname(); //Nastaví aktuálně zobrazený setName
               latestSelecedItem = null;
               tbSetname.Text = string.Empty;
               SetNameService.SaveOrUpdateSetName(newId, setName);
            }
            else
            {
               MessageBox.Show("Název setName nesmí být prázdný.");
            }
         }
         else //edit SetName
         {
            string setName = tbSetname.Text.Trim().ToLower();
            string? newSetName = InputBox.Show($"Zadejte nový název pro {setName}:", "Změnit název setname", nullable: true);
            if (!string.IsNullOrWhiteSpace(newSetName))
            {
               if (cmbSelectSetname.Items.Contains(newSetName) && newSetName != setName)
               {
                  MessageBox.Show($"Setname {newSetName} již existuje. Zvolte jiný název.");
                  return;
               }
               int id = KeyPos.SetNamesDict.FirstOrDefault(x => x.Value == setName).Key; // Získání ID pro stávající setName
               KeyPos.SetNamesDict[id] = newSetName;
               int index = cmbSelectSetname.Items.IndexOf(setName);
               cmbSelectSetname.Items[index] = newSetName; // Aktualizace položky v ComboBoxu
               tbSetname.Text = string.Empty; // Vyprázdní TextBox
               SetNameService.SaveOrUpdateSetName(id, newSetName, setName);
               if (KeyPos.selectedSetName == setName)
               {
                  SelectSetname(newSetName);
               }
               ShowSetname(); // Nastaví aktuálně zobrazený setName
            }
            else if (newSetName != null) // pokud uživatel zruší dialog
            {
               DialogResult result = MessageBox.Show(
                   $"Chcete smazat setname: {setName} se všemi jeho hotkeys?", // text zprávy
                   $"Potvrzení smazání {setName}", // titulek okna
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question
               );

               if (result == DialogResult.Yes) //delete set name se všemi hotkeys
               {
                  int id = KeyPos.SetNamesDict.FirstOrDefault(x => x.Value == setName).Key; // Získání ID pro stávající setName
                  cmbSelectSetname.Items.Remove(setName);
                  tbSetname.Text = string.Empty;
                  SetNameService.DeleteSetNameAndItKeysById(id, setName); // Smazání setName z databáze
                  MessageBox.Show($"Setname: {setName} byl smazán se všemi jeho uloženými hotkeys.");
                  cmbSelectSetname.SelectedIndex = cmbSelectSetname.Items.Count - 1;
                  if (KeyPos.selectedSetName == setName)
                  {
                     SelectSetname("default");
                  }
                  ShowSetname(); // Nastaví aktuálně zobrazený setName
               }
            }
         }
      }

      private void cmbSelectSetname_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (string.IsNullOrWhiteSpace(tbSetname.Text) || cmbSelectSetname.Items.Contains(tbSetname.Text.Trim()))
         {
            tbSetname.Text = cmbSelectSetname.SelectedItem?.ToString() != "default" ? cmbSelectSetname.SelectedItem?.ToString() : string.Empty ?? string.Empty;
            // Nastaví text v TextBoxu na vybraný název z ComboBoxu
         }
         if (tbSetname.Text != cmbSelectSetname.SelectedItem.ToString())
         {
            latestSelecedItem = cmbSelectSetname.SelectedItem?.ToString() ?? "default";
         }
      }

      string? latestSelecedItem = null;
      private void tbSetname_TextChanged(object sender, EventArgs e)
      {
         tbSetname.Text = tbSetname.Text.TrimStart();
         string setName = tbSetname.Text.Trim().ToLower();
         if (setName != "default" && cmbSelectSetname.Items.Contains(setName))
         {
            btnAddSetname.Enabled = MouseHandle.mouseCursorHandle ? false : true;
            btnAddSetname.Text = "Edit"; // Pokud je název již v ComboBoxu, změní text tlačítka na "Edit"
            if (tbSetname.Text.Length > 0 && !cmbSelectSetname.Items.Contains(tbSetname.Text.Substring(0, tbSetname.Text.Length - 1)))
            {
               latestSelecedItem = cmbSelectSetname.SelectedItem?.ToString() ?? "default";
            }
            cmbSelectSetname.SelectedItem = setName;
         }
         else if (setName != "default")
         {
            cmbSelectSetname.SelectedItem = cmbSelectSetname.SelectedItem == "default" ? cmbSelectSetname.SelectedItem : latestSelecedItem ?? cmbSelectSetname.SelectedItem;
            btnAddSetname.Enabled = MouseHandle.mouseCursorHandle ? false : true;
            btnAddSetname.Text = "Add"; // Pokud název není v ComboBoxu, změní text tlačítka na "Add"
         }
         else
         {
            btnAddSetname.Enabled = false;
         }
      }

      private void tbSetname_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyValue == (int)Keys.Enter)
         {
            e.SuppressKeyPress = true; // Zabráníme zvuku Enteru
            AddSetName(); // Simulujeme kliknutí na tlačítko pro přidání/úpravu setName
         }
      }

      private void btnShowSetname_Click(object sender, EventArgs e)
      {
         ShowSetname();
      }

      private void ShowSetname()
      {
         KeyPos.showedSetName = cmbSelectSetname.SelectedItem?.ToString(); //nastaví aktuálně zobrazený setName
         lbShowedSetname.Text = $"ShowedSetname: {cmbSelectSetname.SelectedItem}";
         EnableDisableAddKeyToSetnameButton();
         UpdateDataGridView();
      }

      private void btnSelectSetname_Click(object sender, EventArgs e)
      {
         SelectSetname(cmbSelectSetname.SelectedItem.ToString());
      }

      private void SelectSetname(string? selectedSetName)
      {
         if (selectedSetName != null)
         {
            KeyPos.selectedSetName = cmbSelectSetname.SelectedItem.ToString();
            lbSelectedSetname.Text = $"SelectedSetname: {cmbSelectSetname.SelectedItem}";
            EnableDisableAddKeyToSetnameButton();
            KeyPos.UpdateKeyPosDict();
            DBAccess.SaveSettings();
         }
         else
         {
            MessageBox.Show("Nejprve vyberte setName.");
         }
      }

      /// <summary>
      /// disable Add Key to setName button when selected setName matches showed setName
      /// </summary>
      private void EnableDisableAddKeyToSetnameButton() => btnAddKeyToSelectedSetname.Enabled = KeyPos.selectedSetName != KeyPos.showedSetName ? true : false;

      private void btnAddKeyToSelectedSetname_Click(object sender, EventArgs e)
      {
         if (dgvShowKeysPositions.SelectedRows.Count > 0)
         {
            string key = dgvShowKeysPositions.SelectedRows[0].Cells["Key"].Value.ToString();
            Point position = (Point)dgvShowKeysPositions.SelectedRows[0].Cells["Position"].Value;
            KeyPos.AddKeyToSelectedSetname(key, position); //převést to do seletectedSetName
         }
      }


      #endregion

      #region Export and Import
      private void btnExport_Click(object sender, EventArgs e)
      {
         ExportImport.ExportToJson();
      }

      private void btnImport_Click(object sender, EventArgs e)
      {
         ExportImport.ImportFromJson();
      }

      //preview panel:
      private void btnShowSetName_Preview_Click(object sender, EventArgs e)
      {
         KeyPos_Preview.showedSetName = cmbSelectSetName_Preview.SelectedItem?.ToString(); //nastaví aktuálně zobrazený setName
         lbShowedSetName_Preview.Text = $"ShowedSetname: {KeyPos_Preview.showedSetName}";
         UpdatePreviewDataGridView();
      }

      private void btnImportSet_Preview_Click(object sender, EventArgs e)
      {
         ExportImport.ImportSet();
      }

      private void btnImportAll_Preview_Click(object sender, EventArgs e)
      {
         btnBackToPreview.Hide();
         ExitPreviewPanel();
         ExportImport.ImportFromJson(KeyPos_Preview.selectedFileName);
         KeyPos_Preview.selectedFileName = string.Empty;
      }

      private void btnBackToJsonSelect_Preview_Click(object sender, EventArgs e)
      {
         btnBackToPreview.Hide();
         ExitPreviewPanel(true);
         ExportImport.ImportFromJson();
      }

      private void btnExit_Preview_Click(object sender, EventArgs e)
      {
         btnBackToPreview.Show();
         ExitPreviewPanel(true);
      }

      private void ExitPreviewPanel(bool latestSize = false)
      {
         panelMain.Show();
         panelPreviewImport.Hide();
         if (latestSize)
         {
            Size = Settings.latestSize;
         }
      }

      private void btnBackToPreview_Click(object sender, EventArgs e)
      {
         panelMain.Hide();
         panelPreviewImport.Show();
         Size = Settings.biggerFormSize;
      }

      private void dgvShowKeysPositions_Preview_SelectionChanged(object sender, EventArgs e)
      {
         if (dgvShowKeysPositions_Preview.SelectedRows.Count > 0)
         {
            lbKeyPos_Preview.Text = $"Key: {dgvShowKeysPositions_Preview.SelectedRows[0].Cells["Key"].Value} - ";
            string rawValue = dgvShowKeysPositions_Preview.SelectedRows[0].Cells["Position"].Value.ToString();
            var matches = Regex.Matches(rawValue, @"\d+");
            if (matches.Count == 2)
            {
               tbPosX_Preview.Text = matches[0].Value;
               tbPosY_Preview.Text = matches[1].Value;
            }
         }
      }

      #endregion

      #region Panel Switching
      private void btnMainPanels_Click(object sender, EventArgs e)
      {
         Panel mainPanel = lastPanel ?? panelMain; //determine if mainPanel was on preview import or main panel last time
         ResizeMainForm(latestSize: true, saveLatestFormSize: false);
         SwitchPanels(mainPanel);
         UpdateDataGridView();
      }

      Panel? lastPanel;
      private void btnSettings_Click(object sender, EventArgs e)
      {
         lastPanel = GetLastPanelVisible();
         ResizeMainForm(bigger: true, saveLatestFormSize: false, definedBiggerFormSize: Settings.settingsFormSize);
         SwitchPanels(panelSettings);
      }

      private void SwitchPanels(Panel showPanel)
      {
         foreach (Panel panel in Controls.OfType<Panel>().OfTag("bigpanels"))
         {
            panel.Hide();
         }
         showPanel.Show();
      }

      private Panel? GetLastPanelVisible()
      {
         foreach (Panel panel in Controls.OfType<Panel>().OfTag("bigPanels"))
         {
            if (panel.Visible && panel != panelSettings)
            {
               return panel;
            }
         }
         return lastPanel;
      }

      //Settings panel:
      private void cmbSelectSettingsType_SelectedIndexChanged(object sender, EventArgs e)
      {
         Dictionary<string, Panel> settingsTypeToPanelDict = new Dictionary<string, Panel>
         {
            { "", panelBaseKeysSettings }, //default
            { "Base Keys", panelBaseKeysSettings },
            { "Sounds", panelSoundsSettings },
            { "Other Settings", panelOtherSettings }
         };
         if (cmbSelectSettingsType.SelectedItem != null && settingsTypeToPanelDict.ContainsKey(cmbSelectSettingsType.SelectedItem.ToString() ?? ""))
         {
            SwitchSettingsPanel(settingsTypeToPanelDict[cmbSelectSettingsType.SelectedItem.ToString() ?? ""]);
         }
      }

      private void SwitchSettingsPanel(Panel showPanel)
      {
         foreach (Panel panel in panelSettings.Controls.OfType<Panel>().OfTag("settingsPanels"))
         {
            panel.Hide();
         }
         showPanel.Show();
      }

      #endregion

      #region BaseKeys Settings
      private void BaseKeysCheckBoxes_CheckedChanged(object sender, EventArgs e)
      {
         CheckBox? cbox = sender as CheckBox;
         Dictionary<CheckBox, MouseHandle.mouseActions> checkBoxToMouseActionsDict = new Dictionary<CheckBox, MouseHandle.mouseActions>
         {
            { cboxMoveUp, MouseHandle.mouseActions.goUp },
            { cboxMoveDown, MouseHandle.mouseActions.goDown },
            { cboxMoveLeft, MouseHandle.mouseActions.goLeft },
            { cboxMoveRight, MouseHandle.mouseActions.goRight },
            { cboxLeftMouseClick, MouseHandle.mouseActions.leftMouseClick },
            { cboxRightMouseClick, MouseHandle.mouseActions.rightMouseClick },
            { cboxMiddleMouseClick, MouseHandle.mouseActions.middleMouseClick },
            { cboxMiddleMouseWheelUp, MouseHandle.mouseActions.middleMouseWheelUp },
            { cboxMiddleMouseWheelDown, MouseHandle.mouseActions.middleMouseWheelDown },
         };

         if (cbox != null && BaseKeys.showed != null)
         {
            BaseKeys.showed.KeyActionsEnabledDict[checkBoxToMouseActionsDict[cbox]] = cbox.Checked;
            DBAccess.SaveKeysActionsEnabledDict(BaseKeys.showed.SetName, checkBoxToMouseActionsDict[cbox].ToString(), cbox.Checked);
         }
      }

      private void ChangeBaseKeysCheckBoxesCheckedState()
      {
         if (BaseKeys.showed == null) return;
         var mouseActionsToCheckBoxDict = new Dictionary<MouseHandle.mouseActions, CheckBox>
         {
            { MouseHandle.mouseActions.goUp, cboxMoveUp },
            { MouseHandle.mouseActions.goDown, cboxMoveDown },
            { MouseHandle.mouseActions.goLeft, cboxMoveLeft },
            { MouseHandle.mouseActions.goRight, cboxMoveRight },
            { MouseHandle.mouseActions.leftMouseClick, cboxLeftMouseClick },
            { MouseHandle.mouseActions.rightMouseClick, cboxRightMouseClick },
            { MouseHandle.mouseActions.middleMouseClick, cboxMiddleMouseClick },
            { MouseHandle.mouseActions.middleMouseWheelUp, cboxMiddleMouseWheelUp },
            { MouseHandle.mouseActions.middleMouseWheelDown, cboxMiddleMouseWheelDown },
         };
         foreach (var kvp in mouseActionsToCheckBoxDict)
         {
            var action = kvp.Key;
            var checkBox = kvp.Value;
            if (BaseKeys.showed.KeyActionsEnabledDict.TryGetValue(action, out bool isEnabled))
            {
               checkBox.Checked = isEnabled;
            }
            else
            {
               checkBox.Checked = true; // nebo nějaká výchozí hodnota
            }
         }
      }

      private void BaseKeysTextBoxes_KeyDown(object sender, KeyEventArgs e)
      {
         Dictionary<TextBox, MouseHandle.mouseActions> textBoxToMouseActionsDict = new Dictionary<TextBox, MouseHandle.mouseActions>
         {
            { tbMoveUp, MouseHandle.mouseActions.goUp },
            { tbMoveDown, MouseHandle.mouseActions.goDown },
            { tbMoveLeft, MouseHandle.mouseActions.goLeft },
            { tbMoveRight, MouseHandle.mouseActions.goRight },
            { tbLeftMouseClick, MouseHandle.mouseActions.leftMouseClick },
            { tbRightMouseClick, MouseHandle.mouseActions.rightMouseClick },
            { tbMiddleMouseWheelUp, MouseHandle.mouseActions.middleMouseWheelUp },
            { tbMiddleMouseWheelDown, MouseHandle.mouseActions.middleMouseWheelDown },
            { tbMiddleMouseClick, MouseHandle.mouseActions.middleMouseClick },
            { tbAltMoveUp, MouseHandle.mouseActions.goUp },
            { tbAltMoveDown, MouseHandle.mouseActions.goDown },
            { tbAltMoveLeft, MouseHandle.mouseActions.goLeft },
            { tbAltMoveRight, MouseHandle.mouseActions.goRight },
            { tbAltLeftMouseClick, MouseHandle.mouseActions.leftMouseClick },
            { tbAltRightMouseClick, MouseHandle.mouseActions.rightMouseClick },
            { tbAltMiddleMouseWheelUp, MouseHandle.mouseActions.middleMouseWheelUp },
            { tbAltMiddleMouseWheelDown, MouseHandle.mouseActions.middleMouseWheelDown },
            { tbAltMiddleMouseClick, MouseHandle.mouseActions.middleMouseClick }
         };

         Dictionary<MouseHandle.mouseActions, string> mouseActionsToNamesDict = new Dictionary<MouseHandle.mouseActions, string>()
         {
            { MouseHandle.mouseActions.goUp, "move up" },
            { MouseHandle.mouseActions.goDown, "move down" },
            { MouseHandle.mouseActions.goLeft, "move left" },
            { MouseHandle.mouseActions.goRight, "move right" },
            { MouseHandle.mouseActions.leftMouseClick, "left mouse click" },
            { MouseHandle.mouseActions.rightMouseClick, "right mouse click" },
            { MouseHandle.mouseActions.middleMouseClick, "middle mouse click" },
            { MouseHandle.mouseActions.middleMouseWheelUp, "middle mouse wheel up" },
            { MouseHandle.mouseActions.middleMouseWheelDown, "middle mouse wheel down" },
         };

         var mouseActionsToCheckBoxDict = new Dictionary<MouseHandle.mouseActions, CheckBox>
         {
            { MouseHandle.mouseActions.goUp, cboxMoveUp },
            { MouseHandle.mouseActions.goDown, cboxMoveDown },
            { MouseHandle.mouseActions.goLeft, cboxMoveLeft },
            { MouseHandle.mouseActions.goRight, cboxMoveRight },
            { MouseHandle.mouseActions.leftMouseClick, cboxLeftMouseClick },
            { MouseHandle.mouseActions.rightMouseClick, cboxRightMouseClick },
            { MouseHandle.mouseActions.middleMouseClick, cboxMiddleMouseClick },
            { MouseHandle.mouseActions.middleMouseWheelUp, cboxMiddleMouseWheelUp },
            { MouseHandle.mouseActions.middleMouseWheelDown, cboxMiddleMouseWheelDown },
         };

         if (BaseKeys.showed == null) return;

         if (e.KeyCode == Keys.Delete)
         {
            Keys keyToRemove = (Keys)Enum.Parse(typeof(Keys), (sender as TextBox).Text, true);
            (sender as TextBox).Text = ""; //Clear the TextBox when delete is pressed
            BaseKeys.showed.KeysToActionDict.Remove(keyToRemove);
            e.SuppressKeyPress = true; //Prevent the "ding" sound
            return;
         }
         if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.Menu)
         {
            return; //ignore Tab, Shift, Ctrl, and Alt keys - basic
         }

         TextBox? tb = sender as TextBox;
         if (tb != null)
         {
            Keys pressedKey = e.KeyCode;
            if (tb.Text.Equals(pressedKey.ToString(), StringComparison.OrdinalIgnoreCase))
            {
               return; //If the same key is pressed, do nothing
            }

            //Check if the key is already assigned to another action
            if (BaseKeys.showed.KeysToActionDict.TryGetValue(pressedKey, out MouseHandle.mouseActions existingAction))
            {
               if (existingAction != textBoxToMouseActionsDict[tb]) //existingAction is not the same textBox group
               {
                  var confirm = MessageBox.Show($"Tato klávesa je již přiřazena k akci '{mouseActionsToNamesDict[existingAction]}'. Opravdu chcete změnit přiřazení?", "Potvrzení změny přiřazení", MessageBoxButtons.YesNo);
                  if (confirm != DialogResult.Yes) //If user selects No, do nothing
                  {
                     return;
                  }
                  //Find textbox with the existing assignment and clear it
                  var mouseActionsToTextBoxDict = textBoxToMouseActionsDict.GroupBy(kvp => kvp.Value).ToDictionary(g => g.Key, g => g.Select(x => x.Key).ToList()); //mouseAction na všechny textboxy - [0] primary, [1] alt
                  if (mouseActionsToTextBoxDict.TryGetValue(existingAction, out List<TextBox> existingTbList))
                  {
                     if (existingTbList.Any(p => p.Text.Equals(pressedKey.ToString(), StringComparison.OrdinalIgnoreCase)))
                     {
                        TextBox textbox = existingTbList.Find(p => p.Text.Equals(pressedKey.ToString(), StringComparison.OrdinalIgnoreCase));
                        textbox.Text = "";
                     }
                  }
                  BaseKeys.showed.KeysToActionDict[pressedKey] = textBoxToMouseActionsDict[tb];
                  tb.Text = pressedKey.ToString();
               }
               else //it exist in the same textbox group (primary/alt)
               {
                  var sameAction = textBoxToMouseActionsDict[tb];
                  //najdi všechny textboxy pro tenhle action
                  var siblings = textBoxToMouseActionsDict
                      .Where(kvp => kvp.Value == sameAction)
                      .Select(kvp => kvp.Key)
                      .ToList();
                  //najdi "druhý" textbox (alt/primary)
                  var otherTb = siblings.FirstOrDefault(x => x != tb);

                  if (otherTb != null)
                  {
                     if (otherTb.Text.Equals(pressedKey.ToString(), StringComparison.OrdinalIgnoreCase))
                     {
                        //swap
                        var oldValue = tb.Text;
                        tb.Text = pressedKey.ToString();
                        otherTb.Text = oldValue;
                     }
                     else
                     {
                        otherTb.Text = ""; //běžné chování – smaže alt
                        tb.Text = pressedKey.ToString();
                     }
                  }
                  e.SuppressKeyPress = true;
               }
            }
            else
            {
               if (!string.IsNullOrWhiteSpace(tb.Text)) //something is in textbox
               {
                  Keys keyToRemove = (Keys)Enum.Parse(typeof(Keys), tb.Text, true);
                  BaseKeys.showed.KeysToActionDict.Remove(keyToRemove);
               }
               tb.Text = pressedKey.ToString();
               e.SuppressKeyPress = true;
               var mouseAction = textBoxToMouseActionsDict[tb];
               BaseKeys.showed.KeysToActionDict.Add(pressedKey, mouseAction);
               BaseKeys.showed.KeyActionsEnabledDict[mouseAction] = mouseActionsToCheckBoxDict[mouseAction].Checked;
            }
         }
      }

      private void FillKeybindTextBoxes()
      {
         if (BaseKeys.showed == null) return;
         // 1) Keys → Actions už máte v KeyToActionsDict
         //    teď si uděláme Action → Keys[] List
         var actionToKeysDict = BaseKeys.showed.KeysToActionDict
                .GroupBy(kvp => kvp.Value)
                .ToDictionary(g => g.Key, g => g.Select(kvp => kvp.Key).ToList());

         // 2) Mapování akcí na textboxy
         var actionToTextBoxes = new Dictionary<MouseHandle.mouseActions, (TextBox Primary, TextBox Alt)>
          {
              { MouseHandle.mouseActions.goUp, (tbMoveUp, tbAltMoveUp) },
              { MouseHandle.mouseActions.goDown, (tbMoveDown, tbAltMoveDown) },
              { MouseHandle.mouseActions.goLeft, (tbMoveLeft, tbAltMoveLeft) },
              { MouseHandle.mouseActions.goRight, (tbMoveRight, tbAltMoveRight) },
              { MouseHandle.mouseActions.leftMouseClick, (tbLeftMouseClick, tbAltLeftMouseClick) },
              { MouseHandle.mouseActions.rightMouseClick, (tbRightMouseClick, tbAltRightMouseClick) },
              { MouseHandle.mouseActions.middleMouseWheelUp, (tbMiddleMouseWheelUp, tbAltMiddleMouseWheelUp) },
              { MouseHandle.mouseActions.middleMouseWheelDown, (tbMiddleMouseWheelDown, tbAltMiddleMouseWheelDown) },
              { MouseHandle.mouseActions.middleMouseClick, (tbMiddleMouseClick, tbAltMiddleMouseClick) }
          };

         // 3) Naplnění textbox
         foreach (var kvp in actionToTextBoxes)
         {
            var action = kvp.Key;
            var (primary, alt) = kvp.Value;

            if (actionToKeysDict.TryGetValue(action, out var keys))
            {
               primary.Text = keys.ElementAtOrDefault(0).ToString() ?? "";
               alt.Text = keys.ElementAtOrDefault(1).ToString() ?? "";
            }
            else
            {
               primary.Text = "";
               alt.Text = "";
            }
         }
      }

      private void btnCreateBaseKeysSetname_Click(object sender, EventArgs e)
      {


      }

      private void tbBaseKeysSetName_TextChanged(object sender, EventArgs e)
      {
         //edit when have same name -> change name of selected base keys set
         tbBaseKeysSetName.Text = tbBaseKeysSetName.Text.TrimStart();
         if (tbBaseKeysSetName.Text.Trim().Equals("default", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(tbBaseKeysSetName.Text))
         {
            btnCreateBaseKeysSetname.Enabled = false;
            return;
         }
         else
         {
            btnCreateBaseKeysSetname.Enabled = true;
         }
         if (BaseKeys.BaseKeysList.Any(p => p.SetName == tbBaseKeysSetName.Text.Trim()))
         {
            btnCreateBaseKeysSetname.Text = "edit";
         }
         else
         {
            btnCreateBaseKeysSetname.Text = "create";
         }
      }

      private void btnDeleteBaseKeysSetname_Click(object sender, EventArgs e)
      {


      }

      private void cmbBaseKeysSets_SelectedIndexChanged(object sender, EventArgs e)
      {
         string setName = cmbBaseKeysSets.SelectedItem?.ToString() ?? "default";
         BaseKeys.ChangeShowedBaseKeys(setName);
         ChangeBaseKeysSet();

      }

      private void ChangeBaseKeysSet()
      {
         //enable textboxes
         //naplnit textboxes
         //change checkboxes
         if (BaseKeys.showed == null) return;
         EnableDisableControlsOfTagInPanel(panelBaseKeysSettings, "BaseKeysSettingsTbs", !BaseKeys.showed.SetName.Equals("default", StringComparison.OrdinalIgnoreCase));
         FillKeybindTextBoxes();
         ChangeBaseKeysCheckBoxesCheckedState();
      }

      private void btnSelectBaseKeySet_Click(object sender, EventArgs e)
      {
         string setName = cmbBaseKeysSets.SelectedItem?.ToString() ?? "default";
         BaseKeys.ChangeSelectedBaseKeys(setName);
      }

      private void btnSaveBaseKeySet_Click(object sender, EventArgs e)
      {

      }

      #endregion

   }
}