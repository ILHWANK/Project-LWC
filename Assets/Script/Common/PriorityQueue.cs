using System.Collections.Generic;

namespace script.Common
{
    public class PriorityQueue<T>
    {
        private List<KeyValuePair<T, int>> elements = new List<KeyValuePair<T, int>>();

        public int Count => elements.Count;

        public void Enqueue(T item, int priority)
        {
            elements.Add(new KeyValuePair<T, int>(item, priority));
        }

        public T Dequeue()
        {
            var highestPriorityElement = elements[0];
            var highestPriorityIndex = 0;

            for (var i = 1; i < elements.Count; i++)
            {
                if (elements[i].Value <= highestPriorityElement.Value) 
                    continue;
                
                highestPriorityElement = elements[i];
                highestPriorityIndex = i;
            }

            elements.RemoveAt(highestPriorityIndex);
            return highestPriorityElement.Key;
        }
    }
}