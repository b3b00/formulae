using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace formulae.build.dependencygraph
{
    public class DependencyGraph
    {
        public DependencyGraph()
        {
            Vertexes = new List<DependencyVertex>();
        }

        public List<DependencyVertex> Vertexes { get; set; }

        public void Reset()
        {
            foreach (var vertex in Vertexes) vertex.Reset();
        }

        public void AddVertex(string name)
        {
            Vertexes.Add(new DependencyVertex(name));
        }

        public DependencyVertex GetVertex(string name)
        {
            return Vertexes.FirstOrDefault(x => x.Name == name);
        }


        public bool CheckCycle()
        {
            foreach (var vertex in Vertexes)
            {
                if (CheckCycle(vertex)) return true;
                Reset();
            }

            return false;
        }

        private bool CheckCycle(DependencyVertex dependencyVertex)
        {
            var cycle = false;
            if (dependencyVertex.Visited) return true;
            dependencyVertex.Visited = true;
            var vertices = dependencyVertex.Vertices.Where(v => !v.Visited);
            if (vertices.Any())
                foreach (var vertice in vertices)
                {
                    vertice.Visited = true;
                    cycle = cycle || CheckCycle(vertice.Target);
                }

            return cycle;
        }

        public string Dump()
        {
            var builder = new StringBuilder();
            foreach (var vertex in Vertexes) builder.AppendLine(vertex.ToString());

            return builder.ToString();
        }
    }
}