using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuizGenerator_programowanie_IV.Modules
{
    class CezarEncryption : IEncryption
    {
        int key = 2; // można dodać żeby było to dodawane jako argument do klasy lub metod
        public string EncryptText(string text)
        {
            char[] textToArray = text.ToCharArray();
            string outputText = string.Empty;

            for (int i = 0; i < textToArray.Length; i++)
            {
                // jeśli chcemy żeby szyfrowane byly tylko litery
                //if (!char.IsLetter(textToArray[i]))
                //{
                //    continue;
                //}
                outputText += (char)(textToArray[i] + key);
            }

            return outputText;

        }

        public string DecryptText(string text)
        {
            char[] textToArray = text.ToCharArray();
            string outputText = string.Empty;

            for (int i = 0; i < textToArray.Length; i++)
            {
                // jeśli chcemy żeby szyfrowane byly tylko litery
                //if (!char.IsLetter(textToArray[i]))
                //{
                //    continue;
                //}
                outputText += (char)(textToArray[i] - key);
            }
            return outputText;
        }
    }
}
