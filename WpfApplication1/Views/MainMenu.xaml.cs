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

        public void StartGame(object sender, EventArgs e)
        {
            this.pickPlayerGrid.Visibility = System.Windows.Visibility.Visible;
            comboBox.SelectedIndex = 0;
        }

        public void Options(object sender, EventArgs e)
        {
            Switcher.pageSwitcher.Navigate(new Options());
        }

        public void Highscores(object sender, EventArgs e)
        {
            Switcher.pageSwitcher.Navigate(new Highscores());
        }

        public void SubmitPlayer()
        {
            controller.CreateHumanPlayer(pvm.Name, pvm.DiscColor);
            Switcher.pageSwitcher.Navigate(new LoadGame(this.controller));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text.Equals("Player name"))
            {
                nameTextBox.Text = "";
                nameTextBox.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(nameTextBox.Text)) 
            {
                this.initializePlaceholderText();
            }
        }

        private void initializePlaceholderText()
        {
            nameTextBox.Text = "Player name";
            nameTextBox.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
        }
    }
}
