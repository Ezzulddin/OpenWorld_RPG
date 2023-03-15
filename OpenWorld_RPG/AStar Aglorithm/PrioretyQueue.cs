using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.AStar_Aglorithm
{
    public class PrioretyQueue<T>
    {
        private List<Tuple<T, double>> _elements = new List<Tuple<T, double>>();

        public int Count => _elements.Count;

        public void Enqueue(T item, double priority) => _elements.Add(Tuple.Create(item, priority));
        public T Dequeue()
        {
            int bestIndex = 0;

            for (int i = 0; i < _elements.Count; i++)
            {
                if (_elements[i].Item2 < _elements[bestIndex].Item2)
                {
                    bestIndex = i;
                }
            }

            T bestItem = _elements[bestIndex].Item1;
            _elements.RemoveAt(bestIndex);
            return bestItem;
        }
    }
}
