using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FES__File_Encryption_Software_
{
    internal static class SQLAcc
    {
        internal static readonly string connectionString = @"Data Source=den1.mssql6.gear.host;Initial Catalog=testlogindb;Persist Security Info=True;User ID=testlogindb;Password=Ye9k3~~mYrF1";
        internal static string LoggedUser { get; set; }
        internal static string LoggedHash { private get; set; }
        internal static string LoggedPass
        {
            private get
            {
                using (RijndaelManaged rm = new RijndaelManaged())
                {
                    rm.Mode = CipherMode.ECB;
                    rm.Key = new SHA256Managed().ComputeHash(System.Text.Encoding.UTF8.GetBytes(LoggedHash));

                    using (ICryptoTransform ctransform = rm.CreateDecryptor())
                    {
                        byte[] data = _LoggedPass;
                        return System.Text.Encoding.UTF8.GetString(ctransform.TransformFinalBlock(data, 0, data.Length));
                    }
                }
            }

            set
            {
                using (RijndaelManaged rm = new RijndaelManaged())
                {
                    rm.Mode = CipherMode.ECB;
                    rm.Key = new SHA256Managed().ComputeHash(System.Text.Encoding.UTF8.GetBytes(LoggedHash));

                    using (ICryptoTransform ctransform = rm.CreateEncryptor())
                    {
                        byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
                        _LoggedPass = ctransform.TransformFinalBlock(data, 0, data.Length);
                    }
                }
            }
        }
        private static byte[] _LoggedPass;

        public delegate void SQLAccEventHandler();
        public static event SQLAccEventHandler OnUsernameUpdate;
        public static event SQLAccEventHandler OnPasswordUpdate;
        public static event SQLAccEventHandler OnAccountDelete;
        public static event SQLAccEventHandler OnAccountRegister;

        /// <summary>
        /// Checks if the program is able to connect to SQLDB
        /// </summary>
        /// <returns>Returns true on success</returns>
        public static async Task<bool> CheckConnection()
        {
            using (SqlConnection sqlc = new SqlConnection(connectionString + ";Connection Timeout=1"))
            {
                try
                {
                    await sqlc.OpenAsync();
                    return (sqlc.State == ConnectionState.Open);
                }

                catch (Exception) { return false; }
            }
        }

        /// <summary>
        /// Updates the User's encryption key
        /// </summary>
        /// <param name="key">AES Key as <see cref="string"/></param>
        public static async Task UpdateKey(string key = null)
        {
            using (SqlConnection sqlc = new SqlConnection(connectionString))
            {
                await sqlc.OpenAsync();
                SqlCommand command = sqlc.CreateCommand();

                if (key != null)
                {
                    using (RijndaelManaged rm = new RijndaelManaged())
                    {
                        rm.Mode = CipherMode.ECB;
                        rm.Key = new SHA256Managed().ComputeHash(System.Text.Encoding.UTF8.GetBytes(LoggedPass));

                        using (ICryptoTransform ctransform = rm.CreateEncryptor())
                        {
                            byte[] data = System.Text.Encoding.UTF8.GetBytes(key);
                            key = Convert.ToBase64String(ctransform.TransformFinalBlock(data, 0, data.Length));

                            data = null;
                        }
                    }

                    command.CommandText = $"UPDATE [Users] SET [Key] = @key WHERE [Username] = @username AND [Password] = @password;";
                    command.Parameters.Add("@key", SqlDbType.NChar).Value = key;
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = LoggedUser;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = LoggedHash;
                }

                else
                {
                    command.CommandText = $"UPDATE [Users] SET [Key] = NULL WHERE [Username] = @username AND [Password] = @password;"; ;
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = LoggedUser;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = LoggedHash;
                }

                await command.ExecuteNonQueryAsync();

                sqlc.Close();
            }
        }

        /// <summary>
        /// Gets the User's encryption key
        /// </summary>
        /// <returns>AES key as <see cref="string"/></returns>
        public static async Task<string> GetKey()
        {
            string key = null;

            using (SqlConnection sqlc = new SqlConnection(connectionString))
            {
                await sqlc.OpenAsync();
                SqlCommand command = sqlc.CreateCommand();

                command.CommandText = $"SELECT [Key] FROM [Users] WHERE [Username] = @username AND [Password] = @password;";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = LoggedUser;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = LoggedHash;

                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    if (dataReader.HasRows)
                    {
                        while (await dataReader.ReadAsync())
                        {
                            key = dataReader.GetString(0);

                            using (RijndaelManaged rm = new RijndaelManaged())
                            {
                                rm.Mode = CipherMode.ECB;
                                rm.Key = new SHA256Managed().ComputeHash(System.Text.Encoding.UTF8.GetBytes(LoggedPass));

                                using (ICryptoTransform ctransform = rm.CreateDecryptor())
                                {
                                    byte[] data = Convert.FromBase64String(key);
                                    key = System.Text.Encoding.UTF8.GetString(ctransform.TransformFinalBlock(data, 0, data.Length));

                                    data = null;
                                }
                            }
                        }
                    }
                }

                sqlc.Close();
            }

            return key;
        }

        /// <summary>
        /// Updates the User's username
        /// </summary>
        public static async Task UpdateUsername(string username)
        {
            using (SqlConnection sqlc = new SqlConnection(connectionString))
            {
                await sqlc.OpenAsync();
                SqlCommand command = sqlc.CreateCommand();

                command.CommandText = $"UPDATE [Users] SET [Username] = @newusername WHERE [Username] = @oldusername AND [Password] = @password;";
                command.Parameters.Add("@newusername", SqlDbType.NChar).Value = username;
                command.Parameters.Add("@oldusername", SqlDbType.NVarChar).Value = LoggedUser;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = LoggedHash;

                await command.ExecuteNonQueryAsync();
                LoggedUser = username;
                OnUsernameUpdate?.Invoke();

                sqlc.Close();
            }
        }

        /// <summary>
        /// Updates the User's password
        /// </summary>
        public static async Task UpdatePassword(string password)
        {
            using (SqlConnection sqlc = new SqlConnection(connectionString))
            {
                await sqlc.OpenAsync();
                SqlCommand command = sqlc.CreateCommand();

                string newkey = Convert.ToBase64String(Key.Value);

                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                using (RijndaelManaged rm = new RijndaelManaged())
                {
                    byte[] buffer = new byte[32];
                    rng.GetBytes(buffer);

                    string salt = Convert.ToBase64String(buffer);
                    string givenPassword = password;

                    using (Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, buffer))
                        password = Convert.ToBase64String(rfc.GetBytes(32)) + ":" + salt;

                    rng.Dispose();

                    rm.Mode = CipherMode.ECB;
                    rm.Key = new SHA256Managed().ComputeHash(System.Text.Encoding.UTF8.GetBytes(givenPassword));

                    using (ICryptoTransform ctransform = rm.CreateEncryptor())
                    {
                        byte[] data = System.Text.Encoding.UTF8.GetBytes(newkey);
                        newkey = Convert.ToBase64String(ctransform.TransformFinalBlock(data, 0, data.Length));

                        data = null;
                    }

                    rm.Dispose();
                }

                command.CommandText = $"UPDATE [Users] SET [Password] = @newpassword, [Key] = @newkey WHERE [Username] = @username AND [Password] = @oldpassword;";
                command.Parameters.Add("@username", SqlDbType.NChar).Value = LoggedUser;
                command.Parameters.Add("@newpassword", SqlDbType.NVarChar).Value = password;
                command.Parameters.Add("@oldpassword", SqlDbType.NVarChar).Value = LoggedHash;
                command.Parameters.Add("@newkey", SqlDbType.NVarChar).Value = newkey;

                await command.ExecuteNonQueryAsync();
                OnPasswordUpdate?.Invoke();

                sqlc.Close();
            }
        }

        /// <summary>
        /// Deletes the User's account
        /// </summary>
        public static async Task DeleteAccount()
        {
            using (SqlConnection sqlc = new SqlConnection(connectionString))
            {
                await sqlc.OpenAsync();
                SqlCommand command = sqlc.CreateCommand();

                command.CommandText = $"DELETE FROM [Users] WHERE [Username] = @username AND [Password] = @password;";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = LoggedUser;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = LoggedHash;

                await command.ExecuteNonQueryAsync();
                OnAccountDelete?.Invoke();

                sqlc.Close();
            }
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        public static async Task Register(string username, string password, string ipaddress)
        {
            using (SqlConnection sqlc = new SqlConnection(connectionString))
            {
                await sqlc.OpenAsync();
                SqlCommand command = sqlc.CreateCommand();
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;

                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    byte[] buffer = new byte[32];
                    rng.GetBytes(buffer);
                    string salt = Convert.ToBase64String(buffer);

                    using (Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, buffer))
                        password = Convert.ToBase64String(rfc.GetBytes(32)) + ":" + salt;
                }

                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                command.Parameters.Add("@IPAddress", SqlDbType.NChar).Value = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(System.Text.Encoding.UTF8.GetBytes(ipaddress))).Replace("-", "").ToLower();
                command.Parameters.Add("@ErMessage", SqlDbType.NVarChar).Value = "User with same IPAdrress already exists.";

                command.CommandText = $"IF NOT EXISTS (SELECT [IPAddress] FROM [Users] WHERE [IPAddress] = @IPAddress) INSERT INTO [Users] VALUES (@Username, @Password, NULL, @IPAddress); ELSE RAISERROR (@ErMessage, 16, 1)";
                await command.ExecuteNonQueryAsync();
                OnAccountRegister?.Invoke();

                sqlc.Close();
            }
        }

        /// <summary>
        /// Gets the User's hashed password
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Password as SHA-256 hash</returns>
        public static async Task<string> GetUserHash(string username)
        {
            string data = null;

            using (SqlConnection sqlc = new SqlConnection(connectionString))
            {
                await sqlc.OpenAsync();
                SqlCommand command = sqlc.CreateCommand();
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;

                command.CommandText = $"SELECT Password FROM [Users] WHERE Username = @Username";

                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    if (dataReader.HasRows)
                    {
                        while (await dataReader.ReadAsync())
                        {
                            data = dataReader.GetString(0);
                        }
                    }
                }

                sqlc.Close();
            }

            return data;
        }
    }
}
