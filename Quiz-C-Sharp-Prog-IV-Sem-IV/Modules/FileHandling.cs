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
        public List<Quiz> quizzes = new List<Quiz>();
        public void SaveToFile(Quiz quiz)
        {
            //string json = JsonSerializer.Serialize(quiz);

            // tutaj będzie szyfrowanie

            // można tutaj dodać jak nam się będzie nudziło żeby program tworzył nowy folder w "Dokumentach" użytkownika i tam zapisywał quiz'y
            File.WriteAllText($"C:\\Users\\2000g\\OneDrive\\Pulpit\\QUIZY\\{quiz.QuizName}.txt", json);
        }

        public List<Quiz> ReadFromFile()
        {

            quizzes.Clear();
            string[] files = Directory.GetFiles("C:\\Users\\2000g\\OneDrive\\Pulpit\\Quizy\\");

            foreach (string file in files)
            {
                string text = File.ReadAllText(file);
                //Quiz stuff = JsonSerializer.Deserialize<Quiz>(text);

                quizzes.Add(stuff);
            }

            return quizzes;
        }
    }
}
