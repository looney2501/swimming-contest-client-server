using System.Security.Cryptography;
using System.Text;

namespace MPP_lab_project.Utils;

public static class PasswordHashingUtils
{
    public static string MD5Hashing(string password)
    {
        MD5 md5 = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(password);
        byte[] hashBytes = md5.ComputeHash(inputBytes);
     
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("X2"));
        }
        return sb.ToString();
    }
}