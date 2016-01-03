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
        List<Player> highscoreList;

        /*List<Player> HighscoreList
        {
            get { return this.highscoreList; }
            set { this.highscoreList = value; }
        }*/

        public Highscores()
        {
            HighscoresReadWriteHandler highscoreHandler = new HighscoresReadWriteHandler("../../../ClassLibrary1/Resources/Highscores.csv");
            highscoreList = highscoreHandler.Read();
            InitializeComponent();
            highscoreGrid.ItemsSource = highscoreList;
        }

        public void MainMenu(object sender, EventArgs e)
        {
            Switcher.pageSwitcher.Navigate(new MainMenu());
        }
    }
}
