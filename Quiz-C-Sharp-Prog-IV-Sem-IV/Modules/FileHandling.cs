using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace QuizGenerator_programowanie_IV.Modules
{
    class FileHandling
    {
        CezarEncryption cezar = new CezarEncryption();
        public List<Quiz> quizzes = new List<Quiz>();
        string userDocumentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public void SaveToFile(Quiz quiz)
        {
            string json = JsonSerializer.Serialize(quiz);

            File.WriteAllText($"{userDocumentsPath}\\QUIZY{ quiz.QuizName}.json", cezar.EncryptText(json));
        }

        public List<Quiz> ReadFromFile()
        {
            quizzes.Clear();
            //C:\\Users\\Kiepson\\Desktop\\QUIZY\\ - scieżka Wojtka
            string[] files = Directory.GetFiles($"{userDocumentsPath}\\QUIZY");

            foreach (string file in files)
            {
                string text = File.ReadAllText(file);

                Quiz stuff = JsonSerializer.Deserialize<Quiz>(cezar.DecryptText(text));

                quizzes.Add(stuff);
            }

            return quizzes;
        }
    }
}
