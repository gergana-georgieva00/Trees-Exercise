namespace Demo
{
    using System;
    using System.Collections.Generic;
    using Tree;

    class Program
    {
        static void Main(string[] args)
        {
            var treeFactory = new Tree.IntegerTreeFactory();
            var input = new List<string>();
            while (true)
            {
                var currentInput = Console.ReadLine();
                if (currentInput == "end")
                {
                    break;
                }

                input.Add(currentInput);
            }
            
            treeFactory.CreateTreeFromStrings(input.ToArray());
        }
    }
}
