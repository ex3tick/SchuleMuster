namespace WebApp.Model;

public interface LoginInterface
{
    
    bool CheckLogin(LoginTransferObjekt login);
    bool Register(LoginTransferObjekt login);
    
    bool UserExists(LoginTransferObjekt login);
    
}