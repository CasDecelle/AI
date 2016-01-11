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
            if (this.index == 0)
                Switcher.pageSwitcher.Navigate(new MainMenu());
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
            this.explanations.Add("This is what the board looks like when you start a new game.\n\n"
                + "You and your opponent take turns, placing one piece each turn.\n\n"
                + "Valid moves are indicated with a dark green disc.\n\n"
                + "In this example, we will play with the black pieces. "
                + "We will begin the game by placing a disc in the red square.");
            this.explanations.Add("Hooray! \n\nYou have captured one of our opponent's pieces. "
                + "You have turned it into one of your own color.\n\n"
                + "Be aware though that next turn, your opponent can do the same!\n\n"
                + "As an example, he will now place a disc in the red square.");
            this.explanations.Add("You are lucky this time! \n\nYour opponent has only captured one of your pieces.\n\n"
                + "The game will go on, taking turns, until both players have no moves left.\n\n"
                + "Keep in mind that you or your opponent can continue playing one of you can not make a valid move.\n\n"
                + "Never give up though! The game can completely turn around in the last couple of moves.\n\n"
                + "Are you ready to face the challenge?\n\n"
                + "Hint: the corners are valuable, since they can never be recaptured.");
        }

    }
}
