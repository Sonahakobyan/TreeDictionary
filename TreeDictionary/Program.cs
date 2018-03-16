using System;
using System.Collections.Generic;
using TreeDictionary.TreeDictionary;

namespace TreeDictionary
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TreeDictionary<Int32, String> dict = new TreeDictionary<Int32, String>(Trees.TreeType.AVL);
            dict.Add(1, "a");
            dict.Add(new KeyValuePair<Int32, String>(2, "b"));
            dict.Remove(1);
            dict.Remove(new KeyValuePair<Int32, String>(2, ""));

            

            Console.Read();
        }
    }
}