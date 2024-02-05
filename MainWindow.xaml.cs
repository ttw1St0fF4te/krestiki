using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
using System.Xml.Serialization;

namespace krestiki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button[] buttons;
        Random random = new Random();
        Label[] labels;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[9] { _1, _2, _3, _4, _5, _6, _7, _8, _9 };
            labels = new Label[1] { gameResult };
            LockAllButtons();
        }
        public int gameCount = 1;
        private void LockAllButtons()
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i].IsEnabled = false;
            }
        }
        private void UnlockAllButtons()
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i].IsEnabled = true;
            }
        }
        private void ClearAllContent()
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i].Content = " ";
            }
        }
        private void WinGuesser(int gameCount)
        {
            int[,] chances = new int[8, 3] // Все случаи выйгрыша
{
                { 0, 1, 2 }, // По оси X
                { 3, 4, 5 },
                { 6, 7, 8 },
                { 0, 3, 6 }, // По оси Y
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 2, 4, 6 }, // Диагональ
                { 0, 4, 8 }
};
            int check = 0;

            for (int i = 0; i < chances.GetLength(0); i++) // Победы и поражения
            {
                for (int j = 0; j < chances.GetLength(1); j++)
                {
                    if (buttons[chances[i, j]].Content == "X" &&
                        buttons[chances[i, j + 1]].Content == "X" &&
                        buttons[chances[i, j + 2]].Content == "X")
                    {
                        LockAllButtons();
                        if (gameCount % 2 == 0)
                        {
                            labels[0].Content = "Победа ;3";
                        }
                        if (gameCount % 2 != 0)
                        {
                            labels[0].Content = "ПроигрышЬЬЬ(((";
                        }
                        startGameButton.IsEnabled = true;
                        return;
                    }
                    else if (buttons[chances[i, j]].Content == "O" &&
                        buttons[chances[i, j + 1]].Content == "O" &&
                        buttons[chances[i, j + 2]].Content == "O")
                    {
                        LockAllButtons();
                        if (gameCount % 2 == 0)
                        {
                            labels[0].Content = "ПроигрышЬЬЬ(((";
                        }
                        if (gameCount % 2 != 0)
                        {
                            labels[0].Content = "Победа ;3";
                        }
                        startGameButton.IsEnabled = true;
                        return;
                    }
                    break;
                }
            }
            for (int i = 0; i < 9; i++) // Ничья 
            {
                if (buttons[i].IsEnabled == false)
                {
                    check++;
                    if (check == 9)
                    {
                        LockAllButtons();
                        labels[0].Content = "Ничя -_-";
                        startGameButton.IsEnabled = true;
                        return;
                    }
                }
            }
        }
        private void _1_Click(object sender, RoutedEventArgs e)
        {

            int knopka = random.Next(0, 9);
            if (gameCount % 2 == 0)
                (sender as Button).Content = "X";
            if (gameCount % 2 != 0)
                (sender as Button).Content = "O";
            (sender as Button).IsEnabled = false;

            WinGuesser(gameCount);

            for (int i = 0; i < 9; i++)
            {
                if (buttons[knopka].IsEnabled == false)
                {
                    knopka = random.Next(0, 9);
                    continue;
                }
                if (gameCount % 2 == 0)
                    buttons[knopka].Content = "O";
                if (gameCount % 2 != 0)
                    buttons[knopka].Content = "X";
                buttons[knopka].IsEnabled = false;
                break;
            }
            WinGuesser(gameCount);
        }

        private void startGameButton_Click(object sender, RoutedEventArgs e)
        {
            gameCount++;
            labels[0].Content = " ";
            ClearAllContent();
            UnlockAllButtons();
            startGameButton.IsEnabled = false;
        }
    }
}