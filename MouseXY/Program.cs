namespace MouseXY
{
   internal static class Program
   {
      /// <summary>
      ///  The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         //// To customize application configuration such as set high DPI settings or default font,
         //// see https://aka.ms/applicationconfiguration.
         //ApplicationConfiguration.Initialize();
         //Application.Run(new Form1());

         const string mutexName = "MyUniqueAppNameMutex";
         bool createdNew;

         using (Mutex mutex = new Mutex(true, mutexName, out createdNew))
         {
            if (!createdNew)
            {
               //StartupManager.BringOtherInstanceToFront();
               MessageBox.Show("Process is already running.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
               return; // Exit the new instance
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MouseHandle._hookID = MouseHandle.SetHook(MouseHandle._proc); // Nastaví hook pro sledování klávesnice
            //ApplicationConfiguration.Initialize();
            Application.Run(new Form1()); // Nevyžaduje WinForm, ale drží aplikaci naživu
            MouseHandle.UnhookWindowsHookEx(MouseHandle._hookID);


         }


      }
   }
}