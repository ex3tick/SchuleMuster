namespace WebApp.Model;

public interface IAccessable
{
    bool Login(LoginTransferObjekt login);
    bool Register(LoginTransferObjekt login);
    bool UserExists(LoginTransferObjekt login);
    bool UpdateUser(LoginTransferObjekt login);
    bool DeleteUser(int userId);
    bool ResetPassword(string username, string newPassword);
    Person GetUserById(int userId);
 
}