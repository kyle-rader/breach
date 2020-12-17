using System;

namespace Breacher
{
    public class PuzzleMatrix
    {
        private readonly int bufferSize;
        private readonly int[,] input;
        private readonly int N;

        public PuzzleMatrix(int bufferSize, int[,] input)
        {
            this.bufferSize = bufferSize;
            this.input = input;
            N = input.GetLength(0);
        }

        public bool Check(Attempt attempt)
        {
            return false;
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
