namespace Minesweeper.Core
{
    public class Board
    {
        /// <summary>
        /// Here you have the Size, Columns, Rows, Cells, & the bomb count
        /// </summary>
        public int Size { get; }
        public int cols;
        public int rows;
        private Cell[,] cells;
        private int bombCount;
        
        /// <summary>
        /// Here this will have the size, bombcount & cells for the Board
        /// </summary>
        /// <param name="size"></param>
        /// <param name="seed"></param>
        public Board(int size, int seed)
        {
            Size = size;
            bombCount = size * size / 6;
            cells = new Cell[size, size];

            Init();
            PlaceBombs(seed);
            CalculateAdjacency();
        }
        /// <summary>
        /// This should help the size of the maze to show up
        /// </summary>
        private void Init()
        {
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)

                        cells[r, c] = new Cell();
        }
        /// <summary>
        /// This will place the bombs on the board
        /// </summary>
        /// <param name="seed"></param>
        private void PlaceBombs (int seed)
        {
            var rand = new Random(seed);
            int placed = 0;

            while (placed < bombCount) 
            {
                int r = rand.Next(Size);
                int c = rand.Next(Size);

                if (!cells[r, c].Isbomb)
                {
                    cells[r,c].Isbomb = true;
                    placed++;
                }
            }
        }
        /// <summary>
        /// This will help with the placement of the bombs and points you will get.
        /// </summary>
        private void CalculateAdjacency()
        {
            for (int r = 0; r < Size;r++)
                for (int c = 0;c < Size;c++)
                {
                    if (cells[r, c].Isbomb) continue;

                    int count = 0;
                    foreach (var (nr, nc) in Neighbors(r, c))
                        if (cells[nr,nc].Isbomb) count++;

                    cells[r, c].AdjacentBombs = count;
                }
        }
        /// <summary>
        /// This should reveal what block the player has chosen
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <exception cref="Exception"></exception>
        public void Reveal(int r, int c)
        {
            if (!InBounds(r, c)) return;

            var cell = cells[r, c];
            if (cell.IsFlagged || cell.IsRevealed) return;

            if (cell.Isbomb)
                throw new Exception("BOOM");

            FloodReveal(r, c);
        }
        /// <summary>
        /// This should keep you inbounds and all the bombs inbound as well
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool InBounds(int row, int col)
        {
            return row >= 0 && row < rows && col >= 0 && col < cols;
        }
        /// <summary>
        /// This should allow you to flag things
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        public void ToggleFlag(int r, int c)
        {
            if (!InBounds(r, c))  return;

            var cell = cells[r, c];
            if(!cell.IsRevealed)
                cell.IsFlagged = !cell.IsFlagged;
        }
        /// <summary>
        /// This will show what is in the maze or what has been shown in the maze
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        private void FloodReveal(int r, int c)
        {
            var stack = new Stack<(int, int)>();
            stack.Push((r, c));

            while (stack.Count > 0)
            {
                var (cr, cc) = stack.Pop();
                var cell = cells[cr, cc];

                if (cell.IsRevealed || cell.IsFlagged) continue;

                cell.IsRevealed = true;
                if (cell.AdjacentBombs == 0)
                {
                    foreach (var (nr, nc) in Neighbors(cr, cc))
                        stack.Push((nr, nc));
                }
            }
        }

        public bool IsWin()
        {
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                {
                    var cell = cells[r, c];
                    if (!cell.Isbomb && !cell.IsRevealed)
                        return false;
                }
            return false;
        }
        /// <summary>
        /// this should reveal what is on the maze
        /// </summary>
        public void RevealAll()
        {
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    var cell = cells[r, c];
                    if (!cell.IsRevealed)
                        Console.Write(cell.IsFlagged ? "f" : "#");
                    else if (cell.Isbomb)
                        Console.Write("b");
                    else if (cell.AdjacentBombs == 0)
                        Console.Write(".");
                    else
                        Console.Write(cell.AdjacentBombs + "");
                }
                Console.WriteLine();
            }
        }

        private IEnumerable<(int,int)>Neighbors(int r, int c)
        {
            for (int dr = -1; dr <= 1; dr++)
                for (int dc = -1; dc <= 1; dc++)
                {
                    if(dr == 0 && dc == 0) continue;
                    int nr = r + dr;
                    int nc = c + dc;

                    if (InBounds(nr, nc))
                        yield return (nr, nc);
                }
        }
        /// <summary>
        /// This should show the stuff on the board
        /// </summary>
        public void Print()
        {
            Console.Write(" ");
            for (int c = 0; c < cols; c++)
                Console.Write(c +  " ");
            Console.WriteLine();

            
            for (int r = 0; r < rows; r++)
            {
                Console.Write(r +  " ");
                for (int c = 0; c < cols; c++)
                {
                    Cell cell = cells[r, c];
                    if (!cell.IsRevealed)
                        Console.Write(". ");
                    else if (cell.Isbomb)
                        Console.Write("b ");
                    else if (cell.AdjacentBombs == 0)
                        Console.Write(".");
                    else
                        Console.Write(cell.AdjacentBombs + "");
                }
                Console.WriteLine();
            }
           
        }
    }
}
