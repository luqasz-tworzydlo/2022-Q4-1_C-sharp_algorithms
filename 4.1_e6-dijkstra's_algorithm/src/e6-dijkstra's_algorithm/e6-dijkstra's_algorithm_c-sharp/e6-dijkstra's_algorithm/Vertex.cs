﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e6_dijkstra_s_algorithm
{
    internal class Vertex
    {
        public String name;
        public int status;
        public int predecessor;
        public int pathLength;

        public Vertex(String name)
        {
            this.name = name;
        }
    }
}