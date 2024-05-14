using WebApp.SqlDal;

namespace WebApp.Model
{
    /// <summary>
    /// Provides services related to user authentication and registration.
    /// </summary>
    public class Services
    {
        private readonly IAccessable _userDal;

        /// <summary>
        /// Initializes a new instance of the <see cref="Services"/> class.
        /// </summary>
        /// <param name="configuration">The configuration object to get the connection string.</param>
        public Services(IConfiguration configuration)
        {
            string _connectionString = configuration.GetConnectionString("DefaultConnection");
            _userDal = new UserDAL(_connectionString);
        }

        /// <summary>
        /// Checks if the user is logged in and returns the corresponding <see cref="LoginTransferObjekt"/>.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="isAdmin">A boolean indicating whether the user is an admin.</param>
        /// <returns>A <see cref="LoginTransferObjekt"/> containing the user's login information.</returns>
        public LoginTransferObjekt Islogedin(string username, bool isAdmin)
        {
            LoginTransferObjekt login;
            try
            {
                login = new LoginTransferObjekt
                {
                    person = new Person
                    {
                        Username = username,
                        IsAdmin = isAdmin
                    }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return login;
        }

        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <param name="login">The login transfer object containing the user's credentials.</param>
        /// <returns>True if the login is successful, otherwise false.</returns>
        public bool Login(LoginTransferObjekt login)
        {
            return _userDal.Login(login);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="login">The login transfer object containing the user's registration details.</param>
        /// <returns>True if the registration is successful, otherwise false.</returns>
        public bool Register(LoginTransferObjekt login)
        {
            return _userDal.Register(login);
        }
    }
}
