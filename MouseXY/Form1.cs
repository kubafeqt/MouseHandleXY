using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MouseXY
{
   public partial class Form1 : Form
   {
      private NotifyIcon trayIcon;
      private ContextMenuStrip trayMenu;
      string appName = "MouseHandleXY";
      System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

      public Form1()
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
         trayIcon.Icon = SystemIcons.Application; // lze nahradit vlastní ikonou
         trayIcon.ContextMenuStrip = trayMenu;
         trayIcon.Visible = true;
         trayIcon.DoubleClick += OnShow;

         // Událost pro minimalizaci
         this.Resize += OnResize;

         // Skryj okno po startu
         this.Load += (s, e) => this.Hide();
         this.FormClosing += OnFormClosing;

         timer.Interval = 50; // Interval v milisekundách
         timer.Tick += timer_tick;


         #region events
         // event for change button enabled state when mouse cursor is controlled by keyboard or not
         MouseHandle.OnMouseCursorOpenChanged += (val) =>
         {
            btnSetKeyPos.Enabled = !val;
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

      private void Form1_Load(object sender, EventArgs e)
      {
         //MouseHandle.ShowCursor(true);

         // Nastaví CheckBox podle toho jestli je aplikace zapsaná v registrech pro spouštění
         cboxOnStartup.Checked = StartupManager.IsInStartup(appName);
         lbDelayMsDescription.Text = "for double control (open/close mouse control by keyboard)\nand double shift (change speed of mouse step) methods";

         #region DB_loading
         //DBAccess.ConnectionTest();
         DBAccess.LoadSetNames(); // načtení názvů setNames z databáze do slovníku setNames
         DBAccess.LoadKeysPositions(); // načtení pozic kláves z databáze do objektu KeyPos a seznamu KeyPosList
         DBAccess.LoadLatestSelectedSetName(); // načtení posledního vybraného setName z databáze
         KeyPos.UpdateKeyPosDict(); // Aktualizace slovníku pozic kláves z KeyPosList
         Settings.delayMs = DBAccess.GetDelayMsExists().Item1;
         cboxShowSetKeyPos.Checked = DBAccess.GetShowDgvAfterSetKeyPos();
         nmDelayMs.Value = Settings.delayMs;

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

      BindingSource bs;
      bool isFirstItemEmptyDict;
      private void UpdateDataGridView()
      {
         bs = new BindingSource();
         bs.DataSource = new BindingList<KeyPos>(KeyPos.KeyPositions.Where(k => k.SetName == KeyPos.showedSetName).ToList());
         var items = bs.List.Cast<object>().ToList();
         isFirstItemEmptyDict = items.Count == 1 &&
         items[0] is System.Collections.IDictionary dict &&
         dict.Count == 0;
         if (isFirstItemEmptyDict)
         {
            dgvShowKeysPositions.DataSource = null;   // žádné sloupce ani prázdný řádek
            ShowControlsOfTag("EditPos", false);
         }
         else
         {
            dgvShowKeysPositions.DataSource = bs; // Přiřazení BindingSource do DataGridView
            ShowControlsOfTag("EditPos");
         }
      }

      private void ShowControlsOfTag(string tag, bool show = true)
      {
         var matchingControls = Controls.OfType<Control>().Where(c => c.Tag?.ToString() == tag);
         foreach (var control in matchingControls)
         {
            control.Visible = !show ? show : showKeysPositions && !isFirstItemEmptyDict;
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
         EnableDisableAddKeyToSdSetnameButton();
      }

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

      private void cboxOnStartup_CheckedChanged(object sender, EventArgs e)
      {
         string appPath = Application.ExecutablePath;
         StartupManager.SetStartup(cboxOnStartup.Checked, appName, appPath);
      }

      private void btnAcceptDelayMs_Click(object sender, EventArgs e)
      {
         Settings.delayMs = (int)nmDelayMs.Value;
         DBAccess.SaveDelayMs(Settings.delayMs, cboxShowSetKeyPos.Checked);
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

      private void timer_tick(object sender, EventArgs e)
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

      private void btnDeleteKeyPosition_Click(object sender, EventArgs e)
      {
         //selected row in DataGridView delete and update datagridview
         if (dgvShowKeysPositions.SelectedRows.Count > 0)
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
            }
            UpdateDataGridView(); // Aktualizace DataGridView s pozicemi kláves
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
               KeyPos.UpdateKeyPosition(keyString, newPosition, false); // Aktualizace pozice v KeyPos
               DBAccess.SaveOrUpdateKeyPos(key, newPosition, KeyPos.showedSetName); // Uložení změn do databáze
               KeyPos.UpdateKeyPosDict();
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
         DBAccess.SaveShowDgvAfterSetKeyPos(cboxShowSetKeyPos.Checked);
      }

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

      #region SetNames Controls
      private void btnAddSetname_Click(object sender, EventArgs e)
      {
         if (!btnAddSetname.Text.Equals("Edit", StringComparison.OrdinalIgnoreCase))
         {
            int newId = KeyPos.PossibleFreeIdForSetname(); // Získání nového ID pro SetName
            string setName = tbSetname.Text != string.Empty ? tbSetname.Text.ToLower() : Interaction.InputBox("Zadejte název pro nový SetName:", "Přidat nový SetName", $"SetName {newId}").Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(setName))
            {
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
            string newSetName = Interaction.InputBox($"Zadejte nový název pro {setName}:", "Přidat nový SetName", $"").Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(newSetName))
            {
               int id = KeyPos.setNames.FirstOrDefault(x => x.Value == setName).Key; // Získání ID pro stávající setName
               KeyPos.setNames[id] = newSetName;
               int index = cmbSelectSetname.Items.IndexOf(setName);
               cmbSelectSetname.Items[index] = newSetName; // Aktualizace položky v ComboBoxu
               //cmbSelectSetname.SelectedItem = newSetName; // Nastaví právě upravený název jako vybraný
               tbSetname.Text = string.Empty; // Vyprázdní TextBox
               DBAccess.SaveOrUpdateSetName(id, newSetName);
            }
            else // pokud je nový název prázdný zobrazí se dialog pro potvrzení smazání
            {
               DialogResult result = MessageBox.Show(
                   $"Chcete smazat setname: {setName} se všemi jeho hotkeys?",       // text zprávy
                   $"Potvrzení smazání {setName}",           // titulek okna
                   MessageBoxButtons.YesNo,       // tlačítka Ano / Ne
                   MessageBoxIcon.Question        // ikona s otazníkem
               );

               if (result == DialogResult.Yes)
               {
                  // Odstranění setName z mapy dictionary:
                  int id = KeyPos.setNames.FirstOrDefault(x => x.Value == setName).Key; // Získání ID pro stávající setName
                  KeyPos.setNames.Remove(id);
                  cmbSelectSetname.Items.Remove(setName); // Odstranění položky z ComboBoxu
                  tbSetname.Text = string.Empty; // Vyprázdní TextBox
                  DBAccess.DeleteSetName(id); // Smazání setName z databáze
                  DBAccess.DeleteKeysBySetName(setName); // Smazání všech kláves spojených s tímto setName z databáze
                  KeyPos.DeleteKeysBySetName(setName); // Smazání všech kláves spojených s tímto setName z objektu KeyPos v listu KeyPositions
                  MessageBox.Show($"Setname: {setName} byl smazán se všemi jeho uloženými hotkeys.");
                  cmbSelectSetname.SelectedIndex = cmbSelectSetname.Items.Count - 1;
                  if (KeyPos.selectedSetName == setName)
                  {
                     KeyPos.selectedSetName = "default"; // Pokud byl smazán vybraný setName, nastaví se na default
                     lbSelectedSetname.Text = $"SelectedSetname: {KeyPos.selectedSetName}";
                     DBAccess.SaveLatesSelectedSetName(cboxShowSetKeyPos.Checked); // Uložení vybraného setName do databáze
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
            btnAddSetname.PerformClick(); // Simulujeme kliknutí na tlačítko pro přidání/úpravu setName
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
         EnableDisableAddKeyToSdSetnameButton();
         UpdateDataGridView();
      }

      private void btnSelectSetname_Click(object sender, EventArgs e)
      {
         KeyPos.selectedSetName = cmbSelectSetname.SelectedItem.ToString();
         lbSelectedSetname.Text = $"SelectedSetname: {cmbSelectSetname.SelectedItem}";
         EnableDisableAddKeyToSdSetnameButton();
         KeyPos.UpdateKeyPosDict();
         DBAccess.SaveLatesSelectedSetName(cboxShowSetKeyPos.Checked); // Uložení vybraného setName do databáze
      }

      private void EnableDisableAddKeyToSdSetnameButton() => btnAddKeyToSelectedSetname.Enabled = KeyPos.selectedSetName != KeyPos.showedSetName ? true : false;

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