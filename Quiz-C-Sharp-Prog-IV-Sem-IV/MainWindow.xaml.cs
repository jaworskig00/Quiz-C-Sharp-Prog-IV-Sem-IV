using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using QuizGenerator_programowanie_IV.Modules;

namespace Quiz_C_Sharp_Prog_IV_Sem_IV
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileHandling fileHandle;
        CezarEncryption encryptionHanlde;

        string previousLocation;
        string quizOldName;
        string nick;

        List<Quiz> quizzes;
        List<Answer> answers;
        List<Question> questions;
        int questionIndex;

        public MainWindow()
        {
            InitializeComponent();
            fileHandle = new FileHandling();
            encryptionHanlde = new CezarEncryption();

            previousLocation = "";
            quizOldName = "";
            nick = "";

            quizzes = new List<Quiz>();
            answers = new List<Answer>();
            questions = new List<Question>();
            questionIndex = 0;

            UpdateQuizListBox();
        }

        #region NAWIGACJA

        // 1. MenuView
        private void StartQuiz_ButtonClick(object sender, RoutedEventArgs e)
        {
            MenuView.Visibility = Visibility.Collapsed;
            QuizView.Visibility = Visibility.Visible;
            SummaryView.Visibility = Visibility.Collapsed;

            nick = NickText.Text;

            questions.Clear();
            questionIndex = 0;

            foreach (Question question in quizzes[QuizListBox.SelectedIndex].Questions)
            {
                questions.Add(question);
            }

            QuizName.Text = quizzes[QuizListBox.SelectedIndex].QuizName;
            QuestionText.Text = questions[questionIndex].QuestionText;
            AnswerA.Text = questions[questionIndex].Answers[0].AnswerText;
            AnswerB.Text = questions[questionIndex].Answers[1].AnswerText;
            AnswerC.Text = questions[questionIndex].Answers[2].AnswerText;
            AnswerD.Text = questions[questionIndex].Answers[3].AnswerText;
        }

        private void Exit_ButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // 2. QuizView and SummaryView
        private void GoToMenu_ButtonClick(object sender, RoutedEventArgs e)
        {
            MenuView.Visibility = Visibility.Visible;
            QuizView.Visibility = Visibility.Collapsed;
            SummaryView.Visibility = Visibility.Collapsed;
        }

        public void GoToSummaryView()
        {
            MenuView.Visibility = Visibility.Collapsed;
            QuizView.Visibility = Visibility.Collapsed;
            SummaryView.Visibility = Visibility.Visible;

            //logika
        }

        #endregion

        private void NextQuestion_ButtonClick(object sender, RoutedEventArgs e)
        {
            questionIndex += 1;

            if (questionIndex >= questions.Count())
            {
                Console.WriteLine(questions.Count());
                GoToSummaryView();
            }
            else
            {
                if (questions.Any(q => q.QuestionNumber == questionIndex))
                {
                    QuestionText.Text = questions[questionIndex].QuestionText;
                    AnswerA.Text = questions[questionIndex].Answers[0].AnswerText;
                    AnswerB.Text = questions[questionIndex].Answers[1].AnswerText;
                    AnswerC.Text = questions[questionIndex].Answers[2].AnswerText;
                    AnswerD.Text = questions[questionIndex].Answers[3].AnswerText;
                    IsRightA.IsChecked = false;
                    IsRightB.IsChecked = false;
                    IsRightC.IsChecked = false;
                    IsRightD.IsChecked = false;
                }
                else
                {
                    MessageBox.Show("Error: No More Questions");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void UpdateQuizListBox()
        {
            QuizListBox.Items.Clear();
            quizzes.Clear();

            quizzes = fileHandle.ReadFromFile();

            

            foreach (Quiz quiz in quizzes)
            {
                QuizListBox.Items.Add(quiz.QuizName);
            }
        }
    }
}
