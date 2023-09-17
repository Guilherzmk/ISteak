using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Repositories.Criptographys
{
    public class Cryptography
    {

        public static byte[] CreateHash(string password, string salt = "9B14CC95A5118D3432CA6B195EDB9C5DAA13E36B393E22206092CC46C7EEB62A")
        {
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
            argon2.Salt = Encoding.UTF8.GetBytes(salt);
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 128;

            return argon2.GetBytes(32);


        }




    }
}
