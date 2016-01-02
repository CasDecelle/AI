using Othello.Controller;
using Othello.Model;
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
using System.Threading;
using WpfGUI.ViewModels;
using System.Windows.Threading;

namespace WpfGUI.Views
{
    /// <summary>
    /// Interaction logic for LoadGame.xaml
    /// </summary>
    public partial class LoadGame : Page
    {
        private Game controller;
        private PlayerViewModel pvm;

        public LoadGame(Game controller)
        {
            this.controller = controller;
            InitializeComponent();
            StartGame();
        }

        public void ClickOnDisc(object sender, EventArgs e)
        {
            Ellipse disc = sender as Ellipse;
            int row = Grid.GetRow(disc);
            int col = Grid.GetColumn(disc);
            controller.ExecuteValidMove(row, col);
            this.SwitchPlayer(this.controller.CurrentPlayer);
            UpdateBoard();
            this.AllowUIUpdate();
            Thread.Sleep(1000);
            controller.ExecuteAIMove(row, col);
            this.SwitchPlayer(this.controller.CurrentPlayer);
            UpdateBoard();
        }

        public void StartGame()
        {
            DiscColor color = DiscColor.Black;
            if (controller.Players.First.Value.Color == DiscColor.Black) color = DiscColor.White;
            controller.CreateRoboticPlayer("Mr. Robot", color);
            controller.PickPlayer();
            pvm = new PlayerViewModel(this.controller.CurrentPlayer);
            this.DataContext = pvm;
            UpdateBoard();
        }

        public void UpdateBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    String discName = "Disc" + i.ToString() + "_" + j.ToString();
                    Ellipse disc = (Ellipse)this.FindName(discName);
                    if (controller.Board.BoardSquares[i, j].Disc == null)
                    {
                        disc.Fill = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        if (controller.Board.BoardSquares[i, j].Disc.Color == DiscColor.Black)
                        {
                            disc.Fill = new SolidColorBrush(Colors.Black);
                        }
                        if (controller.Board.BoardSquares[i, j].Disc.Color == DiscColor.White)
                        {
                            disc.Fill = new SolidColorBrush(Colors.White);
                        }
                    }

                }
            }
        }

        public void MainMenu(object sender, EventArgs e)
        {
            Switcher.pageSwitcher.Navigate(new MainMenu());
        }

        private void SwitchPlayer(Player player)
        {
            pvm.Name = player.Name;
            pvm.DiscColor = player.Color;
            pvm.Score = player.Score;
        }

        private void AllowUIUpdate()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Render, new DispatcherOperationCallback(delegate(object parameter)
            {
                frame.Continue = false;
                return null;
            }), null);
            Dispatcher.PushFrame(frame);
        }
    }
}
