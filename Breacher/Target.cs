namespace Breacher
{
    public class Target
    {
        public Target(int[] values, int weight)
        {
            this.values = values;
            this.weight = weight;
        }

        public int[] values;
        public int weight;

        public override string ToString()
        {
            return $"[{string.Join(',', values)}]";
        }
    }
}
