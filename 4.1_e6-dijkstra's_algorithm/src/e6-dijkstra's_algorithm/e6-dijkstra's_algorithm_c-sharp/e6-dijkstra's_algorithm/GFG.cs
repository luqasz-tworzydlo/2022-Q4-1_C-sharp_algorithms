﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e6_dijkstra_s_algorithm
{
    public class GFG
    {
        // https://www.geeksforgeeks.org/csharp-program-for-dijkstras-shortest-path-algorithm-greedy-algo-7/
        // A utility function to find the
        // vertex with minimum distance
        // value, from the set of vertices
        // not yet included in shortest
        // path tree
        static int V = 5;
        int minDistance(int[] dist,
                        bool[] sptSet)
        {
            // Initialize min value
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }

        // A utility function to print
        // the constructed distance array
        void printSolution(int[] dist, int n)
        {
            Console.Write("Vertex     Distance "
                          + "from Source\n");
            for (int i = 0; i < V; i++)
                Console.Write(i + " \t\t " + dist[i] + "\n");

            // to do ścieżka od 0 do 4 [sekwencja tych wierzchołków, od 1 do 5]

            Hi();
        }

        public void Hi()
        {
            Console.WriteLine("HI");
        }

        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///


        public readonly int MAX_VERTICES = 30;

        int n;
        int e;
        int[,] adj;
        Vertex[] vertexList;

        private readonly int TEMPORARY = 1;
        private readonly int PERMANENT = 2;
        private readonly int NIL = -1;
        private readonly int INFINITY = 99999;

        public GFG()
        {
            adj = new int[MAX_VERTICES, MAX_VERTICES];
            vertexList = new Vertex[MAX_VERTICES];
        }


        private void Dijkstra(int s)
        {
            int v, c;

            for (v = 0; v < n; v++)
            {
                vertexList[v].status = TEMPORARY;
                vertexList[v].pathLength = INFINITY;
                vertexList[v].predecessor = NIL;
            }

            vertexList[s].pathLength = 0;

            while (true)
            {
                c = TempVertexMinPL();

                if (c == NIL)
                    return;

                vertexList[c].status = PERMANENT;

                for (v = 0; v < n; v++)
                {
                    if (IsAdjacent(c, v) && vertexList[v].status == TEMPORARY)
                        if (vertexList[c].pathLength + adj[c, v] < vertexList[v].pathLength)
                        {
                            vertexList[v].predecessor = c;
                            vertexList[v].pathLength = vertexList[c].pathLength + adj[c, v];
                        }
                }
            }
        }

        private int TempVertexMinPL()
        {
            int min = INFINITY;
            int x = NIL;
            for (int v = 0; v < n; v++)
            {
                if (vertexList[v].status == TEMPORARY && vertexList[v].pathLength < min)
                {
                    min = vertexList[v].pathLength;
                    x = v;
                }
            }
            return x;
        }

        public void FindPaths(String source)
        {
            int s = GetIndex(source);

            Dijkstra(s);

            Console.WriteLine("Source Vertex : " + source + "\n");

            for (int v = 0; v < n; v++)
            {
                Console.WriteLine("Destination Vertex : " + vertexList[v].name);
                if (vertexList[v].pathLength == INFINITY)
                    Console.WriteLine("There is no path from " + source + " to vertex " + vertexList[v].name + "\n");
                else
                    FindPath(s, v);
            }
        }

        private void FindPath(int s, int v)
        {
            int i, u;
            int[] path = new int[n];
            int sd = 0;
            int count = 0;

            while (v != s)
            {
                count++;
                path[count] = v;
                u = vertexList[v].predecessor;
                sd += adj[u, v];
                v = u;
            }
            count++;
            path[count] = s;

            Console.Write("Shortest Path is : ");
            for (i = count; i >= 1; i--)
                Console.Write(path[i] + " ");
            Console.WriteLine("\n Shortest distance is : " + sd + "\n");
        }

        private int GetIndex(String s)
        {
            for (int i = 0; i < n; i++)
                if (s.Equals(vertexList[i].name))
                    return i;
            throw new System.InvalidOperationException("Invalid Vertex");
        }

        public void InsertVertex(String name)
        {
            vertexList[n++] = new Vertex(name);
        }


        private bool IsAdjacent(int u, int v)
        {
            return (adj[u, v] != 0);
        }

        /*Insert an edge (s1,s2) */
        public void InsertEdge(String s1, String s2, int wt)
        {
            int u = GetIndex(s1);
            int v = GetIndex(s2);
            if (u == v)
                throw new System.InvalidOperationException("Not a valid edge");

            if (adj[u, v] != 0)
                Console.Write("Edge already present");
            else
            {
                adj[u, v] = wt;
                e++;
            }
        }


        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///

        // Function that implements Dijkstra's
        // single source shortest path algorithm
        // for a graph represented using adjacency
        // matrix representation
        public void dijkstra(int[,] graph, int src)
        {
            int[] dist = new int[V]; // The output array. dist[i]
                                     // will hold the shortest
                                     // distance from src to i

            // sptSet[i] will true if vertex
            // i is included in shortest path
            // tree or shortest distance from
            // src to i is finalized
            bool[] sptSet = new bool[V];

            // Initialize all distances as
            // INFINITE and stpSet[] as false
            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            // Distance of source vertex
            // from itself is always 0
            dist[src] = 0;

            // Find shortest path for all vertices
            for (int count = 0; count < V - 1; count++)
            {
                // Pick the minimum distance vertex
                // from the set of vertices not yet
                // processed. u is always equal to
                // src in first iteration.
                int u = minDistance(dist, sptSet);

                // Mark the picked vertex as processed
                sptSet[u] = true;
                //int[] P = new int[V];
                //P[V] = 0;

                // Update dist value of the adjacent
                // vertices of the picked vertex.

                for (int v = 0; v < V; v++)
                    // Update dist[v] only if is not in
                    // sptSet, there is an edge from u
                    // to v, and total weight of path
                    // from src to v through u is smaller
                    // than current value of dist[v]
                    if (!sptSet[v] && graph[u, v] != 0 &&
                         dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                    {
                        dist[v] = dist[u] + graph[u, v];
                        //todo P[V] = u;
                    }
            }

            // print the constructed distance array
            printSolution(dist, V);
        }

    }
}