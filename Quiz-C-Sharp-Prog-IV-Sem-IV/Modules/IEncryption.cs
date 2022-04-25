using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGenerator_programowanie_IV.Modules
{
    public interface IEncryption
    {
        string EncryptText(string text);
        string DecryptText(string text);
    }
}
