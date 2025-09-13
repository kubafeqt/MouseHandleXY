using Microsoft.Data.SqlClient;
using System;

namespace MouseXY
{
   class DBAccess
   {
      static readonly string binDir = AppDomain.CurrentDomain.BaseDirectory; // Běhový adresář (např. bin\Debug\net8.0)
      static readonly string projectDir = Directory.GetParent(binDir).Parent.Parent.Parent.FullName; // Projektová složka = 3 úrovně výš z bin\Debug\netX
      static readonly string dbFilePath = Path.Combine(projectDir, @"mssql_dbFile.mdf");

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

      /// <summary>
      /// Loads Keys Positions, SetNames, Settings
      /// </summary>
      public static void LoadAll()
      {
         LoadKeysPositions();
         LoadSetNames();
         LoadBaseKeysFromDB();
         //LoadBaseKeysSetNamesFromDB();
         LoadSettings();
      }

      #region KeysPosTable
      public static void SaveOrUpdateAllKeyPos() //after import from file
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               foreach (var keypos in KeyPos.KeyPositionsList)
               {
                  string sql = @"
                     IF EXISTS (SELECT 1 FROM KeyPosTable WHERE [Key] = @Key AND SetName = @SetName)
                     BEGIN
                        Update KeyPosTable
                        SET Position = @Position, IsActive = @IsActive, CreatedAt = @CreatedAt
                        WHERE [Key] = @Key AND SetName = @SetName
                     END
                     ELSE
                     BEGIN
                        INSERT INTO KeyPosTable ([Key], Position, SetName, IsActive, CreatedAt)
                        VALUES (@Key, @Position, @SetName, @IsActive, @CreatedAt)
                     END";
                  using (SqlCommand command = new SqlCommand(sql, connection))
                  {
                     command.Parameters.AddWithValue("@Key", keypos.Key.ToString());
                     command.Parameters.AddWithValue("@Position", $"{keypos.Position.X},{keypos.Position.Y}");
                     command.Parameters.AddWithValue("@SetName", keypos.SetName);
                     command.Parameters.AddWithValue("@IsActive", keypos.IsActive);
                     command.Parameters.AddWithValue("@CreatedAt", keypos.CreatedAt);
                     command.ExecuteNonQuery();
                  }
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při ukládání do databáze: " + ex.Message);
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

      private static void LoadKeysPositions()
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

      #endregion

      #region SetNamesTable
      public static void SaveAllSetNames()
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               foreach (var kvp in KeyPos.SetNamesDict)
               {
                  string sql = @"IF NOT EXISTS (SELECT 1 FROM SetNamesTable WHERE Name = @SetName)
                  BEGIN
                      INSERT INTO SetNamesTable (Id, Name) VALUES (@SetId, @SetName);
                  END";
                  using (SqlCommand command = new SqlCommand(sql, connection))
                  {
                     command.Parameters.AddWithValue("@SetID", kvp.Key);
                     command.Parameters.AddWithValue("@SetName", kvp.Value);
                     command.ExecuteNonQuery();
                  }
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
               string sql = @"BEGIN 
                     DELETE FROM SetNamesTable WHERE Id = @Id;
                     DELETE FROM KeyPosTable WHERE SetName = @SetName;
                  END";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@Id", setId);
                  command.Parameters.AddWithValue("@SetName", setName);
                  command.ExecuteNonQuery();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při mazání z databáze: " + ex.Message);
            }
         }
      }

      private static void LoadSetNames()
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
                        KeyPos.SetNamesDict[setId] = setName; // Přidá nebo aktualizuje setName v dictionary
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
                      SET DelayMs = @delay, ShowDgvAfterSetKeyPos = @showDgv, LatestSelectedSetName = @LatestSelectedSetName, LatestSelectedBaseKeysSetname = @LatestSelectedBaseKeysSetname;
                  END
                  ELSE
                  BEGIN
                      INSERT INTO SettingsTable (DelayMs, ShowDgvAfterSetKeyPos, LatestSelectedSetName, LatestSelectedBaseKeysSetname)
                      VALUES (@delay, @showDgv, @LatestSelectedSetName, @LatestSelectedBaseKeysSetname);
                  END";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@delay", Settings.delayMs);
                  command.Parameters.AddWithValue("@showDgv", Settings.showDgvAfterSetKeyPos);
                  command.Parameters.AddWithValue("@LatestSelectedSetName", KeyPos.selectedSetName);
                  command.Parameters.AddWithValue("@LatestSelectedBaseKeysSetname", BaseKeys.selectedSetName);
                  command.ExecuteNonQuery();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při ukládání do databáze: " + ex.Message);
            }
         }
      }

      private static void LoadSettings()
      {
         try
         {
            Settings.delayMs = LoadDelayMs();
            Settings.showDgvAfterSetKeyPos = LoadShowDgvAfterSetKeyPos();
            LoadLatestSelectedSetName(); //načtení posledního vybraného setName z databáze
            LoadLatestSelectedBaseKeysSetName();
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

      private static void LoadLatestSelectedBaseKeysSetName()
      {
         string setName = GetValue("LatestSelectedBaseKeysSetname", "default");
         BaseKeys.selectedSetName = setName;
         BaseKeys baseKeys = BaseKeys.BaseKeysList.Find(x => x.SetName == setName);
         BaseKeys.selected = baseKeys;
         BaseKeys.showed = baseKeys;
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

      #region BaseKeysSettingsTable
      public static void SaveBaseKeysSetNamesToDB()
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               foreach (var baseKeys in BaseKeys.BaseKeysList)
               {
                  string sql = @"IF NOT EXISTS (SELECT 1 FROM BaseKeysTable WHERE SetName = @SetName)
                  BEGIN
                      INSERT INTO BaseKeysTable (SetName) VALUES (@SetName);
                  END";
                  using (SqlCommand command = new SqlCommand(sql, connection))
                  {
                     command.Parameters.AddWithValue("@SetName", baseKeys.SetName);
                     command.ExecuteNonQuery();
                  }
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při ukládání do databáze: " + ex.Message);
            }
         }
      }

      public static void DeleteBaseKeysSetNameAndItsKeys(string setName)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = @"DELETE FROM BaseKeysTable WHERE SetName = @SetName;";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@SetName", setName);
                  command.ExecuteNonQuery();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při mazání z databáze: " + ex.Message);
            }
         }
      }

      public static void DeleteBaseKeyFromSetNameAndAction(string setName, string actionType, Keys keyToRemove)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            string query = @"
            UPDATE BaseKeysTable
            SET KeyValue = CASE WHEN KeyValue = @Key THEN NULL ELSE KeyValue END,
                AltKeyValue = CASE WHEN AltKeyValue = @Key THEN NULL ELSE AltKeyValue END
            WHERE SetName = @SetName AND ActionType = @ActionType;";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
               command.Parameters.AddWithValue("@Key", keyToRemove.ToString());
               command.Parameters.AddWithValue("@SetName", setName);
               command.Parameters.AddWithValue("@ActionType", actionType);

               connection.Open();
               command.ExecuteNonQuery();
            }
         }
      }

      public static void ChangeBaseKeysSetName(string setName, string newSetName)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = @"UPDATE BaseKeysTable SET SetName = @NewSetName WHERE SetName = @SetName;";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@SetName", setName);
                  command.Parameters.AddWithValue("@NewSetName", newSetName);
                  command.ExecuteNonQuery();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při ukládání do databáze: " + ex.Message);
            }
         }
      }

      public static void LoadBaseKeysSetNamesFromDB()
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "SELECT DISTINCT SetName FROM BaseKeysTable WHERE SetName <> 'default'";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  using (SqlDataReader reader = command.ExecuteReader())
                  {
                     while (reader.Read())
                     {
                        string setName = reader["SetName"].ToString();
                        if (!BaseKeys.BaseKeysList.Any(bk => bk.SetName == setName)) //it is not exist with current set name - create new object
                        {
                           new BaseKeys(setName);
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
      }

      //Save - onExit, onPanelChange, onSaveSetButton
      public static void SaveBaseKeysToDB(string action, string keyValue, string altKeyValue, bool enabled, string setName)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = @"IF EXISTS (SELECT ActionType, SetName FROM BaseKeysTable WHERE ActionType = @ActionType AND SetName = @SetName)
                  BEGIN
                      UPDATE BaseKeysTable
                      SET KeyValue = @KeyValue, AltKeyValue = @AltKeyValue, Enabled = @Enabled
                      WHERE SetName = @SetName AND ActionType = @ActionType
                  END
                  ELSE
                  BEGIN
                      INSERT INTO BaseKeysTable (ActionType, KeyValue, AltKeyValue, Enabled, SetName)
                      VALUES (@ActionType, @KeyValue, @AltKeyValue, @Enabled, @SetName)
                  END";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@ActionType", action);
                  command.Parameters.AddWithValue("@KeyValue", keyValue);
                  command.Parameters.AddWithValue("@AltKeyValue", altKeyValue);
                  command.Parameters.AddWithValue("@Enabled", enabled);
                  command.Parameters.AddWithValue("@SetName", setName);
                  command.ExecuteNonQuery();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při ukládání do databáze: " + ex.Message);
            }
         }
      }

      public static void LoadBaseKeysFromDB()
      {
         try
         {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               connection.Open();
               string sql = @"SELECT ActionType, KeyValue, AltKeyValue, SetName FROM BaseKeysTable WHERE SetName <> 'default'";
               using (SqlCommand cmd = new SqlCommand(sql, connection))
               using (SqlDataReader reader = cmd.ExecuteReader())
               {
                  while (reader.Read())
                  {
                     string setName = reader["SetName"]?.ToString() ?? string.Empty;
                     Enum.TryParse<MouseHandle.mouseActions>(reader["ActionType"]?.ToString(), out MouseHandle.mouseActions actionType);
                     string keyValueStr = reader["KeyValue"]?.ToString() ?? string.Empty;
                     Keys? keyValue = keyValueStr == string.Empty ? null : (Keys)Enum.Parse(typeof(Keys), keyValueStr, true);
                     string altKeyValueStr = reader["AltKeyValue"]?.ToString() ?? string.Empty;
                     Keys? altKeyValue = altKeyValueStr == string.Empty ? null : (Keys)Enum.Parse(typeof(Keys), altKeyValueStr, true);

                     if (!BaseKeys.BaseKeysList.Any(bk => bk.SetName == setName)) //it is not exist with current set name - create new object
                     {
                        BaseKeys baseKeys = new BaseKeys(setName);
                        LoadBaseKeysDictValues(baseKeys, actionType, keyValue, altKeyValue);
                     }
                     else //it already with current setname on the object list - get the object by setName
                     {
                        BaseKeys? baseKeys = BaseKeys.BaseKeysList.Find(bk => bk.SetName == setName);
                        LoadBaseKeysDictValues(baseKeys, actionType, keyValue, altKeyValue);
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

      private static void LoadBaseKeysDictValues(BaseKeys? baseKeys, MouseHandle.mouseActions actionType, Keys? keyValue, Keys? altKeyValue)
      {
         if (baseKeys != null)
         {
            if (keyValue != null)
            {
               baseKeys.KeysToActionDict[keyValue] = actionType;
            }
            if (altKeyValue != null)
            {
               baseKeys.KeysToActionDict[altKeyValue] = actionType;
            }
            baseKeys.ActionsToKeysDict[actionType] = new List<Keys?> { keyValue, altKeyValue };
         }
      }

      public static void SaveKeysActionsEnabledDict(string setName, string action, bool enabled)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = @"IF EXISTS (SELECT 1 FROM BaseKeysTable WHERE SetName = @SetName AND ActionType = @ActionType)
                  BEGIN
                      UPDATE BaseKeysTable
                      SET Enabled = @Enabled
                      WHERE SetName = @SetName AND ActionType = @ActionType;
                  END
                  ELSE
                  BEGIN
                      INSERT INTO BaseKeysTable (SetName, ActionType, Enabled)
                      VALUES (@SetName, @ActionType, @Enabled);
                  END";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@SetName", setName);
                  command.Parameters.AddWithValue("@ActionType", action);
                  command.Parameters.AddWithValue("@Enabled", enabled);
                  command.ExecuteNonQuery();
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při ukládání do databáze: " + ex.Message);
            }
         }
      }

      public static bool LoadKeysActionsEnabledDict(string setName, string action)
      {
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            try
            {
               connection.Open();
               string sql = "SELECT Enabled FROM BaseKeysTable WHERE SetName = @SetName AND ActionType = @ActionType";
               using (SqlCommand command = new SqlCommand(sql, connection))
               {
                  command.Parameters.AddWithValue("@SetName", setName);
                  command.Parameters.AddWithValue("@ActionType", action);
                  using (SqlDataReader reader = command.ExecuteReader())
                  {
                     while (reader.Read())
                     {
                        bool enabled = reader.GetBoolean(reader.GetOrdinal("Enabled"));
                        return enabled;
                     }
                  }
               }
            }
            catch (SqlException ex)
            {
               MessageBox.Show("Chyba při načítání z databáze: " + ex.Message);
            }
            return true;
         }
      }



      #endregion

   }
}