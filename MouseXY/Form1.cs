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

         // event for change button enabled state
         MouseHandle.OnMouseCursorChanged += (val) =>
         {
            btnSetKeyPos.Enabled = !val;
            if (val)
            {
               MouseHandle.setKeyToPos = false; // reset key to position after mouse cursor is shown
            }
         };
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         // Nastaví CheckBox podle toho jestli je aplikace zapsaná v registrech pro spouštění
         cboxOnStartup.Checked = StartupManager.IsInStartup(appName);
         //MouseHandle.ShowCursor(true);
         lbDelayMsDescription.Text = "for double control (open/close mouse control by keyboard)\nand double shift (change speed of mouse step) methods";
         //DBAccess.ConnectionTest();
         Settings.delayMs = DBAccess.GetDelayMsExists().Item1;
         nmDelayMs.Value = Settings.delayMs;
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
      }

      public static bool showKeysPositions = false;
      private void btnShowKeysPositions_Click(object sender, EventArgs e)
      {
         showKeysPositions = !showKeysPositions;
         dgvShowKeysPositions.Visible = showKeysPositions;
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
   }
}