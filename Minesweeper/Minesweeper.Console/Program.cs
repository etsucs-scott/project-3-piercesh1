using Minesweeper.Core;
using System.Collections.Generic;
using System.Threading.Channels;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            ///
            /// Here is where you will choose what size you want between these options or just quit the game
            ///
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. 8x8");
                Console.WriteLine("2. 12x12");
                Console.WriteLine("3. 16x16");
                Console.WriteLine("Q. Quit");

                var choice = Console.ReadLine()?.ToLower();
                ///
                ///Here this will help you with what choice you picked
                ///
                if (choice == "q") return;
                int size = choice switch
                {
                    "1" => 8,
                    "2" => 12,
                    "3" => 16,
                    _ => 0
                };
                if (size == 0) continue;
                ///
                ///Here you will enter the seed for what you want
                ///
                Console.Write("Enter seed: ");
                string seedInput = Console.ReadLine();

                ///
                ///This should make your seed and size for the game random
                ///
                int seed = string.IsNullOrWhiteSpace(seedInput) ? new Random().Next()
                    : int.Parse(seedInput);
                var game = new Game(size, seed);
                game.Run();
            }
        }
    }
}
