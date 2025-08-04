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
               connection.Open();

               string sql = "SELECT 1";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  object result = command.ExecuteScalar();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při práci s databází: " + ex.Message);
            }
         }
      }

      #region KeysPosTable
      public static void SaveOrUpdateKeyPos(Keys key, Point position, string setname = "default", bool isActive = true)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = @"
                     IF EXISTS (SELECT 1 FROM KeyPosTable WHERE [Key] = @Key AND SetName = @SetName)
                     BEGIN
                        UPDATE KeyPosTable
                        SET Position = @Position, IsActive = @IsActive
                        WHERE [Key] = @Key AND SetName = @SetName
                     END
                     ELSE
                     BEGIN
                        INSERT INTO KeyPosTable ([Key], Position, SetName)
                        VALUES (@Key, @Position, @SetName)
                     END";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@Key", key.ToString());
                  command.Parameters.AddWithValue("@Position", $"{position.X},{position.Y}");
                  command.Parameters.AddWithValue("@SetName", setname);
                  command.Parameters.AddWithValue("@IsActive", isActive);
                  command.ExecuteNonQuery();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při ukládání do databáze: " + ex.Message);
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

      private static void DeleteKeysBySetName(string setname, SqlConnection connection)
      {
         string sql = "DELETE FROM KeyPosTable WHERE SetName = @SetName";
         using (SqlCommand command = new SqlCommand(sql, connection))
         {
            command.Parameters.AddWithValue("@SetName", setname);
            command.ExecuteNonQuery();
         }         
      }

      #endregion

      #region SetNamesTable
      /// <summary>
      /// Accesible only through SetNameService class.
      /// </summary>
      /// <param name="setId"></param>
      /// <param name="setName"></param>
      /// <param name="oldSetName"></param>
      public static void SaveOrUpdateSetName(int setId, string setName, string oldSetName = "")
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = @"IF EXISTS (SELECT 1 FROM SetNamesTable WHERE Id = @SetId)
                  BEGIN
                      UPDATE SetNamesTable SET Name = @SetName WHERE Id = @SetId;
                      UPDATE KeyPosTable SET SetName = @NewSetName WHERE SetName = @OldSetName;
                  END
                  ELSE
                  BEGIN
                      INSERT INTO SetNamesTable (Id, Name) VALUES (@SetId, @SetName);
                  END";

               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@SetID", setId);
                  command.Parameters.AddWithValue("@SetName", setName);
                  command.Parameters.AddWithValue("@NewSetName", setName);
                  command.Parameters.AddWithValue("@OldSetName", oldSetName);
                  command.ExecuteNonQuery();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při ukládání do databáze: " + ex.Message);
            }
         }
      }

      /// <summary>
      /// Accesible only through SetNameService class.
      /// </summary>
      /// <param name="setId"></param>
      /// <param name="setName"></param>
      public static void DeleteSetNameAndItKeysById(int setId, string setName)
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
               DeleteKeysBySetName(setName, connection); // Smaže všechny klávesy spojené s tímto setName
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
      public static void SaveSettings()
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = @"IF EXISTS (SELECT 1 FROM SettingsTable)
                  BEGIN
                      UPDATE SettingsTable
                      SET DelayMs = @delay, ShowDgvAfterSetKeyPos = @showDgv, LatestSelectedSetName = @LatestSelectedSetName;
                  END
                  ELSE
                  BEGIN
                      INSERT INTO SettingsTable (DelayMs, ShowDgvAfterSetKeyPos, LatestSelectedSetName)
                      VALUES (@delay, @showDgv, @LatestSelectedSetName);
                  END";
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

      public static void LoadSettings()
      {
         try
         {
            Settings.delayMs = LoadDelayMs();
            Settings.showDgvAfterSetKeyPos = LoadShowDgvAfterSetKeyPos(); 
            LoadLatestSelectedSetName(); // načtení posledního vybraného setName z databáze
         }
         catch (Exception ex)
         {
            MessageBox.Show("Chyba při načítání nastavení: " + ex.Message);
         }
      }

      private static int LoadDelayMs()
      {
         return GetValue("DelayMs", Settings.delayMs);
      }

      private static bool LoadShowDgvAfterSetKeyPos()
      {
         return GetValue("ShowDgvAfterSetKeyPos", true);
      }

      private static void LoadLatestSelectedSetName()
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
               MessageBox.Show($"Chyba při čtení {columnName} ze SettingsTable: {ex.Message}");
            }
         }
         return defaultValue;
      }

      #endregion

   }
}