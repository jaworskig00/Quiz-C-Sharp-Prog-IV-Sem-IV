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

namespace Quiz_C_Sharp_Prog_IV_Sem_IV
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region NAWIGACJA

        // 1. MenuView
        private void StartQuiz_ButtonClick(object sender, RoutedEventArgs e)
        {
            MenuView.Visibility = Visibility.Collapsed;
            QuizView.Visibility = Visibility.Visible;
            SummaryView.Visibility = Visibility.Collapsed;
        }

        private void Exit_ButtonClik(object sender, RoutedEventArgs e)
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

        #endregion

        private void PreviousQuestion_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void NextQuestion_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
