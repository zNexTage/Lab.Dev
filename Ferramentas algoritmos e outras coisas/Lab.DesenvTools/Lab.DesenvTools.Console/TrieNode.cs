using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.DesenvTools.Console
{
    public class Node
    {
        public Node()
        {
            Children = new Dictionary<char, Node>();
            Count = 0;
        }

        public Dictionary<char, Node> Children
        {
            get;
            set;
        }

        public bool IsCompleteWord
        {
            get;
            set;
        }

        public int Count
        {
            get;
            set;
        }
    }


    public class TrieNode
    {
        public Node Root { get; private set; }

        public TrieNode()
        {
            Root = new Node();
        }

        public void Insert(string word)
        {
            Node current = Root;

            char[] characters = word.ToCharArray();

            foreach (var c in characters)
            {
                Node child = current.Children.GetValueOrDefault(c);

                if (child is null)
                {
                    child = new Node();
                    child.Count += 1;
                    current.Children.Add(c, child);
                } else
                {
                    child.Count += 1;
                }

                current = child;
            }

            current.IsCompleteWord = true;
        }

        public Node GetNode(string word)
        {
            Node current = Root;

            char[] characteres = word.ToCharArray();

            foreach (var c in characteres)
            {
                Node child = current.Children.GetValueOrDefault(c);

                if (child is null) return null;
                current = child;
            }

            return current;
        }
    }
}
