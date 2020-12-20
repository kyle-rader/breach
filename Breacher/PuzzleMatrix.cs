using System;
using System.Collections.Generic;
using System.Linq;

namespace Breacher
{
    public class PuzzleMatrix
    {
        private readonly int[,] input;
        private readonly int N;
        private int extraBuffer;
        private int bufferSize;

        public PuzzleMatrix(int[,] input)
        {
            this.input = input;
            N = input.GetLength(0);
        }

        public bool Check(int[] chain, int bufferSize, out StackSet<(int row, int col)> path)
        {
            this.bufferSize = bufferSize;
            this.extraBuffer = bufferSize - chain.Length;

            path = new StackSet<(int row, int col)>();
            int row = 0; // Puzzle requires you select from the first row (= 0)
            int col = -1; // The col must selected first.
            int i_chain = 0;

            return CheckRow(chain, path, row, col, i_chain);
        }

        private bool CheckRow(int[] chain, StackSet<(int row, int col)> path, int row, int col, int i_chain)
        {
            foreach (var spot in Row(row))
            {
                var match = spot.Value == chain[i_chain];
                var canBurnASpot = path.Count < extraBuffer && i_chain == 0;
                var coords = (row, spot.Index);

                if (!path.Contains(coords) && (canBurnASpot || match))
                {
                    path.Push((row, spot.Index));
                    var i_chain_next = i_chain + (match ? 1 : 0);
                    var pathFitsInBuffer = path.Count <= bufferSize;

                    if (i_chain_next == chain.Length && pathFitsInBuffer)
                    { // Just found the last match and we are done!
                        return true;
                    }
                    else if (!pathFitsInBuffer)
                    { // Not done matching the path, but we outa buffer
                        path.Pop();
                        return false;
                    }
                    else if (CheckCol(chain, path, row, spot.Index, i_chain_next))
                    { // We can check the column, and there is a path following it
                        return true;
                    }
                    else
                    { // Checked that column but there is no path.
                        path.Pop();
                    }
                }
            }
            // There were no matches in this row and we don't have any extra buffer space.
            return false;
        }

        private bool CheckCol(int[] chain, StackSet<(int row, int col)> path, int row, int col, int i_chain)
        {
            foreach (var spot in Col(col))
            {
                var match = spot.Value == chain[i_chain];
                var canBurnASpot = path.Count < extraBuffer && i_chain == 0;
                var coords = (spot.Index, col);

                // Can only use each coord once.
                if (!path.Contains(coords) && (canBurnASpot || match))
                {
                    path.Push((spot.Index, col));
                    var i_chain_next = i_chain + (match ? 1 : 0);
                    var pathFitsInBuffer = path.Count <= bufferSize;

                    if (i_chain_next == chain.Length && pathFitsInBuffer)
                    { // Just found the last match and we are done!
                        return true;
                    }
                    else if (!pathFitsInBuffer)
                    { // Not done matching the path, but we outa buffer
                        path.Pop();
                        return false;
                    }
                    else if (CheckRow(chain, path, spot.Index, col, i_chain_next))
                    { // We can check the row, and there is a path following it
                        return true;
                    }
                    else
                    { // Checked that column but there is no path.
                        path.Pop();
                    }
                }
            }
            // There were no matches in this row and we don't have any extra buffer space.
            return false;
        }

        public void Print(IEnumerable<(int row, int col)> path)
        {
            int[,] steps = new int[N, N];
            int step = 1;
            foreach (var coord in path)
            {
                steps[coord.row, coord.col] = step++;
            }

            ConsoleColor foreground = Console.ForegroundColor;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (steps[i,j] > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = foreground;
                    }

                    Console.Write($"{input[i, j]:X}");
                    if (steps[i, j] > 0)
                    {
                        ConsoleHelper.WriteColor($"-{steps[i, j]} ", ConsoleColor.DarkYellow);
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }
                Console.Write("\n\n");
            }
            // Reset foreground
            Console.ForegroundColor = foreground;
        }

        // Yield each element in this row (iterate over the columns)
        private IEnumerable<(int Index, int Value)> Row(int row)
        {
            for (int i = 0; i < N; i++)
            {
                yield return (i, input[row, i]);
            }
        }

        private IEnumerable<(int Index, int Value)> Col(int col)
        {
            for (int i = 0; i < N; i++)
            {
                yield return (i, input[i, col]);
            }
        }
    }
}
