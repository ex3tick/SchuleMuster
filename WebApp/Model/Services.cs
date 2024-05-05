using WebApp.SqlDal;

namespace WebApp.Model;

public class Services
{
    private readonly IAccessable _userDal;

   public  Services(IConfiguration configuration)
    {
        string _connectionString = configuration.GetConnectionString("DefaultConnection");
        _userDal = new UserDAL(_connectionString);
        
    }
   
   /// <summary>
   /// 
   /// </summary>
   /// <param name="usernamme"></param>
   /// <param name="isAdmin"></param>
   /// <returns></returns>
   
    public LoginTransferObjekt Islogedin(string usernamme,bool isAdmin)
    {
        LoginTransferObjekt login;
        try
        {
             login = new LoginTransferObjekt();
            login.person = new Person();
            login.person.Username = usernamme;
            login.person.IsAdmin = isAdmin == true;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        
        
        return login;
    }

   /// <summary>
   /// 
   /// </summary>
   /// <param name="login"></param>
   /// <returns></returns>
    public bool Login(LoginTransferObjekt login)
    {

        if (_userDal.Login(login))
        {
            return true;
        }
        else return false;
    }
   /// <summary>
   /// 
   /// </summary>
   /// <param name="login"></param>
   /// <returns></returns>
    public bool Register(LoginTransferObjekt login)
    {
        if (_userDal.Register(login))
        {
            return true;
        }
        else return false;
    }
  
}