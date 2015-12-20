using Othello.AI;
using Othello.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Othello.Controller
{
    public class Game
    {
        private Board board;
        private LinkedList<Player> players;
        private Player currentPlayer;
        private bool isFinished;
        public readonly int MAX_DEPTH = 3;

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
            ArrayList flankingDirections = this.board.IsMoveValid(row, col, currentPlayer.Color);
            if (flankingDirections != null)
            {
                this.board.MakeMove(row, col, currentPlayer.Color, flankingDirections);
                this.PickPlayer();
                // AI move
                if (this.currentPlayer.GetType() == typeof(Robot))
                {
                    Robot beepBoop = (Robot)currentPlayer;
                    Tuple<int, int> move = beepBoop.GetMove();
                  /*  StateSpace stateSpace = new StateSpace(this.board, this.MAX_DEPTH, this.currentPlayer);
                    Tuple<int,int> move = stateSpace.GetBestMove();*/
                    flankingDirections = this.board.IsMoveValid(move.Item1, move.Item2, beepBoop.Color);
                    // Make AI wait a second so move is easier to see
                    //TODO listener maken om gui te updaten en hier notify
                    //Thread.Sleep(1000);
                    this.board.MakeMove(move.Item1, move.Item2, currentPlayer.Color, flankingDirections);
                    this.PickPlayer();
                }
            }
            this.UpdateScore();
            return flankingDirections != null;
        }

        private void UpdateScore()
        {
            foreach (Player p in this.players)
            {
                p.Score = this.board.CountDiscs(p.Color);
            }
        }
    }
}
