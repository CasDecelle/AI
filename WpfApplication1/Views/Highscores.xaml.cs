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
    /// Interaction logic for Highscores.xaml
    /// </summary>
    public partial class Highscores : Page
    {
        List<Player> highscoreList;

        public Highscores()
        {
            HighscoresReadWriteHandler highscoreHandler = new HighscoresReadWriteHandler("../../../Othello/Resources/Highscores.csv");
            List<Player> highscores = highscoreHandler.Read().OrderByDescending(o => o.Score).ToList();
            highscoreList = new List<Player>();
            int count = highscores.Count();
            if (count > 10) count = 10;
            for (int i = 0; i < count; i++)
            {
                highscoreList.Add(highscores.ElementAt(i));
            }
            InitializeComponent();
            highscoreGrid.ItemsSource = highscoreList;
        }

        public void MainMenu(object sender, EventArgs e)
        {
            Switcher.pageSwitcher.Navigate(new MainMenu());
        }
    }
}
