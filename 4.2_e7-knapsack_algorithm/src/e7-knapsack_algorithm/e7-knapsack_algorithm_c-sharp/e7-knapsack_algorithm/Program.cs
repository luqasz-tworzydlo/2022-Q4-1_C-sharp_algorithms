﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Globalization;
using System.Threading;

// e7_knapsack_algorithm

namespace e7_knapsack_algorithm
{
    /// 0-1 Knapsack in C#
    /// Recursive version
    public class Program
    {
        private static void Main(string[] args)
        {
            Action<object> write = Console.Write;
            // var stopwatch = new Stopwatch();
            // stopwatch.Start();

            GAP_B();

            TASK_NO_1();

            GAP_B();

            //TASK_NO_2();

            GAP_B();

            // stopwatch.Stop();

            // Console.Write(string.Format("\n\nDuration: {0}\nPress any key to exit a program...\n", stopwatch.Elapsed.ToString()));
            Console.ReadKey();
        }

        public static void GAP_B()
        {
            Console.WriteLine("\n/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///\n");
        }

        public static void TASK_NO_1()
        {
            Action<object> write = Console.Write;

            // Task no 1 [data]
            const int n = 4; // n => amount of objects
            const int W = 5; // W => max weight
            var items = new List<Item>();

            // var rand = new Random();

            for (var i = 0; i < n; i++)
            {
                // items.Add(new Item { WEIGHT = rand.Next(1, 10), VALUE = rand.Next(1, 100) });
                items.Add(new Item { WEIGHT = 2, VALUE = 3 });
                items.Add(new Item { WEIGHT = 3, VALUE = 4 });
                items.Add(new Item { WEIGHT = 4, VALUE = 5 });
                items.Add(new Item { WEIGHT = 5, VALUE = 6 });
            }

            Knapsack.Init(items, W);
            Knapsack.Run();

            //Knapsack.PrintPicksMatrix(write);
            Knapsack.Print(write, true);
        }
        public static void TASK_NO_2()
        {
            Action<object> write = Console.Write;

            // Task no 2 [data]
            const int n = 5; // n => amount of objects
            const int W = 7; // W => max weight
            var items = new List<Item>();

            for (var i = 0; i < n; i++)
            {
                items.Add(new Item { WEIGHT = 2, VALUE = 3 });
                items.Add(new Item { WEIGHT = 3, VALUE = 4 });
                items.Add(new Item { WEIGHT = 4, VALUE = 5 });
                items.Add(new Item { WEIGHT = 5, VALUE = 6 });
                items.Add(new Item { WEIGHT = 6, VALUE = 7 });
            }

            Knapsack.Init(items, W);
            Knapsack.Run();

            Knapsack.PrintPicksMatrix(write);
            //Knapsack.Print(write, true);
        }
    }

    static class Knapsack
    {
        static int[][] MATRIX { get; set; } // matrix
        static int[][] PICKS { get; set; } // picks
        static Item[] ITEMS { get; set; } // items
        public static int MaxValue { get; private set; }
        static int W { get; set; } // max weight

        public static void Init(List<Item> items, int maxWeight)
        {
            ITEMS = items.ToArray();
            W = maxWeight;

            var n = ITEMS.Length;
            MATRIX = new int[n][];
            PICKS = new int[n][];
            for (var i = 0; i < MATRIX.Length; i++) { MATRIX[i] = new int[W + 1]; }
            for (var i = 0; i < PICKS.Length; i++) { PICKS[i] = new int[W + 1]; }
        }

        public static void Run()
        { MaxValue = Recursive(ITEMS.Length - 1, W, 1); }

        static int Recursive(int i, int w, int depth)
        {
            var take = 0;
            if (MATRIX[i][w] != 0) { return MATRIX[i][w]; }

            if (i == 0)
            {
                if (ITEMS[i].WEIGHT <= w)
                {
                    PICKS[i][w] = 1;
                    MATRIX[i][w] = ITEMS[0].VALUE;
                    return ITEMS[i].VALUE;
                }

                PICKS[i][w] = -1;
                MATRIX[i][w] = 0;
                return 0;
            }

            if (ITEMS[i].WEIGHT <= w)
            {
                take = ITEMS[i].VALUE + Recursive(i - 1, w - ITEMS[i].WEIGHT, depth + 1);
            }

            var dontTake = Recursive(i - 1, w, depth + 1);

            MATRIX[i][w] = Max(take, dontTake);
            
            if (take > dontTake) { PICKS[i][w] = 1; }
            else { PICKS[i][w] = -1; }

            return MATRIX[i][w];
        }

        public static void Print(Action<object> write, bool full)
        {
            var list = new List<Item>();
            list.AddRange(ITEMS);
            var w = W;
            var i = list.Count - 1;

            // display total amount of items [objects]
            write(string.Format("=> Total Amount of Items: = {0}\n\n", list.Count));
            // display every ID with its value and weight
            if (full) { list.ForEach(a => write(string.Format("{0}\n", a))); }

            // display max weight & max value
            write(string.Format("\n=> Max weight = {0}\n", W));
            write(string.Format("=> Max value = {0}\n", MaxValue));
            
            // display all picks
            write("\n=> Picks were:\n");
                        var valueSum = 0;
            var weightSum = 0;
            while (i >= 0 && w >= 0)
            {
                if (PICKS[i][w] == 1)
                {
                    valueSum += list[i].VALUE;
                    weightSum += list[i].WEIGHT;
                    if (full) { write(string.Format("{0}\n", list[i])); }
                    w -= list[i].WEIGHT;
                }

                i--;
            }

            // display value sum & weight sum
            write(string.Format("\n=> Value sum: {0}\n=> Weight sum: {1}\n",
                valueSum, weightSum));
        }

        public static void PrintPicksMatrix(Action<object> write)
        {
            write("\n\n");
            foreach (var i in PICKS)
            {
                foreach (var j in i)
                {
                    var s = j.ToString();
                    var _ = s.Length > 1 ? " " : "  ";
                    write(string.Concat(s, _));
                }
                write("\n");
            }
        }

        static int Max(int a, int b)
        { return a > b ? a : b; }
    }

    class Item
    {
        private static int _COUNTER; // counter for the object
        public int ID { get; private set; } // id of the object
        public int VALUE { get; set; } // value of the object
        public int WEIGHT { get; set; } // weight of the object
        public Item()
        { ID = ++_COUNTER; } // counter in use

        public override string ToString()
        { return string.Format("ID: {0}  Value: {1}  Weight: {2}", ID, VALUE, WEIGHT); } // output [!]
    }
}