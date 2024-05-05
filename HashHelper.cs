using System.Security.Cryptography;
using System.Text;

namespace HashHelper;

public static class HashHelper
{

    public static string HashGenerate(string Password, string Salt, string Pepper)
    {
        // Combine password and salt
        string combinedstring = Password + Salt;

        //Konvertieren in Byte-Array
        byte[] combinedBytes = Encoding.UTF8.GetBytes(combinedstring);

        //Erzeuge einen SHA256-Hash
        using (var sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(Pepper)))
        {
            //Berechne den Hash
            byte[] hashedBytes = sha256.ComputeHash(combinedBytes);

            //Konvertiere den Hash in einen Base64-String
            string base64Hash = System.Convert.ToBase64String(hashedBytes);

            return base64Hash;
        }
        
    }
    
  public static string SaltGenerate()
  {
      var rng = RandomNumberGenerator.Create(); // Definition des RNG
      byte[] saltBytes = new byte[16];
      rng.GetBytes(saltBytes);
      return Convert.ToBase64String(saltBytes);
  }
    
    
   
    public static HashSaltModel HashWithSalt(string password)
    {
    
        HashSaltModel model = new HashSaltModel();
        model.Salt = SaltGenerate();
        model.Password = HashGenerate(password, model.Salt, "Pepper");
        return model;
    }
 
    
    // mehtode hier für schreiben was über schon gesetzt salt  dann ein hash check erstellen kann 
    public static bool ValliedatePassword(string password,  string salt, string hashPassword)
    {
        string hash = HashGenerate(password, salt, "Pepper");
        return hash == hashPassword;
       
    }
}
