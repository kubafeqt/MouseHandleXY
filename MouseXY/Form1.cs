using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MouseXY
{
   public partial class Form1 : Form
   {
      private NotifyIcon trayIcon;
      private ContextMenuStrip trayMenu;
      string appName = "MouseHandleXY";
      System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

      #region comments
      //OTHER:
      //naučit se vylepšení ve Visual Studio 2022 - př. vyhledat TODO a podobný zkratky, ukázání kde je vybraný soubor v projektu a další vylepšení
      //naučit se async/await, SOLID principy a další vylepšení kódu, ... př. C# 10 features, C# 11 features, C# 12 features, ... atd.
      //naučit se používat GitHub a GitHub Desktop (?)

      #endregion

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
         KeyPos.keysPosition = DBAccess.GetKeysPositions(); // načtení pozic kláves z databáze
         DBAccess.LoadKeysPositions(); // načtení pozic kláves z databáze do objektu KeyPos a seznamu KeyPosList
         Settings.delayMs = DBAccess.GetDelayMsExists().Item1;
         cboxShowSetKeyPos.Checked = DBAccess.GetShowDgvAfterSetKeyPos();
         nmDelayMs.Value = Settings.delayMs;

         #endregion

         dgvShowKeysPositions.AllowUserToAddRows = false;
         dgvShowKeysPositions.AllowUserToDeleteRows = false;
         UpdateDataGridView(); // Aktualizace DataGridView s pozicemi kláves
         //dgvShowKeysPositions.ReadOnly = true; // celý grid readonly - nefunguje pak .ReadOnly = false na sloupec IsActive
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
         //TODO: naučit se pracovat s BindingSource a DataGridView a vylepšit zobrazení dat v DataGridView pomocí BindingSource
         //DONE: udělat objekt pro BindingSource, který bude ukazovat hodnoty co jsou v KeyPosTable -> vylepšení SetName popsaný v TODO v DBAccess.cs
         bs = new BindingSource();
         //bs.DataSource = KeyPos.keysPosition; // Přiřazení dat z klávesových pozic do BindingSource
         bs.DataSource = KeyPos.KeyPositions;
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
            if (confirm == DialogResult.Yes)
            {
               KeyPos.keysPosition.Remove(key); // Odstranění klávesy z mapy pozic
               KeyPos.KeyPositions.RemoveAll(k => k.Key == key.ToString()); // Odstranění záznamu z listu KeyPositions
               DBAccess.DeleteKey(key);
            }
            // Odstranění řádku z DataGridView
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
               Keys key = (Keys)Enum.Parse(typeof(Keys), dgvShowKeysPositions.SelectedRows[0].Cells["Key"].Value.ToString());
               Point newPosition = new Point(posX, posY);
               KeyPos.keysPosition[key] = newPosition; // Aktualizace pozice klávesy
               DBAccess.SaveOrUpdateKeyPos(key, newPosition);
               DBAccess.LoadKeysPositions(); // Načtení aktualizovaných pozic kláves z databáze
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
            // Získání hodnoty buňky IsActive
            bool isActive = (bool)dgvShowKeysPositions.Rows[e.RowIndex].Cells["IsActive"].Value;
            // Získání klávesy z buňky Key
            Keys key = (Keys)Enum.Parse(typeof(Keys), dgvShowKeysPositions.Rows[e.RowIndex].Cells["Key"].Value.ToString());
            KeyPos k = KeyPos.KeyPositions.Find(k => k.Key == key.ToString());
            if (k != null)
            {
               k.IsActive = isActive; // Aktualizace stavu IsActive v objektu KeyPos
               DBAccess.SaveOrUpdateKeyPos(key, k.Position, isActive); // Uložení změn do databáze
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

   }
}