namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this.children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => this.children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this.children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string AsString()
        {
            StringBuilder sb = new StringBuilder();

            this.DfsAsString(sb, this, 0);

            return sb.ToString().Trim();
        }

        private void DfsAsString(StringBuilder sb, Tree<T> tree, int indent)
        {
            sb.Append(' ', indent);
            sb.AppendLine(tree.Key.ToString());

            foreach (var child in tree.children)
            {
                this.DfsAsString(sb, child, indent + 2);
            }
        }

        public IEnumerable<T> GetInternalKeys()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var currSubtree = queue.Dequeue();
                if (currSubtree.children.Count > 0 && currSubtree.Parent != null)
                {
                    result.Add(currSubtree.Key);
                }

                foreach (var child in currSubtree.children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public IEnumerable<T> GetLeafKeys()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var currSubtree = queue.Dequeue();
                if (currSubtree.children.Count == 0)
                {
                    result.Add(currSubtree.Key);
                }

                foreach (var child in currSubtree.children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public T GetDeepestKey()
        {
            return this.GetDeepestNode().Key;
        }

        private Tree<T> GetDeepestNode()
        {
            var result = new List<Tree<T>>();
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var currSubtree = queue.Dequeue();
                if (currSubtree.children.Count == 0)
                {
                    result.Add(currSubtree);
                }

                foreach (var child in currSubtree.children)
                {
                    queue.Enqueue(child);
                }
            }

            Tree<T> deepestNode = null;
            var maxDepth = 0;

            foreach (var leaf in result)
            {
                var depth = this.GetDepth(leaf);
                if (depth > maxDepth)
                {
                    maxDepth = depth;
                    deepestNode = leaf;
                }
            }

            return deepestNode;
        }

        private int GetDepth(Tree<T> leaf)
        {
            int depth = 0;
            var tree = leaf;

            while (tree.Parent != null)
            {
                depth++;
                tree = tree.Parent;
            }

            return depth;
        }

        public IEnumerable<T> GetLongestPath()
        {
            throw new NotImplementedException();
        }
    }
}
