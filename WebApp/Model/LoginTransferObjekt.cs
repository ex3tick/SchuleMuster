namespace WebApp.Model;

public class LoginTransferObjekt
{
    public Person person { get; set; }
   public  bool loggedIn { get; set; }
  public  List<Person> Users = new List<Person>();
    
}