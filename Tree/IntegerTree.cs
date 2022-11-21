namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class IntegerTree : Tree<int>, IIntegerTree
    {
        public IntegerTree(int key, params Tree<int>[] children)
            : base(key, children)
        {
        }

        public IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum)
        {
            var result = new List<List<int>>();

            var currPath = new LinkedList<int>();
            currPath.AddFirst(this.Key);
            int currSum = this.Key;
            this.Dfs(this, result, currPath, ref currSum, sum);

            return result;
        }

        private void Dfs(Tree<int> subtree, List<List<int>> result, LinkedList<int> currPath, ref int currSum, int wantedSum)
        {
            foreach (var child in subtree.Children)
            {
                currSum += child.Key;
                currPath.AddLast(child.Key);
                Dfs(child, result, currPath, ref currSum, wantedSum);
            }

            if (currSum == wantedSum)
            {
                result.Add(new List<int>(currPath));
            }

            currSum -= subtree.Key;
            currPath.RemoveLast();
        }

        public IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int sum)
        {
            throw new NotImplementedException();
        }
    }
}
