using Othello.Model;
using Othello.Utility;
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

namespace WpfGUI.Views
{
    /// <summary>
    /// Interaction logic for Scores.xaml
    /// </summary>
    public partial class Highscores : Page
    {
        public Highscores()
        {
            HighscoresHandler highscoreHandler = new HighscoresHandler("../../../ClassLibrary1/Resources/Highscores.csv");
            DataContext = highscoreHandler.Read();
            InitializeComponent();

        }

        public void MainMenu(object sender, EventArgs e)
        {
            Switcher.pageSwitcher.Navigate(new MainMenu());
        }
    }
}
