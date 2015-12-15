using Othello.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;


namespace Othello.Utility
{
    public class HighscoresHandler
    {
        private string separator;
        private string file;
        private string filePath;

        public HighscoresHandler(string filePath)
        {
            this.separator = ";";
            this.filePath = filePath;
        }

        public void Write(Player player)
        {
            List<Player> players = this.Read();
            players.Add(player);
            StringBuilder highscores = new StringBuilder();
            foreach (Player p in players)
            {
                highscores.Append(p.ToString());
            }
            File.WriteAllText(filePath, highscores.ToString());
        }

        public List<Player> Read()
        {
            this.file = File.ReadAllText(filePath);
            List<Player> players = new List<Player>();
            this.file = this.file.Replace("\r", "");
            while (!String.IsNullOrEmpty(this.file))
            {
                Player player = this.ReadRow();
                players.Add(player);
            }
            return players;
        }

        private Player ReadRow()
        {
            Player player = new Player();
            int index = this.file.IndexOf("\n");
            string row = this.file.Substring(0, index);
            this.file = this.file.Substring(index + 1);
            index = row.IndexOf(";");
            player.Name = row.Substring(0, index);
            int score = 0;
            Int32.TryParse(row.Substring(index + 1), out score);
            player.Score = score;
            return player;
        }

    }
}
