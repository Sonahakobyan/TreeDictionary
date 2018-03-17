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
            dict.Add(3, "c");
            dict.Add(new KeyValuePair<Int32, String>(7, "g"));
            dict.Add(4, "d");
            dict.Add(new KeyValuePair<Int32, String>(6, "f"));
            dict.Add(5, "e");
            dict.Add(new KeyValuePair<Int32, String>(10, "i"));
            dict.Add(8, "h");
            dict.Add(new KeyValuePair<Int32, String>(9, "j"));
            //dict.Remove(1);
            //dict.Remove(new KeyValuePair<Int32, String>(2, ""));

            Console.Read();
        }
    }
}