using Othello;
using Othello.Controller;
using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class ConsoleView
    {
        private Game game;

        public ConsoleView(Game game) { this.game = game; }

        public void ShowGameMenu()
        {
            int choice = 0;
            Console.WriteLine("Welkom bij Othello!");
            while (choice == 0) {
                choice = MenuChoice();
                switch (choice)
                {
                    case 1:
                        StartGame();
                        break;
                    case 2:
                        Console.WriteLine("Bye!");
                        break;
                }
            }
        }

        private int MenuChoice()
        {
            bool chosen = false;
            int choice = 0;
            while (!chosen)
            {
                Console.WriteLine("Wat wil je doen?");
                Console.WriteLine("1) Een nieuw spel starten");
                Console.WriteLine("2) Spel beëindigen");
                Int32.TryParse(Console.ReadLine(), out choice);
                if(choice > 0 && choice <= 2) {
                    chosen = true;
                }
                else {
                    Console.WriteLine("Dit is geen geldige keuze!");
                }
            }
            return choice;
        }

        public void StartGame()
        {
            game.CreateHumanPlayer("Speler 1", DiscColor.Black);
            game.CreateRoboticPlayer("Speler 2", DiscColor.White);
            game.StartGame();

            while (!game.IsFinished)
            {
                string color;
                Tuple<int, int> move = Tuple.Create(0, 0);
                switch (game.CurrentPlayer.Color)
                {
                    case DiscColor.White:
                        color = "Wit";
                        break;
                    case DiscColor.Black:
                        color = "Zwart";
                        break;
                    default:
                        color = "";
                        break;
                }
                Console.WriteLine(game.Board);
                Console.WriteLine(String.Format("Het is de beurt aan: {0} ({1})", game.CurrentPlayer.Name, color));
                if (game.CurrentPlayer.GetType() == typeof(Human))
                {
                    bool success = false;
                    while (!success)
                    {
                        move = PromptUserForMove();
                        success = game.ExecuteValidMove(move.Item1, move.Item2);
                        if(!success)
                            Console.WriteLine("Deze zet is niet geldig! Probeer een andere zet te maken.");
                    }
                }
                else
                {
                    Robot r = (Robot)game.CurrentPlayer;
                    Tuple<int, int> t = r.GetMove();
                    game.Board.ExecuteValidMove(t.Item1, t.Item2);
                }
            }
        }

        public Tuple<int, int> PromptUserForMove()
        {
            int x = -1;
            Console.WriteLine("Geef de rijnummer in:");
            while(x < 0 || x > 8)
            {
                Int32.TryParse(Console.ReadLine(), out x);
            } 

            int y = -1;
            Console.WriteLine("Geef de kolomnummer in:");
            while (y < 0 || y > 8)
            {
                Int32.TryParse(Console.ReadLine(), out y);
            } 

            return Tuple.Create(x, y);
        }

        
    }
}
