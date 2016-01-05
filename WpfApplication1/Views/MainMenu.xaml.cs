using Othello.Controller;
using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfGUI.ViewModels;

namespace WpfGUI.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page, ISubmitPlayer
    {
        private Game controller = new Game();
        private PlayerViewModel pvm;

        public MainMenu()
        {
            InitializeComponent();
            this.pvm = new PlayerViewModel(this);
            DataContext = pvm;
            this.initializePlaceholderText();
        }

        public void NavigateStartGame(object sender, EventArgs e)
        {
            this.pickPlayerGrid.Visibility = System.Windows.Visibility.Visible;
            comboBox.SelectedIndex = 0;
        }

        public void NavigateCredits(object sender, EventArgs e)
        {
            Switcher.pageSwitcher.Navigate(new Credits());
        }

        public void NavigateHighscores(object sender, EventArgs e)
        {
            Switcher.pageSwitcher.Navigate(new Highscores());
        }

        public void SubmitPlayer()
        {
            controller.CreateHumanPlayer(pvm.Name, pvm.DiscColor);
            Disc disc = new Disc(pvm.DiscColor);
            disc.InvertDisc();
            if (pvm.IsAIOpponent)
            {
                controller.CreateRoboticPlayer(pvm.OpponentName, disc.Color);
            }
            else
            {
                controller.CreateHumanPlayer(pvm.OpponentName, disc.Color);
            }
            Switcher.pageSwitcher.Navigate(new LoadGame(this.controller));
        }

        private void GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Equals("Player 1") || textBox.Text.Equals("Player 2"))
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

        private void LostFocus(object sender, RoutedEventArgs e)
        {
            this.initializePlaceholderText();
        }

        private void initializePlaceholderText()
        {
            if (String.IsNullOrWhiteSpace(player1Name.Text)){
                player1Name.Text = "Player 1";
                player1Name.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
            }
            if (String.IsNullOrWhiteSpace(player2Name.Text)){
                player2Name.Text = "Player 2";
                player2Name.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
            }
        }
    }
}
