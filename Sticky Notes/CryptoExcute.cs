using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sticky_Notes
{
    class CryptoExcute
    {
        List<string> Encrypted = new List<string>();
        List<string> Decrypted = new List<string>();
        
        public void Encrypt(string input,string output)
        {
            var fileStream = new FileStream(@"C:\Notes\Notes.txt",FileMode.Open,FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.Unicode))
            {
                string line;
                string a;
                while ((line = streamReader.ReadLine()) != null)
                {
                    a = Convert.ToString(line);
                    Encrypted.Add(a);
                }

            }

            var fileStream1 = new FileStream(@"C:\Notes\Notes.txt", FileMode.Create);
            {
                using (var streamReader1 = new StreamReader(fileStream1, Encoding.Unicode))
                {
                    foreach (var item in Encrypted)
                    {
                        streamReader1.ReadLine();
                    }

                }
             }
        }
           
               

        public void Decrypt(string input, string output)
        {

        }
    }
}
