using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGenerator_programowanie_IV.Modules
{
    class Answer
    {
        public string AnswerText { get; set; }
        public bool? IsCorrect { get; set; }

        public Answer(string answerText, bool? isCorrect)
        {
            this.AnswerText = answerText;
            this.IsCorrect = isCorrect;
        }
    }
}
