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
            this.CurrentPlayer = players.ElementAt(0);
            this.players.RemoveFirst();
            this.players.AddLast(currentPlayer);
        }

        public void CreateHumanPlayer(String name, DiscColor color, Difficulty difficulty)
        {
            this.CreatePlayer(name, Player.Type.Human, color, difficulty);
        }

        public void CreateRoboticPlayer(String name, DiscColor color, Difficulty difficulty)
        {
            this.CreatePlayer(name, Player.Type.Robot, color, difficulty);
        }

        private void CreatePlayer(String name, Player.Type type, DiscColor color, Difficulty difficulty) {
            Player player = null;
            switch (type)
            {
                case Player.Type.Human:
                    player = new Human(name, color);
                    break;
                case Player.Type.Robot:
                    player = new Robot(board, name, color, difficulty);
                    break;
            }
            this.players.AddLast(player);
        }

        public bool ExecuteValidMove(int row, int col)
        {
            ArrayList flankingDirections = this.board.IsMoveValid(row, col, currentPlayer.Color);
            if (flankingDirections != null && !this.board.IsGameFinished())
            {
                this.board.MakeMove(row, col, currentPlayer.Color, flankingDirections);
                this.PickPlayer();
            }
            this.CalculateScore();
            return flankingDirections != null;
        }

        public bool ExecuteAIMove(int row, int col)
        {
            if (this.currentPlayer.GetType() == typeof(Robot) 
                && this.board.ValidMoveRemaining(this.currentPlayer.Color)
                && !this.board.IsGameFinished())
            {
                Thread.Sleep(1500);
                Robot beepBoop = (Robot)currentPlayer;
                Tuple<int, int> move = beepBoop.GetBestMove(Tuple.Create(row, col));
                ArrayList flankingDirections = this.board.IsMoveValid(move.Item1, move.Item2, beepBoop.Color);
                this.board.MakeMove(move.Item1, move.Item2, currentPlayer.Color, flankingDirections);
                this.PickPlayer();
                this.CalculateScore();

                return true;
            }
            return false;
        }

        public Player GetWinner()
        {
            return players.First.Value.Score > players.Last.Value.Score ? players.First.Value : players.Last.Value;
        }

        private void CalculateScore()
        {
            foreach (Player p in this.players)
            {
                p.Score = this.board.CountDiscs(p.Color);
            }
        }
    }
}
