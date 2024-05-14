using HashHelper;
using MySql.Data.MySqlClient;
using WebApp.Model;

namespace WebApp.SqlDal
{
    /// <summary>
    /// Data Access Layer for user operations.
    /// </summary>
    public class UserDAL : IAccessable
    {
        private readonly string connectionStringLogin;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDAL"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string for the database.</param>
        public UserDAL(string? connectionString)
        {
            connectionStringLogin = connectionString;
        }

        #region NormalUser

        /// <summary>
        /// Authenticates a user based on the provided login credentials.
        /// </summary>
        /// <param name="login">The login transfer object containing the user's credentials.</param>
        /// <returns>True if the login is successful, otherwise false.</returns>
        public bool Login(LoginTransferObjekt login)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionStringLogin))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM Users WHERE username = @username", connection);
                    command.Parameters.AddWithValue("@Username", login.person.Username);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        HashSaltModel hashSalt = new HashSaltModel
                        {
                            Password = reader.GetString("Password"),
                            Salt = reader.GetString("Salt")
                        };
                        if (HashHelper.HashHelper.ValidatePassword(login.person.Password, hashSalt.Salt, hashSalt.Password))
                        {
                            login.person.IsAdmin = reader.GetBoolean("IsAdmin");
                            login.loggedIn = true;
                            return true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="login">The login transfer object containing the user's registration details.</param>
        /// <returns>True if the registration is successful, otherwise false.</returns>
        public bool Register(LoginTransferObjekt login)
        {
            bool isExist = UserExists(login);

            if (!isExist)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionStringLogin))
                {
                    try
                    {
                        connection.Open();
                        HashSaltModel hashSalt = HashHelper.HashHelper.HashWithSalt(login.person.Password);
                        MySqlCommand command = new MySqlCommand("INSERT INTO Users (username, password, salt) VALUES (@username, @password, @salt)", connection);
                        command.Parameters.AddWithValue("@Username", login.person.Username);
                        command.Parameters.AddWithValue("@Password", hashSalt.Password);
                        command.Parameters.AddWithValue("@Salt", hashSalt.Salt);
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message, "Error");
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if a user already exists.
        /// </summary>
        /// <param name="login">The login transfer object containing the user's details.</param>
        /// <returns>True if the user exists, otherwise false.</returns>
        public bool UserExists(LoginTransferObjekt login)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionStringLogin))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM Users WHERE username = @username", connection);
                    command.Parameters.AddWithValue("@Username", login.person.Username);
                    MySqlDataReader reader = command.ExecuteReader();
                    return reader.Read();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        #endregion

        #region AdminMethods

        /// <summary>
        /// Updates a user's information.
        /// </summary>
        /// <param name="login">The login transfer object containing the user's updated details.</param>
        /// <returns>True if the update is successful, otherwise false.</returns>
        public bool UpdateUser(LoginTransferObjekt login)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionStringLogin))
            {
                try
                {
                    connection.Open();
                    HashSaltModel hashSalt = HashHelper.HashHelper.HashWithSalt(login.person.Password);
                    MySqlCommand command = new MySqlCommand("UPDATE Users SET username = @username, password = @password, salt = @salt, isAdmin = @isAdmin WHERE userId = @userId", connection);
                    command.Parameters.AddWithValue("@Username", login.person.Username);
                    command.Parameters.AddWithValue("@Password", hashSalt.Password);
                    command.Parameters.AddWithValue("@Salt", hashSalt.Salt);
                    command.Parameters.AddWithValue("@IsAdmin", login.person.IsAdmin);
                    command.Parameters.AddWithValue("@UserId", login.person.UserId);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Deletes a user based on their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to be deleted.</param>
        /// <returns>True if the deletion is successful, otherwise false.</returns>
        public bool DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resets a user's password.
        /// </summary>
        /// <param name="username">The username of the user whose password is to be reset.</param>
        /// <param name="newPassword">The new password for the user.</param>
        /// <returns>True if the password reset is successful, otherwise false.</returns>
        public bool ResetPassword(string username, string newPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a user's information based on their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to be retrieved.</param>
        /// <returns>A <see cref="Person"/> object containing the user's details.</returns>
        public Person GetUserById(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionStringLogin))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM Users WHERE userId = @userId", connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Person
                        {
                            UserId = reader.GetInt32("UserId"),
                            Username = reader.GetString("Username"),
                            Password = reader.GetString("Password"),
                            Salt = reader.GetString("Salt"),
                            IsAdmin = reader.GetBoolean("IsAdmin")
                        };
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            } 
            return  new Person();
        }



        #endregion
    }
}
