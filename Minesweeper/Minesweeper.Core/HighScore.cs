using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Core
{
    internal class HighScore
    {
        private static string path = "data/highscores.csv";
        /// <summary>
        /// This will show the highscore on what the player got, how many moves they make,
        /// The seconds they have, and the seed of the board
        /// </summary>
        /// <param name="size"></param>
        /// <param name="seconds"></param>
        /// <param name="moves"></param>
        /// <param name="Seed"></param>
        public static void Save(int size, int seconds, int moves, int Seed)
        {
            Directory.CreateDirectory("data");

            if (!File.Exists(path))
                File.WriteAllText(path, "size,seconds,moves,seed,timestamp\n");

            string line = $"{size},{seconds},{moves}{Seed},{DateTime.UtcNow:o}\n";
            File.AppendAllText(path, line);
            
        }
    }
}
