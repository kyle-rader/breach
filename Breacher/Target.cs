namespace Breacher
{
    public struct Target
    {
        public Target(int[] values, int weight)
        {
            this.values = values;
            this.weight = weight;
        }

        public int[] values;
        public int weight;
    }
}
