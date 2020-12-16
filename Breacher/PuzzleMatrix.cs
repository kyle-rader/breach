using System;

namespace Breacher
{
    public class PuzzleMatrix
    {
        public struct Spot
        {
            public int Value;
            public bool Taken;
        }

        private readonly int bufferSize;
        private readonly int[,] input;
        private readonly int N;

        private Spot[,] board;

        public PuzzleMatrix(int bufferSize, int[,] input)
        {
            this.bufferSize = bufferSize;
            this.input = input;
            N = input.GetLength(0);
            board = new Spot[N, N];

            // Initialize the puzzle board
            ForAll((i, j) =>
            {
                board[i, j] = new Spot()
                {
                    Value = input[i, j],
                    Taken = false,
                };
            });
        }

        public bool Check(Attempt attempt)
        {
            ClearBoard(); // Clear previous attempts
            return false;
        }

        private void ClearBoard()
        {
            ForAll((i, j) => board[i, j].Taken = false);
        }

        private void ForAll(Action<int, int> action)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    action(i, j);
                }
            }
        }

    }
}
