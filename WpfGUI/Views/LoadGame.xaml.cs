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
using Othello.Utility;
using System.Collections;

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
            this.ForceUIUpdate();
            Disc opponentDisc = new Disc(this.controller.CurrentPlayer.Color); opponentDisc.InvertDisc();
            bool moveExecuted = false;
            do
            {
                moveExecuted = this.controller.ExecuteAIMove(row, col);
                UpdateBoard();
                this.ForceUIUpdate();
                if (this.FinishGame())
                    break;
            } while (!this.controller.Board.ValidMoveRemaining(opponentDisc.Color) && this.controller.Board.ValidMoveRemaining(this.controller.CurrentPlayer.Color));
            if (this.controller.CurrentPlayer.Difficulty != Difficulty.NONE)
                this.controller.PickPlayer();
            /*if (controller.ExecuteAIMove(row, col))*/
            this.SwitchPlayer(this.controller.CurrentPlayer);
            UpdateBoard();
        }

        public void StartGame()
        {
            controller.PickPlayer();
            pvm = new PlayerViewModel(this.controller.CurrentPlayer);
            this.DataContext = pvm;
            UpdateBoard();
        }

        public bool FinishGame()
        {
            bool finished = this.controller.Board.IsGameFinished();
            if (finished)
            {
                Player winner = this.controller.GetWinner();
                pvm = new PlayerViewModel(winner);
                this.DataContext = pvm;
                this.gamePanel.Visibility = System.Windows.Visibility.Collapsed;
                this.finishPanel.Visibility = System.Windows.Visibility.Visible;
                this.WriteHighscore(winner);
            }
            return finished;
        }

        public void UpdateBoard()
        {
            this.FinishGame();
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
            ArrayList possibleMoves = controller.Board.GetValidMovesForPlayer(controller.CurrentPlayer.Color);
            if (possibleMoves != null)
            {
                foreach (Tuple<int, int> possibleMove in possibleMoves)
                {
                    String discName = "Disc" + possibleMove.Item1 + "_" + possibleMove.Item2;
                    Ellipse disc = (Ellipse)this.FindName(discName);
                    disc.Fill = new SolidColorBrush(Colors.DarkGreen);
                }
            }
        }

        public void NavigateMainMenu(object sender, EventArgs e)
        {
            Switcher.pageSwitcher.Navigate(new MainMenu());
        }

        private void SwitchPlayer(Player player)
        {
            pvm.Name = player.Name;
            pvm.DiscColor = player.Color;
            pvm.Score = player.Score;
        }

        private void ForceUIUpdate()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Render, new DispatcherOperationCallback(delegate(object parameter)
            {
                frame.Continue = false;
                return null;
            }), null);
            Dispatcher.PushFrame(frame);
        }

        private void WriteHighscore(Player player)
        {
            HighscoresReadWriteHandler writer = new HighscoresReadWriteHandler("../../../Othello/Resources/Highscores.csv");
            writer.Write(player);
        }
    }
}
