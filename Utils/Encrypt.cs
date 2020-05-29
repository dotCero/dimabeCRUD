using System.Security.Cryptography;
using System.Text;

namespace dimabeCRUD.Utils
{
    public class Encrypt
    {
        public string EncSHA256(string secret)
        {
            var method = SHA256.Create();
            var encoding = new ASCIIEncoding();
            byte[] streamSize;
            var sb = new StringBuilder();
            streamSize = method.ComputeHash(encoding.GetBytes(secret));
            foreach (var t in streamSize)
            {
                sb.AppendFormat("{0:x2}", t);
            }

            return sb.ToString();
        }
    }
}