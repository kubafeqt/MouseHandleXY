using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
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

      #region SettingsTable
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
               string sql = "SELECT DelayMs FROM SettingsTable";
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

      public static bool GetShowDgvAfterSetKeyPos()
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               bool resultShowDgvAfterSetKeyPos;
               // Otevře spojení
               connection.Open();
               // Vytvoří SQL příkaz
               string sql = "SELECT ShowDgvAfterSetKeyPos FROM SettingsTable";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  object result = command.ExecuteScalar();
                  if (result != null && bool.TryParse(result.ToString(), out resultShowDgvAfterSetKeyPos))
                  {
                     return resultShowDgvAfterSetKeyPos;
                  }
                  else
                  {
                     return true;
                  }
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při práci s databází: " + ex.Message);
               return true;
            }
         }
      }

      public static void SaveDelayMs(int delayMs, bool showDgvAfterSetKeyPos)
      {
         bool delayMsExist = GetDelayMsExists().Item2;
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();

               if (delayMsExist)
               {
                  // UPDATE
                  string updateSql = "UPDATE SettingsTable SET DelayMs = @delay";
                  using (SqlCommand updateCmd = new SqlCommand(updateSql, connection))
                  {
                     updateCmd.Parameters.AddWithValue("@delay", delayMs);
                     updateCmd.ExecuteNonQuery();
                  }
               }
               else
               {
                  // INSERT
                  string insertSql = "INSERT INTO SettingsTable (DelayMs, ShowDgvAfterSetKeyPos) VALUES (@delay, @showDgv)";
                  using (SqlCommand insertCmd = new SqlCommand(insertSql, connection))
                  {
                     insertCmd.Parameters.AddWithValue("@delay", delayMs);
                     insertCmd.Parameters.AddWithValue("@showDgv", showDgvAfterSetKeyPos);
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

      public static void SaveShowDgvAfterSetKeyPos(bool showDgvAfterSetKeyPos)
      {
         bool rowExist = GetDelayMsExists().Item2;
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();

               if (rowExist)
               {
                  // UPDATE
                  string updateSql = "UPDATE SettingsTable SET ShowDgvAfterSetKeyPos = @showDgv";
                  using (SqlCommand updateCmd = new SqlCommand(updateSql, connection))
                  {
                     updateCmd.Parameters.AddWithValue("@showDgv", showDgvAfterSetKeyPos);
                     updateCmd.ExecuteNonQuery();
                  }
               }
               else
               {
                  // INSERT
                  string insertSql = "INSERT INTO SettingsTable (DelayMs, ShowDgvAfterSetKeyPos) VALUES (@delay, @showDgv)";
                  using (SqlCommand insertCmd = new SqlCommand(insertSql, connection))
                  {
                     insertCmd.Parameters.AddWithValue("@delay", Settings.delayMs);
                     insertCmd.Parameters.AddWithValue("@showDgv", showDgvAfterSetKeyPos);
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
      //ALTER: zobrazit/nezobrazit datagridview po uložení pozice klávesy -> json file save settings (?) -> (zatím?) db SettingsTable, ShowDgvAfterSetKeyPos
      //TODO: přidat do datagridview sloupec s SetName, SetID, CreatedAt, IsActive //advanced pro SetName, SetID
      //kategorie SetName a změny -> UI/UX jak? -> add to setname, remove from setname, change setname -> db SetNameTable , SetID ??
      //udělat sety pro KeysPosition, ... ; //advanced
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

      public static void SaveOrUpdateKeyPos(Keys key, Point position, bool isActive = true)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();

               if (SavedKeyExist(key))
               {
                  // UPDATE
                  string updateSql = "UPDATE KeyPosTable SET Position = @Position, IsActive = @IsActive WHERE [Key] = @Key";
                  using (SqlCommand updateCmd = new SqlCommand(updateSql, connection))
                  {
                     updateCmd.Parameters.AddWithValue("@Key", key.ToString());
                     updateCmd.Parameters.AddWithValue("@Position", $"{position.X},{position.Y}");
                     updateCmd.Parameters.AddWithValue("@IsActive", isActive);
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

      public static void LoadKeysPositions()
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "SELECT * FROM KeyPosTable";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  using (SqlDataReader reader = command.ExecuteReader())
                  {
                     while (reader.Read())
                     {
                        string keyStr = reader["Key"].ToString();
                        string positionStr = reader["Position"].ToString();
                        Point pos = new();
                        if (Enum.TryParse(keyStr, out Keys key) && !string.IsNullOrEmpty(positionStr))
                        {
                           string[] posParts = positionStr.Split(',');
                           if (posParts.Length == 2 && int.TryParse(posParts[0], out int x) && int.TryParse(posParts[1], out int y))
                           {
                               pos = new Point(x, y);
                           }
                        }
                        string setName = reader["SetName"]?.ToString() ?? "none";
                        DateTime createdAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                        bool isActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        new KeyPos(keyStr, pos, setName, createdAt, isActive); // Přidá novou KeyPos do seznamu, pokud ještě neexistuje
                     }
                  }
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při načítání z databáze: " + ex.Message);
            }
         }
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