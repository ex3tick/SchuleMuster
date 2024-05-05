using MySql.Data;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using WebApp.Model;

namespace WebApp.SqlDal;


public class UserDAL : LoginInterface
{
    string connectionStringLogin = "Server=194.164.193.128;Port=3306;Database=UserAccounts;Uid=root;Pwd=Chanatl21;";


    public bool CheckLogin(LoginTransferObjekt login) {
        using (MySqlConnection connection = new MySqlConnection(connectionStringLogin)) {
            try {
                if (string.IsNullOrEmpty(login.person.Username) || string.IsNullOrEmpty(login.person.Password)) {
                    return false;
                }

                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Users WHERE Username = @username", connection);
                command.Parameters.AddWithValue("@username", login.person.Username);

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {
                    string pwHash = reader.GetString(2); // Passwort-Hash auslesen

                    if (BCrypt.CheckPassword(login.person.Password, pwHash)) {
                        login.person.UserId = reader.GetInt32(0); // ID auslesen
                        if (!reader.IsDBNull(3)) {
                            login.person.IsAdmin = reader.GetInt32(3) == 1;
                        } else {
                            login.person.IsAdmin = false;
                        }
                        return true; 
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); 
                return false; 
            }
        }

        return false; // Rückgabe bei ungültigen Anmeldedaten oder Fehler
    }

    
            
           
         

    public bool Register(LoginTransferObjekt login)
    {
        string saltedPw = BCrypt.HashPassword(login.person.Password, BCrypt.GenerateSalt(12));
        bool isAllradyUser = UserExists(login);
        if (!isAllradyUser)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionStringLogin))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("INSERT INTO Users (Username, Password) VALUES (@username, @password)", connection);
                    command.Parameters.AddWithValue("@username", login.person.Username);
                    command.Parameters.AddWithValue("@password", saltedPw);

                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
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
                MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @username LIMIT1", connection);
                command.Parameters.AddWithValue("@username", login.person.Username);

                int userCount = Convert.ToInt32(command.ExecuteScalar());

                return userCount > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
    }

    public List<Person> GetAllUsers()
    {
        List<Person> users = new List<Person>();
        using (MySqlConnection connection = new MySqlConnection(connectionStringLogin))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM Users", connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Person person = new Person();
                person.UserId = reader.GetInt32(0);
                person.Username = reader.GetString(1);
                person.Password = "Leer";
                if (!reader.IsDBNull(3)) {
                    person.IsAdmin = reader.GetInt32(3) == 1;
                } else {
                    person.IsAdmin = false; // oder eine andere geeignete Standardbehandlung
                }
                users.Add(person);
            }
        }

        return users;
    }
    
}