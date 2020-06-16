using System.Collections.Generic;
using System.Linq;

namespace formulae.build.dependencygraph
{
    // A C# Program to detect cycle in a graph 


    public class Vertex
    {
        public List<Vertice> Vertices;

        public Vertex(string name)
        {
            Name = name;
            Vertices = new List<Vertice>();
        }

        public string Name { get; set; }

        public bool Visited { get; set; }

        public bool IsIndependant => Vertices.Count == 0;

        public void AddVertice(Vertex target)
        {
            Vertices.Add(new Vertice(target));
        }

        public void Reset()
        {
            Visited = false;
            foreach (var vertice in Vertices) vertice.Visited = false;
        }

        public override string ToString()
        {
            return $"{Name} => {string.Join(", ", Vertices.Select(x => x.Target.Name))}";
        }
    }
}