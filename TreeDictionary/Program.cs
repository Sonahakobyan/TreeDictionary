using System;
using System.Collections.Generic;
using System.Diagnostics;
using TreeDictionary.TreeDictionary;
using TreeDictionary.Trees;

namespace TreeDictionary
{
    internal class Program
    {
        /// <summary>
        /// Tests for hash-table, RB tree and AVL tree based dictionary implementations
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            TreeDictionary<Int32, Int32> RBDictionary = new TreeDictionary<Int32, Int32>(TreeType.RB);
            TreeDictionary<Int32, Int32> AVLDictionary= new TreeDictionary<Int32, Int32>(TreeType.AVL);
            Dictionary<Int32, Int32> HashTableDictionary = new Dictionary<Int32, Int32>();

            Random keyRandom = new Random(DateTime.Now.Millisecond);
            const Int32 size1 = 320;
            const Int32 size2 = 640;
            const Int32 size3 = 1280;

            List<Int32> keys1 = new List<Int32>(size1);
            List<Int32> keys2 = new List<Int32>(size2);
            List<Int32> keys3 = new List<Int32>(size3);

            
            while (keys1.Count != size1)
            {
                int r = keyRandom.Next();

                if (!keys1.Contains(r))
                {
                    keys1.Add(r);
                }
            }

            while (keys2.Count != size2)
            {
                int r = keyRandom.Next();

                if (!keys2.Contains(r))
                {
                    keys2.Add(r);
                }
            }

            while (keys3.Count != size3)
            {
                int r = keyRandom.Next();

                if (!keys3.Contains(r))
                {
                    keys3.Add(r);
                }
            }

           
            Stopwatch watch = Stopwatch.StartNew();

            // Insert
            Console.WriteLine(" Insertion: 320 element");

            // keys1

            //Calculating running time 
            watch = Stopwatch.StartNew();

            foreach (var key in keys1)
            {
                HashTableDictionary.Add(key, 0);
            }

            watch.Stop();
            Console.WriteLine($"Hash Table: {watch.ElapsedMilliseconds}");

            //Calculating running time 
            watch.Start();

            foreach (var key in keys1)
            {
                RBDictionary.Add(key, 0);
            }
       
            watch.Stop();
            Console.WriteLine($"RB: {watch.ElapsedMilliseconds}");
            
            //Calculating running time 
            watch = Stopwatch.StartNew();

            foreach (var key in keys1)
            {
                AVLDictionary.Add(key, 0);
            }

            watch.Stop();
            Console.WriteLine($"AVL: {watch.ElapsedMilliseconds}");




            // Clear dictionaries
            RBDictionary.Clear();
            AVLDictionary.Clear();
            HashTableDictionary.Clear();

            Console.WriteLine(" Insertion: 640 element");
            // keys2

            //Calculating running time 
            watch = Stopwatch.StartNew();

            for (int i = 0; i < keys2.Count; i++)
            {
                HashTableDictionary.Add(keys2[i], 0);
            }

            watch.Stop();
            Console.WriteLine($"Hash Table: {watch.ElapsedMilliseconds}");


            //Calculating running time 
            watch.Start();

            for (int i = 0; i < keys2.Count; i++)
            {
                RBDictionary.Add(keys2[i], 0);
            }

            watch.Stop();
            Console.WriteLine($"RB: {watch.ElapsedMilliseconds}");


            //Calculating running time 
            watch = Stopwatch.StartNew();

            for (int i = 0; i < keys2.Count; i++)
            {
                AVLDictionary.Add(keys2[i], 0);
            }

            watch.Stop();
            Console.WriteLine($"AVL: {watch.ElapsedMilliseconds}");

           


            // Clear dictionaries
            RBDictionary.Clear();
            AVLDictionary.Clear();
            HashTableDictionary.Clear();

            Console.WriteLine(" Insertion: 1280 element");
            // keys3

            //Calculating running time 
            watch = Stopwatch.StartNew();

            for (int i = 0; i < keys3.Count; i++)
            {
                HashTableDictionary.Add(keys3[i], 0);
            }

            watch.Stop();
            Console.WriteLine($"Hash Table: {watch.ElapsedMilliseconds}");

            //Calculating running time 
            watch.Start();

            for (int i = 0; i < keys3.Count; i++)
            {
                RBDictionary.Add(keys3[i], 0);
            }

            watch.Stop();
            Console.WriteLine($"RB: {watch.ElapsedMilliseconds}");


            //Calculating running time 
            watch = Stopwatch.StartNew();

            for (int i = 0; i < keys3.Count; i++)
            {
                AVLDictionary.Add(keys3[i], 0);
            }

            watch.Stop();
            Console.WriteLine($"AVL: {watch.ElapsedMilliseconds}");

            

            // Clear dictionaries
            RBDictionary.Clear();
            AVLDictionary.Clear();
            HashTableDictionary.Clear();

            Console.Read();
        }
    }
}