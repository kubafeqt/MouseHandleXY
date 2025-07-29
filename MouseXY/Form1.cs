using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
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
         // event for change button enabled state
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
            btnSetKeyPos.PerformClick();
            UpdateDataGridView(); // Aktualizace DataGridView s pozicemi kláves
            if (cboxShowSetKeyPos.Checked && !showKeysPositions)
            {
               btnShowKeysPositions.PerformClick();
            }
         };

         #endregion

      }

      private void Form1_Load(object sender, EventArgs e)
      {
         // Nastaví CheckBox podle toho jestli je aplikace zapsaná v registrech pro spouštění
         cboxOnStartup.Checked = StartupManager.IsInStartup(appName);
         //MouseHandle.ShowCursor(true);
         lbDelayMsDescription.Text = "for double control (open/close mouse control by keyboard)\nand double shift (change speed of mouse step) methods";

         #region DB_loading
         MouseHandle.keysPosition = DBAccess.GetKeysPositions(); // načtení pozic kláves z databáze
         //DBAccess.ConnectionTest();
         Settings.delayMs = DBAccess.GetDelayMsExists().Item1;
         nmDelayMs.Value = Settings.delayMs;

         #endregion

         dgvShowKeysPositions.AllowUserToAddRows = false;
         UpdateDataGridView(); // Aktualizace DataGridView s pozicemi kláves

      }

      bool isFirstItemEmptyDict;
      private void UpdateDataGridView()
      {
         BindingSource bs = new BindingSource();
         bs.DataSource = MouseHandle.keysPosition; // Přiřazení dat z klávesových pozic do BindingSource
         var items = bs.List.Cast<object>().ToList();
         isFirstItemEmptyDict = items.Count == 1 &&
       items[0] is System.Collections.IDictionary dict &&
       dict.Count == 0;
         if (isFirstItemEmptyDict)
         {
            dgvShowKeysPositions.DataSource = null;   // žádné sloupce ani prázdný řádek
            btnDeleteKeyPosition.Visible = false;
         }
         else
         {
            dgvShowKeysPositions.DataSource = bs; // Přiřazení BindingSource do DataGridView
            btnDeleteKeyPosition.Visible = showKeysPositions;
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
         DBAccess.SaveDelayMs(Settings.delayMs);
      }

      private void btnSetKeyPos_Click(object sender, EventArgs e)
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
      public void btnShowKeysPositions_Click(object sender, EventArgs e)
      {
         showKeysPositions = !showKeysPositions;
         dgvShowKeysPositions.Visible = showKeysPositions;
         btnDeleteKeyPosition.Visible = showKeysPositions && !isFirstItemEmptyDict;
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
            // Předpokládáme, že máš sloupec "Id" jako primární klíč
            Keys key = (Keys)Convert.ToInt32(dgvShowKeysPositions.SelectedRows[0].Cells["Key"].Value);

            var confirm = MessageBox.Show("Opravdu chcete tento záznam smazat?", "Potvrzení", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
               MouseHandle.keysPosition.Remove(key); // Odstranění klávesy z mapy pozic
               DBAccess.DeleteKey(key);
            }
            // Odstranění řádku z DataGridView
            //dgvShowKeysPositions.Rows.RemoveAt(dgvShowKeysPositions.SelectedRows[0].Index);
            UpdateDataGridView(); // Aktualizace DataGridView s pozicemi kláves
         }
         else
         {
            MessageBox.Show("Nejprve vyberte řádek ke smazání.");
         }

      }
   }
}