using System; 
using System.Collections.Generic;
using System.Resources;

namespace formulae.build.dependencygraph
{
    // A C# Program to detect cycle in a graph 
  

    public class Vertex { 

        public string Name { get; set; }
        
        public bool Visited { get; set; }

        public List<Vertice> Vertices;

        public Vertex(string name)
        {
            Name = name;
            Vertices = new List<Vertice>();
        }

        public void AddVertice(Vertex target)
        {
            Vertices.Add(new Vertice(target));
        }
        
        public void Reset()
        {
            Visited = false;
            foreach (var vertice in Vertices)
            {
                vertice.Visited = false;
            }
        }
    }
}