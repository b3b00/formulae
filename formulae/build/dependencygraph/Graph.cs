using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using formulae.model;

namespace formulae.build.dependencygraph
{
    public class Graph
    {
        public List<Vertex> Vertexes { get; set; }

        public void Reset()
        {
            foreach (var vertex in Vertexes)
            {
                vertex.Reset();
            }
        }

        public Graph()
        {
            Vertexes = new List<Vertex>();
        }

        public void AddVertex(string name)
        {
            Vertexes.Add(new Vertex(name));
        }

        public Vertex GetVertex(string name) => Vertexes.FirstOrDefault(x => x.Name == name);

        
        
        public bool CheckCycle()
        {
            foreach (var vertex in Vertexes)
            {
                if (CheckCycle(vertex))
                {
                    return true;
                }
                Reset();
            }
            return false;
        }

        private bool CheckCycle(Vertex vertex)
        {
            bool cycle = false;
            if (vertex.Visited)
            {
                return true;
            }
            vertex.Visited = true;
            var vertices = vertex.Vertices.Where(v => !v.Visited);
            if (vertices.Any())
            {
                
                foreach (var vertice in vertices)
                {
                    vertice.Visited = true;
                    cycle = cycle ||  CheckCycle(vertice.Target);
                }
            }
            return cycle;
        }
        
        public string Dump()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var vertex in Vertexes)
            {
                builder.AppendLine(vertex.ToString());
            }

            return builder.ToString();
        }
    }
}