using System;
using System.Collections.Generic;
using System.Linq;
using System.gm;
using System.Text;
using System.Threading.Tasks;

namespace System.gm
{
    public class UserLogin
    {
        public static void GenerateNewPwdHashSalt(string originalPwd, out string hash, out string salt)
        {
            salt = BCrypt.Net.BCrypt.GenerateSalt();
            hash = BCrypt.Net.BCrypt.HashPassword(originalPwd, salt);
        }

        public static string GetPwdHash(string originalPwd, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(originalPwd, salt);
        }

    }
}
