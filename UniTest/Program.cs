using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTest
{
    class Program
    {
        static void Main(string[] args)
        {
             var a = PasswordStorage.CreateHash("abc");
            string pass = "sha1:64000:18:mfw65jrNvjhuqnBa3maneB32jCwWdort:ZIziTLLSJMTeWBarHiDItonQ";
            // Console.Write("heheh");
            var b = PasswordStorage.VerifyPassword("abc", pass);
            string va = "";
        }
    }
}
