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

namespace CSHP_320A_Assignments_Homework_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool toggleTurn = true;
        private int[][] winningPositions = new int[8][]
          {
            new int[3] {0,1,2},
            new int[3] {3,4,5},
            new int[3] {6,7,8},
            new int[3] {0,3,6},
            new int[3] {1,4,7},
            new int[3] {2,5,8},
            new int[3] {0,4,8},
            new int[3] {2,4,6}
          };
        private bool isGameFinished;
        private int turnCount = 1;


        public MainWindow()
        {
            InitializeComponent();
            resetGameBoard();
        }
        private void UxNewGame_Click(object sender, RoutedEventArgs e)
        {
            resetGameBoard();
        }
        private void UxExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(this, "Are you sure to exit the game?", "Exit", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btnSender = (Button)sender;
            string t = (string)btnSender.Tag;

            if (btnSender.Content == null || btnSender.Content.ToString() == "")
            {
                if (toggleTurn == true)
                {
                    btnSender.Content = "X";
                    uxTurn.Text = "Player 2 (O) turn";
                }
                if (toggleTurn == false)
                {
                    btnSender.Content = "O";
                    uxTurn.Text = "Player 1 (X) turn";
                }
                toggleTurn = !(toggleTurn);
                turnCount++;
                CheckWinner();
            }
        }
        private void CheckWinner()
        {
            string btnContent = "";
            string winnerContent = "";
            foreach (int[] p in winningPositions)
            {
                for (int i = 0; i <= 2; i++)
                {
                    Button button = (Button)uxGrid.Children[p[i]];
                    if (i == 0)
                        btnContent = button.Content.ToString();
                    if (btnContent != button.Content.ToString())
                        break;
                    if (btnContent == "")
                        break;
                    if (i == 2 && btnContent == button.Content.ToString())
                    {
                        isGameFinished = true;
                        winnerContent = btnContent;
                    }
                }
            }
            if (isGameFinished)
            {
                if (winnerContent == "X")
                {
                    uxTurn.Text = "Player 1 (X) wins! Congratulations!";
                }
                else if (winnerContent == "O")
                {
                    uxTurn.Text = "Player 2 (O) wins! Congratulations!";
                }
                disableGameBoard();
            }
            if (turnCount == 10 && !isGameFinished)
            {
                uxTurn.Text = "Tie!";
                disableGameBoard();
            }
        }

        private void resetGameBoard()
        {
            isGameFinished = false;
            toggleTurn = true;
            turnCount = 1;
            uxTurn.Text = "Player 1 (X) turn";
            foreach (var item in uxGrid.Children)
            {
                if (item is Button)
                {
                    Button b = (Button)item;
                    b.Content = "";
                    b.IsEnabled = true;

                }
            }
        }
        private void disableGameBoard()
        {
            foreach (var item in uxGrid.Children)
            {
                if (item is Button)
                {
                    Button b = (Button)item;
                    b.IsEnabled = false;
                }
            }
        }
    }
}
