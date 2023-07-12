using System.Text;

namespace Domain.Share.Utils.Helpers
{
    public class MD5Helper
    {
        /// <summary>
        /// md5计算（32）
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string MD5(string src)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(src));
                var sb = new StringBuilder();
                foreach (var d in data)
                {
                    sb.Append(d.ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }
    }
}
