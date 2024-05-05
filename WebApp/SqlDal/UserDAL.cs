using HashHelper;
using MySql.Data.MySqlClient;
using WebApp.Model;

namespace WebApp.SqlDal;


public class UserDAL : IAccessable
{
    private readonly string connectionStringLogin;

    public UserDAL(string? connectionString)
    {
        connectionStringLogin = connectionString;
    }

    
    #region NormalUser

    


    /// <summary>
    /// 
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>

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
                    HashSaltModel hashSalt = new HashSaltModel();
                    hashSalt.Password = reader.GetString("Password");
                    hashSalt.Salt = reader.GetString("Salt");
                    if (HashHelper.HashHelper.ValidatePassword( login.person.Password, hashSalt.Salt, hashSalt.Password))
                    {
                        login.person.IsAdmin = reader.GetBoolean("IsAdmin");
                        login.loggedIn = true;
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
    /// 
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
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

  
    public bool UpdateUser(LoginTransferObjekt login)
    {
        throw new NotImplementedException();
    }

    public bool DeleteUser(int userId)
    {
        throw new NotImplementedException();
    }

    public bool ResetPassword(string username, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Person GetUserById(int userId)
    {
        throw new NotImplementedException();
    }

    

    #endregion

   
}
