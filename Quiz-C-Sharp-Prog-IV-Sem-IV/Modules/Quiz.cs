using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGenerator_programowanie_IV.Modules
{
    class Quiz
    {
        public string QuizName { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz(string quizName, List<Question> questions)
        {
            this.QuizName = quizName;
            this.Questions = new List<Question>();

            foreach (Question question in questions)
            {
                this.Questions.Add(question);
            }
        }
    }
}
