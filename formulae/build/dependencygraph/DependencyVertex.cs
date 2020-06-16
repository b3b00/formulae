using System.Collections.Generic;
using System.Linq;

namespace formulae.build.dependencygraph
{
    // A C# Program to detect cycle in a graph 


    public class DependencyVertex
    {
        public List<DependencyVertice> Vertices;

        public DependencyVertex(string name)
        {
            Name = name;
            Vertices = new List<DependencyVertice>();
        }

        public string Name { get; set; }

        public bool Visited { get; set; }

        public bool IsIndependant => Vertices.Count == 0;

        public void AddVertice(DependencyVertex target)
        {
            Vertices.Add(new DependencyVertice(target));
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