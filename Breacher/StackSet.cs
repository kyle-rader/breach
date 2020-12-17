using System.Collections.Generic;

namespace Breacher
{
    public class StackSet<T>
    {
        private const int InitialCapcity = 12;
        private Stack<T> stack = new Stack<T>(capacity: InitialCapcity);
        private HashSet<T> set = new HashSet<T>(capacity: InitialCapcity);

        public int Count
        {
            get { return stack.Count; }
        }

        public int Push(T v)
        {
            if (!set.Add(v)) throw new AlreadyInStackSetException($"'{v}' is already in the stack. This is a StackSet and does not allow duplicates. Use the Contains method to efficiently check for existence.");
            stack.Push(v);
            return stack.Count;
        }

        public T Pop()
        {
            var item = stack.Pop();
            set.Remove(item);
            return item;
        }

        public bool Contains(T item)
        {
            return set.Contains(item);
        }
    }
}
