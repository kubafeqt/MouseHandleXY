using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace MouseXY
{
    class DBAccess
    {
      static readonly string binDir = AppDomain.CurrentDomain.BaseDirectory; // Běhový adresář (např. bin\Debug\net8.0)
      static readonly string projectDir = Directory.GetParent(binDir).Parent.Parent.Parent.FullName; // Projektová složka = 3 úrovně výš z bin\Debug\netX
      static readonly string dbFilePath = Path.Combine(projectDir, "mssql_dbFile.mdf");

      static readonly string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbFilePath};Integrated Security=True";

      public static void ConnectionTest()
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               // Otevře spojení
               connection.Open();
               //MessageBox.Show("Spojení s databází bylo úspěšně navázáno.");

               // Vytvoří SQL příkaz
               string sql = "SELECT 1";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  // Spustí příkaz a získá výsledek
                  object result = command.ExecuteScalar();
                  //MessageBox.Show("Výsledek dotazu: " + result);
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při práci s databází: " + ex.Message);
            }
         }
      }

      public static (int, bool) GetDelayMsExists()
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               int resultDelayMs;
               // Otevře spojení
               connection.Open();

               // Vytvoří SQL příkaz
               string sql = "SELECT DelayMs FROM DelayTable";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                 object result = command.ExecuteScalar();

                  if (result != null && int.TryParse(result.ToString(), out resultDelayMs))
                  {
                     return (resultDelayMs, true);
                  }
                  else
                  {
                     return (Settings.delayMs, false);
                  }
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při práci s databází: " + ex.Message);
               return (Settings.delayMs, false);
            }
         }
      }

      public static void SaveDelayMs(int delayMs)
      {
         bool DelayMsExist = GetDelayMsExists().Item2;
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();

               //   // Zjisti, jestli tabulka už má nějaký řádek
               //   string checkSql = "SELECT COUNT(*) FROM DelayTable";
               //   using (SqlCommand checkCmd = new SqlCommand(checkSql, connection))
               //   {
               //      int count = (int)checkCmd.ExecuteScalar();
               //      if (count > 0)
               //      {
               //         // UPDATE
               //      }
               //      else
               //      {
               //         // INSERT
               //      }
               //   }
               //}

               if (DelayMsExist)
               {
                  // UPDATE
                  string updateSql = "UPDATE DelayTable SET DelayMs = @delay";
                  using (SqlCommand updateCmd = new SqlCommand(updateSql, connection))
                  {
                     updateCmd.Parameters.AddWithValue("@delay", delayMs);
                     updateCmd.ExecuteNonQuery();
                  }
               }
               else
               {
                  // INSERT
                  string insertSql = "INSERT INTO DelayTable (DelayMs) VALUES (@delay)";
                  using (SqlCommand insertCmd = new SqlCommand(insertSql, connection))
                  {
                     insertCmd.Parameters.AddWithValue("@delay", delayMs);
                     insertCmd.ExecuteNonQuery();
                  }
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při ukládání do databáze: " + ex.Message);
            }
         }
      }
   }
}
