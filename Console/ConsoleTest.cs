using Othello.Controller;
using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class ConsoleTest
    {
        static void Main(string[] args)
        {
            Game othello = new Game();
            ConsoleView view = new ConsoleView(othello);
            view.ShowGameMenu();
        }
    }
}
