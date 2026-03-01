/*
    Implementação árvore binária 
*/
namespace Lab.DesenvTools.Console.Tree
{

    internal class Node<T>
    {
        public int Key { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public T Data { get; set; }
    }
    internal class Tree<T>
    {
        public Node<T> Root { get; set; }

        public void Insert(int key, T data)
        {
            Root = InsertNode(Root, key, data);
        }

        public Node<T>? Search(int key)
        {
            return SearchNode(Root, key);
        }

        public void Remove(int key)
        {
            RemoveNode(Root, key);
        }

        public void PreOrder()
        {
            PreOrderPrint(Root);
        }

        public void Order()
        {
            OrderPrint(Root);
        }

        public void PostOrder()
        {
            PostOrderPrint(Root);
        }

        private void PreOrderPrint(Node<T> node)
        {
            PrintNode(node);

            if (node.Left is not null)
            {
                PreOrderPrint(node.Left);
            }

            if (node.Right is not null)
            {
                PreOrderPrint(node.Right);
            }
        }

        private void OrderPrint(Node<T> node)
        {
            if (node is null) return;

            OrderPrint(node.Left);

            PrintNode(node);

            OrderPrint(node.Right);
        }

        private void PostOrderPrint(Node<T> node)
        {
            if (node is null) return;

            PostOrderPrint(node.Left);

            PostOrderPrint(node.Right);

            PrintNode(node);
        }

        private void PrintNode(Node<T> node)
        {
            System.Console.WriteLine($"{node.Key} - {node.Data}");
        }


        private Node<T>? SearchNode(Node<T> node, int key)
        {
            if (node is null || node.Key == key)
            {
                return node;
            }

            if (key > node.Key)
            {
                return SearchNode(node.Right, key);
            }
            else
            {
                return SearchNode(node.Left, key);
            }
        }

        private Node<T> InsertNode(Node<T> node, int key, T data)
        {
            if (node is null)
            {
                return new Node<T>() { Data = data, Key = key };
            }

            if (key > node.Key)
            {
                node.Right = InsertNode(node.Right, key, data);
            }
            else
            {
                node.Left = InsertNode(node.Left, key, data);
            }

            return node;
        }

        private Node<T>? RemoveNode(Node<T> node, int key)
        {
            if (node is null) return null;

            if (key < node.Key)
            {
                node.Left = RemoveNode(node.Left, key);
                return node;
            }

            if (key > node.Key)
            {
                node.Right = RemoveNode(node.Right, key);
                return node;
            }

            // Caso 1: sem filhos
            if (node.Left == null && node.Right == null)
            {
                return null;
            }

            // Caso 2: um filho
            if (node.Left == null)
                return node.Right;

            if (node.Right == null)
                return node.Left;

            var min = FindMin(node.Right);
            node.Data = min.Data;
            node.Key = min.Key;
            node.Right = RemoveNode(node.Right, min.Key);

            return node;
        }

        private Node<T> FindMin(Node<T> node)
        {
            while (node.Left is not null)
            {
                node = node.Left;
            }

            return node;
        }
    }

    public class TreeExample()
    {
        record User
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        /*
         * Monta uma árvore binário de usuários
         */
        public void Run()
        {
            // Usuários
            var users = new List<User>
            {
                new () { Id = 10, Name = "Christian" },
                new () { Id = 1, Name = "Maria" },
                new () { Id = 17, Name = "Felipe" },
                new () { Id = 5, Name = "João" },
                new () { Id = 13, Name = "Eduardo" },
                new () { Id = 8, Name = "Ana" },
                new () { Id = 19, Name = "Ricardo" },
                new () { Id = 4, Name = "Carlos" },
                new () { Id = 2, Name = "Fernanda" },
                new () { Id = 20, Name = "Vanessa" },
                new () { Id = 16, Name = "Beatriz" },
                new () { Id = 3, Name = "Lucas" },
                new () { Id = 14, Name = "Larissa" },
                new () { Id = 9, Name = "Juliana" },
                new () { Id = 7, Name = "Rafael" },
                new () { Id = 18, Name = "Aline" },
                new () { Id = 6, Name = "Patrícia" },
                new () { Id = 11, Name = "Bruno" },
                new (){ Id = 12, Name = "Camila" },
                new () { Id = 15, Name = "Gustavo" },
            };

            var tree = new Tree<User>();

            users.ForEach(u =>
            {
                tree.Insert(u.Id, u);
            });

            var id = 6;
            var node = tree.Search(id);

            if (node is null)
            {
                System.Console.WriteLine($"Usuário com ID {id} não foi localizado.");
            }
            else
            {
                System.Console.WriteLine(node.Data.Name);
            }

            //tree.Remove(id);

            tree.PostOrder();
        }
    }
}
