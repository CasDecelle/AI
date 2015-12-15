using Othello.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Controller
{
    public class Game
    {
        private Board board;
        private LinkedList<Player> players;
        private Player currentPlayer;
        private bool isFinished;

        public Board Board { 
            get { return this.board; } 
            set { this.board = value; } 
        }

        public LinkedList<Player> Players { 
            get { return this.players;} 
            set { this.players = value;} 
        }

        public Player CurrentPlayer { 
            get { return this.currentPlayer; }
            set { this.currentPlayer = value; }
        }

        public bool IsFinished {
            get { return this.isFinished; }
            set { this.isFinished = value; }
        }

        public Game()
        {
            this.ResetGame();
        }

        public void ResetGame()
        {
            board = new Board();
            players = new LinkedList<Player>();
            isFinished = false;
        }

        public void StartGame()
        {
            if (players.Count != 0)
            {
                this.PickPlayer();
            }
        }

        public void PickPlayer()
        {
            this.currentPlayer = players.ElementAt(0);
            this.players.RemoveFirst();
            this.players.AddLast(currentPlayer);
        }

        public void CreateHumanPlayer(String name, DiscColor color)
        {
            this.CreatePlayer(name, Player.Type.Human, color);
        }

        public void CreateRoboticPlayer(String name, DiscColor color)
        {
            this.CreatePlayer(name, Player.Type.Robot, color);
        }

        private void CreatePlayer(String name, Player.Type type, DiscColor color) {
            Player player = null;
            switch (type)
            {
                case Player.Type.Human:
                    player = new Human(name, color);
                    break;
                case Player.Type.Robot:
                    player = new Robot(board, name, color);
                    break;
            }
            if (this.players.Contains(player))
                player.Name = player.Name + " 2";
            this.players.AddLast(player);
        }

        public bool ExecuteValidMove(int row, int col)
        {
            
            ArrayList flankingDirectons = this.board.IsMoveValid(row, col, currentPlayer.Color);

            if (flankingDirectons != null)
            {
                this.board.MakeMove(row, col, currentPlayer.Color, flankingDirectons);
                this.PickPlayer();
            }

            return flankingDirectons != null;
        }
    }
}
