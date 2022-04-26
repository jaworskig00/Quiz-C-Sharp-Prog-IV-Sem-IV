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
using System.Windows.Forms;
using QuizGenerator_programowanie_IV.Modules;

namespace Quiz_C_Sharp_Prog_IV_Sem_IV
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        FileHandling fileHandle;

        string userDocumentsPath;

        List<Quiz> quizzes;
        List<Question> questions;
        int questionTime;
        int questionIndex;

        string nick;
        Timer timer;
        List<double> questionScore;
        bool[,] correctAnswersList;
        int totalCorrectAnswers;
        double score;

        public MainWindow()
        {
            InitializeComponent();
            fileHandle = new FileHandling();

            userDocumentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (!Directory.Exists($"{userDocumentsPath}\\QUIZY"))
            {
                Directory.CreateDirectory($"{userDocumentsPath}\\QUIZY");
            }

            quizzes = new List<Quiz>();
            questions = new List<Question>();
            questionTime = 0;
            questionIndex = 0;

            nick = "";
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(UpdateTimer);
            questionScore = new List<double>();
            totalCorrectAnswers = 0;
            score = 0;

            UpdateQuizListBox();
        }

        #region NAWIGACJA

        // 1. MenuView
        private void StartQuiz_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (QuizListBox.SelectedItem is null)
            {
                System.Windows.MessageBox.Show("Proszę wybrać quiz");
                return;
            }

            MenuView.Visibility = Visibility.Collapsed;
            QuizView.Visibility = Visibility.Visible;
            SummaryView.Visibility = Visibility.Collapsed;

            nick = NickText.Text;

            questions.Clear();
            questionIndex = 0;
            totalCorrectAnswers = 0;
            questionScore.Clear();

            foreach (Question question in quizzes[QuizListBox.SelectedIndex].Questions)
            {
                questions.Add(question);
            }

            correctAnswersList = new bool[questions.Count(), 4];

            questionTime = questions[questionIndex].Time;

            QuizTimer.Text = $"Pozostały czas: {questionTime} sek";
            QuizName.Text = quizzes[QuizListBox.SelectedIndex].QuizName;
            QuestionText.Text = questions[questionIndex].QuestionText;
            AnswerA.Text = questions[questionIndex].Answers[0].AnswerText;
            AnswerB.Text = questions[questionIndex].Answers[1].AnswerText;
            AnswerC.Text = questions[questionIndex].Answers[2].AnswerText;
            AnswerD.Text = questions[questionIndex].Answers[3].AnswerText;
            IsRightA.IsChecked = false;
            IsRightB.IsChecked = false;
            IsRightC.IsChecked = false;
            IsRightD.IsChecked = false;

            timer.Start();
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

            timer.Stop();
        }

        public void GoToSummaryView()
        {
            MenuView.Visibility = Visibility.Collapsed;
            QuizView.Visibility = Visibility.Collapsed;
            SummaryView.Visibility = Visibility.Visible;

            //logika
            timer.Stop();
            WelcomeText.Text = $"{nick}, ukończyłeś nasz quiz:";
            Title.Text = QuizName.Text;
            int totalActualCorrectAnswers = 0;
            score = 0;

            for (int i = 0; i < questions.Count(); i++)
            {
                foreach (Answer answer in questions[i].Answers)
                {
                    if ((bool)answer.IsCorrect)
                    {
                        totalActualCorrectAnswers += 1;
                    }
                }
            }

            CorrectQuestions.Text = $"{totalCorrectAnswers} na {totalActualCorrectAnswers} poprawnych odpowiedzi";

            for (int i = 0; i < questionScore.Count(); i++)
            {
                score += questionScore[i];
            }

            Score.Text = $"{score} punktów";

            // Wypełnianie listboxa ze szczegółami
            DetailsListBox.Items.Clear();

            for (int i = 0; i < questions.Count(); i++)
            {
                if (correctAnswersList[i, 0] == questions[i].Answers[0].IsCorrect &&
                    correctAnswersList[i, 1] == questions[i].Answers[1].IsCorrect &&
                    correctAnswersList[i, 2] == questions[i].Answers[2].IsCorrect &&
                    correctAnswersList[i, 3] == questions[i].Answers[3].IsCorrect)
                {
                    DetailsListBox.Items.Add($"{questions[i].QuestionText} - poprawne");
                }
                else
                {
                    Expander expander = new Expander();
                    StackPanel stackPanel = new StackPanel();
                    Console.WriteLine(questions[i].Answers.Count());
                    for (int j = 0; j < questions[i].Answers.Count(); j++)
                    {
                        TextBlock textBlock = new TextBlock();
                        if ((bool)questions[i].Answers[j].IsCorrect)
                        {
                            textBlock.Text = questions[i].Answers[j].AnswerText;
                            textBlock.Foreground = new SolidColorBrush(Colors.Green);
                        }
                        else
                        {
                            textBlock.Text = questions[i].Answers[j].AnswerText;
                            textBlock.Foreground = new SolidColorBrush(Colors.Red);
                        }

                        stackPanel.Children.Add(textBlock);
                    }

                    expander.Header = $"{questions[i].QuestionText} - błędnie";
                    expander.Content = stackPanel;
                    DetailsListBox.Items.Add(expander);
                }

            }
        }

        #endregion


        #region OBSŁUGA PYTAŃ

        private void NextQuestion_ButtonClick(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            double timeLeft = Convert.ToDouble(QuizTimer.Text.Substring(QuizTimer.Text.Length - 4 - questions[questionIndex].Time.ToString().Length, questions[questionIndex].Time.ToString().Length));

            correctAnswersList[questionIndex, 0] = (bool)IsRightA.IsChecked;
            correctAnswersList[questionIndex, 1] = (bool)IsRightB.IsChecked;
            correctAnswersList[questionIndex, 2] = (bool)IsRightC.IsChecked;
            correctAnswersList[questionIndex, 3] = (bool)IsRightD.IsChecked;

            double correctAnswers = 0;
            if ((bool)IsRightA.IsChecked && (IsRightA.IsChecked == questions[questionIndex].Answers[0].IsCorrect)) correctAnswers += 1;
            if ((bool)IsRightB.IsChecked && (IsRightB.IsChecked == questions[questionIndex].Answers[1].IsCorrect)) correctAnswers += 1;
            if ((bool)IsRightC.IsChecked && (IsRightC.IsChecked == questions[questionIndex].Answers[2].IsCorrect)) correctAnswers += 1;
            if ((bool)IsRightD.IsChecked && (IsRightD.IsChecked == questions[questionIndex].Answers[3].IsCorrect)) correctAnswers += 1;

            totalCorrectAnswers += Convert.ToInt32(correctAnswers);

            double actualCorrectAnswers = 0;
            foreach (Answer answer in questions[questionIndex].Answers)
            {
                if ((bool)answer.IsCorrect)
                {
                    actualCorrectAnswers += 1;
                }
            }

            questionScore.Add(correctAnswers / actualCorrectAnswers * timeLeft / Convert.ToDouble(questions[questionIndex].Time) * 100);
            
            questionIndex += 1;
            
            if (questionIndex >= questions.Count())
            {
                GoToSummaryView();
            }
            else
            {
                if (questions.Any(q => q.QuestionNumber == questionIndex))
                {
                    questionTime = questions[questionIndex].Time;
                    QuizTimer.Text = $"Pozostały czas: {questionTime.ToString()} sek";
                    QuestionText.Text = questions[questionIndex].QuestionText;
                    AnswerA.Text = questions[questionIndex].Answers[0].AnswerText;
                    AnswerB.Text = questions[questionIndex].Answers[1].AnswerText;
                    AnswerC.Text = questions[questionIndex].Answers[2].AnswerText;
                    AnswerD.Text = questions[questionIndex].Answers[3].AnswerText;
                    IsRightA.IsChecked = false;
                    IsRightB.IsChecked = false;
                    IsRightC.IsChecked = false;
                    IsRightD.IsChecked = false;

                    timer.Start();
                }
                else
                {
                    System.Windows.MessageBox.Show("Error: No More Questions");
                }
            }
        }

        #endregion


        #region FUNKCJE DODATKOWE

        private void ShowMore_ButtonClick(object sender, RoutedEventArgs e)
        {
            DetailsListBox.Visibility = DetailsListBox.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
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

        public void UpdateTimer(Object obj, EventArgs e)
        {
            int timerTemp = Convert.ToInt32(QuizTimer.Text.Substring(QuizTimer.Text.Length - 4 - questions[questionIndex].Time.ToString().Length, questions[questionIndex].Time.ToString().Length)) - 1;

            if (timerTemp <= 0)
            {
                object sender = new object(); 
                RoutedEventArgs rEvent = new RoutedEventArgs();
                NextQuestion_ButtonClick(sender, rEvent);
                return;
            }

            string timerFormated = $"Pozostały czas: {timerTemp} sek";
            QuizTimer.Text = timerFormated;
        }

        #endregion
    }
}
