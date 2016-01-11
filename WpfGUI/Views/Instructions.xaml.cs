using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for Instructions.xaml
    /// </summary>
    public partial class Instructions : Page
    {
        private List<string> imagePaths;
        private List<string> explanations;
        private int index = 0;


        public Instructions()
        {
            this.InitializeLists();
            InitializeComponent();
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            this.ChangeImage();
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            if (this.index == 2)
                Switcher.pageSwitcher.Navigate(new MainMenu());
            if (index < 2)
                this.index++;
            this.ChangeImage();
        }

        private void Previous(object sender, RoutedEventArgs e)
        {
            if (index > 0)
                this.index--;
            this.ChangeImage();
        }

        private void ChangeImage()
        {
            BitmapImage b = new BitmapImage();
            string imgPath = Directory.GetCurrentDirectory() + this.imagePaths.ElementAt(this.index);
            b.BeginInit();
            b.UriSource = new Uri(imgPath);
            b.EndInit();
            instructionImage.Source = b;

            instructionText.Text = this.explanations.ElementAt(this.index);
        }

        private void InitializeLists()
        {
            this.imagePaths = new List<string>();
            this.imagePaths.Add("\\..\\..\\Resources\\move1.png");
            this.imagePaths.Add("\\..\\..\\Resources\\move2.png");
            this.imagePaths.Add("\\..\\..\\Resources\\move3.png");

            this.explanations = new List<string>();
            this.explanations.Add("This is the state of the board when you start a new game.\n"
                + "During your turn, you will have to place one disc.\n"
                + "A disc can be placed if it aligns with another piece of yours\n"
                + "and at least one opponent piece can be captured.\n"
                + "The dark green discs are possible moves you can make.\n"
                + "In this example, we will be placing the disc on the right.\n");
            this.explanations.Add("The white piece that was surrounded by the last move is now black.\n"
                + "All pieces that get surrounded by an opponent move will be captured.\n"
                + "Watch out though, as an opponent can recapture your pieces.\n\n"
                + "Hint: the game can sometimes completely turn around in the final moves. \n\n");
            this.explanations.Add("The game ends when both players have no moves left.\n"
                + "The player with the highest number of pieces is the winner.\n\n"
                + "Hint: the corners are valuable, since they can never be recaptured.\n\n\n");
        }

    }
}
