using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGenerator_programowanie_IV.Modules
{
    class Question
    {
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public int Time { get; set; }
        public List<Answer> Answers { get; set; }

        public Question(int questionNumber, string questionText, int time, List<Answer> answers)
        {
            this.QuestionNumber = questionNumber;
            this.QuestionText = questionText;
            this.Time = time;
            this.Answers = new List<Answer>();

            foreach (Answer answer in answers)
            {
                this.Answers.Add(answer);
            }
        }
    }
}
