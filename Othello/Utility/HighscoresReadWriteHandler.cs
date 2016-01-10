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
    public class HighscoresReadWriteHandler
    {
        private string separator;
        private string file;
        private string filePath;

        public HighscoresReadWriteHandler(string filePath)
        {
            this.separator = ";";
            this.filePath = filePath;
        }

        public void Write(Player player)
        {
            List<Player> players = this.Read();
            foreach (Player p in players)
            {
                if (player.Name.Equals(p.Name) && player.Score == p.Score) return;
            }
            File.AppendAllText(filePath, player.ToString());
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
            int firstSemicolonIndex = row.IndexOf(";");
            int lastSemicolonIndex = row.LastIndexOf(";");
            player.Name = row.Substring(0,  firstSemicolonIndex);
            int score = 0;
            Int32.TryParse(row.Substring(firstSemicolonIndex + 1, lastSemicolonIndex - firstSemicolonIndex - 1), out score);
            player.Score = score;
            player.Difficulty = (Difficulty) Enum.Parse(typeof(Difficulty), row.Substring(lastSemicolonIndex + 1));
            return player;
        }

    }
}
