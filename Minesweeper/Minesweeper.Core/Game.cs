using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Core
{
    public class Game
    {
        private Board board;
        private int moves;
        private Stopwatch timer;
        private int seed;
        /// <summary>
        /// This part of the code here should put in the size, the seed, a stopwatch, and allow you to make moves.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="seed"></param>
        public Game (int size, int seed)
        {
            this.seed = seed;
            board = new Board(size, seed);
            moves = 0;
            timer = Stopwatch.StartNew();
            
        }
        public void Run()
        {
            while(true)
            {
                Console.Clear();
                board.Print();
                ///
                ///When the timer ends the game is over and you win
                ///
                if(board.IsWin())
                {
                    timer.Stop();
                    Console.WriteLine("You Win!");
                }
                HighScore.Save(board.Size, (int)timer.Elapsed.TotalSeconds, moves, seed);
                return;
                ///This should show you what options to pick
                Console.Write("Enter move (r/f row col, q): ");
                var input = Console.ReadLine()?.Split(' ');

                if (input == null || input.Length == 0) continue;
                if (input[0] == "q") return;
                if (input.Length != 3) continue;
                if (!int.TryParse(input[1], out int r) || int.TryParse(input[2], out int c)) continue;
                ///
                /// Here this will do many things such as show all the blocks once the player hits a bomb
                /// It will show a game over
                /// it will clear the board
                /// and will allow you to make moves
                ///
                try
                {
                    if (input[0] == "r")
                        board.Reveal(r, c);
                    else if (input[0] == "f")
                        board.ToggleFlag(r, c);
                    moves++;
                }
                catch
                {
                    board.RevealAll();
                    Console.Clear();
                    board.Print();
                    Console.WriteLine("Game Over!");
                    return;
                }
            }
        }
    }
}
