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

      #region KeysPosTable
      public static bool SavedKeyExist(Keys k, string setName)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "SELECT 1 FROM KeyPosTable WHERE [Key] = @Key and SetName = @SetName";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@Key", k.ToString());
                  command.Parameters.AddWithValue("@SetName", setName);
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

      public static void SaveOrUpdateKeyPos(Keys key, Point position, string setname = "default", bool isActive = true)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();

               if (SavedKeyExist(key, setname))
               {
                  // UPDATE
                  string updateSql = "UPDATE KeyPosTable SET Position = @Position, IsActive = @IsActive WHERE [Key] = @Key AND SetName = @SetName";
                  using (SqlCommand updateCmd = new SqlCommand(updateSql, connection))
                  {
                     updateCmd.Parameters.AddWithValue("@Key", key.ToString());
                     updateCmd.Parameters.AddWithValue("@SetName", setname);
                     updateCmd.Parameters.AddWithValue("@Position", $"{position.X},{position.Y}");
                     updateCmd.Parameters.AddWithValue("@IsActive", isActive);
                     updateCmd.ExecuteNonQuery();
                  }
               }
               else
               {
                  // INSERT
                  string insertSql = "INSERT INTO KeyPosTable ([Key], Position, SetName) VALUES (@Key, @Position, @SetName)";
                  using (SqlCommand insertCmd = new SqlCommand(insertSql, connection))
                  {
                     insertCmd.Parameters.AddWithValue("@Key", key.ToString());
                     insertCmd.Parameters.AddWithValue("@Position", $"{position.X},{position.Y}");
                     insertCmd.Parameters.AddWithValue("@SetName", setname);
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

      private static void UpdateKeysSetName(string newSetName, string oldSetName)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "UPDATE KeyPosTable SET SetName = @NewSetName WHERE SetName = @OldSetName";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@NewSetName", newSetName);
                  command.Parameters.AddWithValue("@OldSetName", oldSetName);
                  command.ExecuteNonQuery();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při aktualizaci názvu sady: " + ex.Message);
            }
         }
      }

      public static void LoadKeysPositions()//(string setName)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "SELECT * FROM KeyPosTable";// WHERE @SetName = SetName";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  //command.Parameters.AddWithValue("@SetName", setName);
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
                        string setname = reader["SetName"]?.ToString() ?? "default";
                        DateTime createdAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                        bool isActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        new KeyPos(keyStr, pos, setname, createdAt, isActive); // Přidá novou KeyPos do seznamu, pokud ještě neexistuje
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
               string sql = "DELETE FROM KeyPosTable WHERE [Key] = @Key AND SetName = @SetName";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@SetName", KeyPos.showedSetName);
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


      public static void DeleteKeysBySetName(string setname)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "DELETE FROM KeyPosTable WHERE SetName = @SetName";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@SetName", setname);
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

      #region SetNamesTable
      public static void SaveOrUpdateSetName(int setId, string setName, string oldSetName = "")
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               if (DbContainsSetNameId(setId))
               {
                  // UPDATE
                  string updateSql = "UPDATE SetNamesTable SET Name = @Name WHERE Id = @Id";
                  using (SqlCommand updateCmd = new SqlCommand(updateSql, connection))
                  {
                     updateCmd.Parameters.AddWithValue("@Id", setId);
                     updateCmd.Parameters.AddWithValue("@Name", setName);
                     updateCmd.ExecuteNonQuery();
                  }
                  UpdateKeysSetName(setName, oldSetName); // Aktualizuje název sady v tabulce KeyPosTable
                  KeyPos.UpdateKeysSetName(setName, oldSetName); // Aktualizuje název sady v objektu KeyPos
               }
               else
               {
                  // INSERT
                  string insertSql = "INSERT INTO SetNamesTable (Id, Name) VALUES (@SetID, @SetName)";
                  using (SqlCommand insertCmd = new SqlCommand(insertSql, connection))
                  {
                     insertCmd.Parameters.AddWithValue("@SetID", setId);
                     insertCmd.Parameters.AddWithValue("@SetName", setName);
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

      public static bool DbContainsSetNameId(int id)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "SELECT 1 FROM SetNamesTable WHERE Id = @id";

               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@id", id);
                  object result = command.ExecuteScalar();
                  if (result != null && result != DBNull.Value)
                  {
                     return true; // Id exists
                  }
                  else
                  {
                     return false; // Id does not exist
                  }
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při načítání z databáze: " + ex.Message);
               return false;
            }
         }
      }

      public static void DeleteSetName(int setId)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "DELETE FROM SetNamesTable WHERE Id = @Id";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@Id", setId);
                  command.ExecuteNonQuery();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při mazání z databáze: " + ex.Message);
            }
         }
      }

      public static void LoadSetNames()
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "SELECT Id, Name FROM SetNamesTable";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  using (SqlDataReader reader = command.ExecuteReader())
                  {
                     while (reader.Read())
                     {
                        int setId = reader.GetInt32(reader.GetOrdinal("Id"));
                        string setName = reader["Name"].ToString();
                        KeyPos.setNames[setId] = setName; // Přidá nebo aktualizuje setName v dictionary
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

      #endregion

      #region SettingsTable
      //ALTER: zobrazit/nezobrazit datagridview po uložení pozice klávesy -> json file save settings (?) -> (zatím?) db SettingsTable, ShowDgvAfterSetKeyPos
      public static (int, bool) LoadDelayMsSettingsRowExists()
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

      public static bool LoadShowDgvAfterSetKeyPos()
      {
         return GetValue("ShowDgvAfterSetKeyPos", true);
      }

      public static void LoadLatestSelectedSetName()
      {
         string setName = GetValue("LatestSelectedSetName", "default");
         KeyPos.selectedSetName = setName;
         KeyPos.showedSetName = setName;
      }

      private static T GetValue<T>(string columnName, T defaultValue)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = $"SELECT {columnName} FROM SettingsTable";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  object result = command.ExecuteScalar();
                  if (result != null && result != DBNull.Value)
                  {
                     return (T)Convert.ChangeType(result, typeof(T));
                  }
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show($"Chyba při čtení {columnName}: {ex.Message}");
            }
         }
         return defaultValue;
      }


      public static void SaveSettings()
      {
         bool rowExist = LoadDelayMsSettingsRowExists().Item2;
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();

               string sql = rowExist
                  ? "UPDATE SettingsTable SET DelayMs = @delay, ShowDgvAfterSetKeyPos = @showDgv, LatestSelectedSetName = @LatestSelectedSetName"
                  : "INSERT INTO SettingsTable (DelayMs, ShowDgvAfterSetKeyPos, LatestSelectedSetName) VALUES (@delay, @showDgv, @LatestSelectedSetName)";

               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@delay", Settings.delayMs);
                  command.Parameters.AddWithValue("@showDgv", Settings.showDgvAfterSetKeyPos);
                  command.Parameters.AddWithValue("@LatestSelectedSetName", KeyPos.selectedSetName);
                  command.ExecuteNonQuery();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při ukládání do databáze: " + ex.Message);
            }
         }
      
      }


      #endregion

   }
}