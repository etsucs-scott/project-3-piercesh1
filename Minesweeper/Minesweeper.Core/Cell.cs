using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Core
{
    public class Cell
    {
        /// <summary>
        /// Here is the Isbomb, IsRevealed, IsFlagged, AdjacentBombs, and cells to help with the code
        /// </summary>
        public bool Isbomb {  get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public int AdjacentBombs { get; set; }

        public Cell[,] Cells;

    }
}
