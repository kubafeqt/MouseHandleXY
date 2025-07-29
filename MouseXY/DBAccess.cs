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

      #region DelayMs
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

      #endregion

      #region KeysPosition
      //TODO: zobrazit/nezobrazit datagridview po uložení pozice klávesy -> json file save settings (?)
      //TODO: přidat do datagridview sloupec s datumem CreatedAt a IsActive, SetName //advanced
      //TODO: přidat do datagridview sloupec s SetName, SetID, CreatedAt, IsActive //advanced
      //TODO: kategorie SetName a změny -> UI/UX jak? -> add to setname, remove from setname, change setname -> db SetNameTable , SetID ??
      //udělat sety pro KeysPosition, ... ; //advanced
      //TODO: upravit hodnoty Position pomocí textboxu - vybraný z datagridview
      public static bool SavedKeyExist(Keys k)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "SELECT 1 FROM KeyPosTable WHERE [Key] = @Key";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@Key", k.ToString());
                  using (SqlDataReader reader = command.ExecuteReader())
                  {
                     return reader.HasRows;
                  }
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při práci s databází: " + ex.Message);
               return false;
            }
         }
      }

      public static void SaveOrUpdateKeyPos(Keys key, Point position)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();

               if (SavedKeyExist(key))
               {
                  // UPDATE
                  string updateSql = "UPDATE KeyPosTable SET Position = @Position WHERE [Key] = @Key";
                  using (SqlCommand updateCmd = new SqlCommand(updateSql, connection))
                  {
                     updateCmd.Parameters.AddWithValue("@Key", key.ToString());
                     updateCmd.Parameters.AddWithValue("@Position", $"{position.X},{position.Y}");
                     updateCmd.ExecuteNonQuery();
                  }
               }
               else
               {
                  // INSERT
                  string insertSql = "INSERT INTO KeyPosTable ([Key], Position) VALUES (@Key, @Position)";
                  using (SqlCommand insertCmd = new SqlCommand(insertSql, connection))
                  {
                     insertCmd.Parameters.AddWithValue("@Key", key.ToString());
                     insertCmd.Parameters.AddWithValue("@Position", $"{position.X},{position.Y}");
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

      public static Dictionary<Keys, Point> GetKeysPositions()
      {
         Dictionary<Keys, Point> keysPosition = new Dictionary<Keys, Point>();
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "SELECT [Key], Position FROM KeyPosTable";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  using (SqlDataReader reader = command.ExecuteReader())
                  {
                     while (reader.Read())
                     {
                        string keyStr = reader["Key"].ToString();
                        string positionStr = reader["Position"].ToString();
                        if (Enum.TryParse(keyStr, out Keys key) && !string.IsNullOrEmpty(positionStr))
                        {
                           string[] posParts = positionStr.Split(',');
                           if (posParts.Length == 2 && int.TryParse(posParts[0], out int x) && int.TryParse(posParts[1], out int y))
                           {
                              keysPosition[key] = new Point(x, y);
                           }
                        }
                     }
                  }
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při načítání z databáze: " + ex.Message);
            }
         }
         return keysPosition;
      }

      public static void DeleteKey(Keys key)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "DELETE FROM KeyPosTable WHERE [Key] = @Key";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@Key", key.ToString());
                  command.ExecuteNonQuery();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při mazání z databáze: " + ex.Message);
            }
         }
      }

      #endregion

   }
}