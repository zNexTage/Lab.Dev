using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.DesenvTools.Console
{
    public class RunningMedian
    {
        private void AddNumber(
        int value,
        PriorityQueue<int, int> lower,
        PriorityQueue<int, int> upper)
        {
            // Se lower estiver vazia ou o valor for menor/igual ao maior da metade menor,
            // ele entra em lower
            if (lower.Count == 0 || value <= lower.Peek())
            {
                lower.Enqueue(value, -value); // prioridade negativa => maior valor sai primeiro
            }
            else
            {
                upper.Enqueue(value, value);  // min-heap normal
            }
        }

        private void Rebalance(
            PriorityQueue<int, int> lower,
            PriorityQueue<int, int> upper)
        {
            // Se uma heap ficar com mais de 1 elemento de diferença, move o topo
            if (lower.Count > upper.Count + 1)
            {
                int moved = lower.Dequeue();
                upper.Enqueue(moved, moved);
            }
            else if (upper.Count > lower.Count + 1)
            {
                int moved = upper.Dequeue();
                lower.Enqueue(moved, -moved);
            }
        }

        private double GetMedian(
            PriorityQueue<int, int> lower,
            PriorityQueue<int, int> upper)
        {
            if (lower.Count == upper.Count)
            {
                return (lower.Peek() + upper.Peek()) / 2.0;
            }

            if (lower.Count > upper.Count)
            {
                return lower.Peek();
            }

            return upper.Peek();
        }

        public List<double> Median(List<int> a)
        {
            var result = new List<double>();

            // Metade menor (simulando max-heap com prioridade negativa)
            var lower = new PriorityQueue<int, int>();

            // Metade maior (min-heap normal)
            var upper = new PriorityQueue<int, int>();

            foreach (int value in a)
            {
                AddNumber(value, lower, upper);
                Rebalance(lower, upper);
                result.Add(GetMedian(lower, upper));
            }

            return result;
        }
    }
}
