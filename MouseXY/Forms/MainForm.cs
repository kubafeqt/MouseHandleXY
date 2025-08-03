using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
         trayIcon.Text = "Myš ovládaná klávesnicí";
         trayIcon.Icon = SystemIcons.Application; //lze nahradit vlastní ikonou
         trayIcon.ContextMenuStrip = trayMenu;
         trayIcon.Visible = true;
         trayIcon.DoubleClick += OnShow;

         // Událost pro minimalizaci
         this.Resize += OnResize;

         // Skryj okno po startu
         this.Load += (s, e) => this.Hide();
         this.FormClosing += OnFormClosing;

         timer.Interval = 50;
         timer.Tick += timer_tick;


         #region events
         // event for change button enabled state when mouse cursor is controlled by keyboard or not
         MouseHandle.OnMouseCursorOpenChanged += (val) =>
         {
            //btnSetKeyPos.Enabled = !val;
            EnableDisableControlsOfTag("MouseControlDisable", !val); // Enable/disable controls for editing positions of keys
            lbMouseControl.Visible = val; // Zobrazí nebo skryje popisek pro ovládání myši
            lbMouseControl.Text = val ? "Mouse control is ON" : "Mouse control is OFF"; // Změní text popisku podle stavu ovládání myši
            if (val)
            {
               MouseHandle.setKeyToPos = false; // reset key to position after mouse cursor is controlled by keyboard
            }
         };
         // event for set key to position of mouse cursor
         MouseHandle.OnSetKeyToPos += () =>
         {
            SetKeyPos();
            UpdateDataGridView(); // Aktualizace DataGridView s pozicemi kláves
            if (cboxShowSetKeyPos.Checked && !showKeysPositions)
            {
               ShowKeysPositions();
            }
         };

         #endregion

      }

      private void MainForm_Load(object sender, EventArgs e)
      {
         cboxOnStartup.Checked = StartupManager.IsInStartup(appName); // Nastaví CheckBox podle toho jestli je aplikace zapsaná v registrech pro spouštění
         lbDelayMsDescription.Text = "for double control (open/close mouse control by keyboard)\nand double shift (change speed of mouse step) methods";

         #region DB_loading
         //DBAccess.ConnectionTest();
         DBAccess.LoadSetNames(); // načtení názvů setNames z databáze do dictionary setNames
         DBAccess.LoadKeysPositions(); // načtení pozic kláves z databáze do objektů KeyPos a na seznam KeyPositions
         DBAccess.LoadLatestSelectedSetName(); // načtení posledního vybraného setName z databáze
         KeyPos.UpdateKeyPosDict(); // aktualizuje/načte dictionary pozic kláves z KeyPositions
         Settings.delayMs = DBAccess.LoadDelayMsSettingsRowExists().Item1;
         nmDelayMs.Value = Settings.delayMs;
         cboxShowSetKeyPos.Checked = DBAccess.LoadShowDgvAfterSetKeyPos();
         Settings.showDgvAfterSetKeyPos = cboxShowSetKeyPos.Checked;

         #endregion

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
      }

      private void LoadComboBoxSetNames()
      {
         cmbSelectSetname.Items.Add("default"); // Přidání výchozího SetName
         lbShowedSetname.Text = $"ShowedSetname: {KeyPos.showedSetName}"; //then load from DB
         lbSelectedSetname.Text = $"SelectedSetname: {KeyPos.selectedSetName}"; //then load from DB settings
         foreach (var setName in KeyPos.setNames.Values)
         {
            cmbSelectSetname.Items.Add(setName);
         }
         int index = cmbSelectSetname.Items.IndexOf(KeyPos.showedSetName);
         cmbSelectSetname.SelectedIndex = index;
         EnableDisableAddKeyToSetnameButton();
      }

      #endregion

      #region Updating Controls
      BindingSource bs;
      private void UpdateDataGridView(int selectedRowIndex = 0)
      {
         bs = new BindingSource();
         bs.DataSource = new BindingList<KeyPos>(KeyPos.KeyPositions.Where(k => k.SetName == KeyPos.showedSetName).ToList());
         dgvShowKeysPositions.DataSource = bs; // Přiřazení BindingSource do DataGridView
         selectedRowIndex = selectedRowIndex > 0 && dgvShowKeysPositions.RowCount > selectedRowIndex ? selectedRowIndex : --selectedRowIndex;
         if (selectedRowIndex > 0 && selectedRowIndex <= dgvShowKeysPositions.Rows.Count)
         {
            dgvShowKeysPositions.CurrentCell = dgvShowKeysPositions.Rows[selectedRowIndex].Cells[0]; // Nastaví aktuální buňku na vybraný řádek
         }
      }

      private void ShowControlsOfTag(string tag, bool show = true)
      {
         var matchingControls = Controls.OfType<Control>().Where(c => c.Tag?.ToString() == tag);
         foreach (var control in matchingControls)
         {
            control.Visible = !show ? show : showKeysPositions;
         }
      }

      private void EnableDisableControlsOfTag(string tag, bool enable = true)
      {
         var matchingControls = Controls.OfType<Control>().Where(c => c.Tag?.ToString() == tag);
         foreach (var control in matchingControls)
         {
            control.Enabled = enable;
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
         SetKeyPos();
      }

      private void SetKeyPos()
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
         ShowControlsOfTag("EditPos");
         if (showKeysPositions)
         {
            Size = new Size(870, 695);
            btnShowKeysPositions.Text = btnShowKeysPositions.Text.Replace("Show", "hide", StringComparison.OrdinalIgnoreCase);

         }
         else
         {
            Size = Settings.defaultFormSize;
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
                  KeyPos.keysPositionDict.Remove(key); // Odstranění klávesy z mapy pozic
               }
               KeyPos.KeyPositions.RemoveAll(k => k.Key == key.ToString() && k.SetName == KeyPos.showedSetName); // Odstranění záznamu z listu KeyPositions
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
            KeyPos k = KeyPos.KeyPositions.Find(k => k.Key == key.ToString());
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
            int newId = KeyPos.PossibleFreeIdForSetname(); // Získání nového ID pro SetName
            string setName = tbSetname.Text != string.Empty ? tbSetname.Text.ToLower() : InputBox.Show("Zadejte název pro nový SetName:", "Přidat nový SetName", $"SetName {newId}").Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(setName))
            {
               if (cmbSelectSetname.Items.Contains(setName))
               {
                  MessageBox.Show($"Setname {setName} již existuje. Zvolte jiný název.");
                  return;
               }
               KeyPos.setNames[newId] = setName; // Přidání nového názvu do slovníku setNames
               // Aktualizace ComboBoxu s názvy nastavení:
               cmbSelectSetname.Items.Add(setName);
               latestSelecedItem = null;
               cmbSelectSetname.SelectedItem = setName; // Nastaví právě přidaný název jako vybraný
               tbSetname.Text = string.Empty;
               DBAccess.SaveOrUpdateSetName(newId, setName);
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
               newSetName = newSetName.Trim().ToLower();
               if (cmbSelectSetname.Items.Contains(newSetName) && newSetName != setName)
               {
                  MessageBox.Show($"Setname {newSetName} již existuje. Zvolte jiný název.");
                  return;
               }
               int id = KeyPos.setNames.FirstOrDefault(x => x.Value == setName).Key; // Získání ID pro stávající setName
               KeyPos.setNames[id] = newSetName;
               int index = cmbSelectSetname.Items.IndexOf(setName);
               cmbSelectSetname.Items[index] = newSetName; // Aktualizace položky v ComboBoxu
               tbSetname.Text = string.Empty; // Vyprázdní TextBox
               DBAccess.SaveOrUpdateSetName(id, newSetName, setName);
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
                  int id = KeyPos.setNames.FirstOrDefault(x => x.Value == setName).Key; // Získání ID pro stávající setName
                  KeyPos.setNames.Remove(id); // Odstranění setName z mapy dictionary
                  cmbSelectSetname.Items.Remove(setName);
                  tbSetname.Text = string.Empty;
                  DBAccess.DeleteSetName(id); // Smazání setName z databáze
                  DBAccess.DeleteKeysBySetName(setName); // Smazání všech kláves spojených s tímto setName z databáze
                  KeyPos.DeleteKeysBySetName(setName); // Smazání všech kláves spojených s tímto setName z objektu KeyPos v listu KeyPositions
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
         string setName = tbSetname.Text.Trim().ToLower();
         if (setName != "default" && cmbSelectSetname.Items.Contains(setName))
         {
            btnAddSetname.Enabled = true;
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
            btnAddSetname.Enabled = true;
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

   }
}