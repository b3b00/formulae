using System;
using System.Collections.Generic;
using System.Linq;
using formulae.model;

namespace formulae.build.dependencygraph
{
    public class Dependencies
    {
        public Dependencies()
        {
            DependenciesDictionary = new Dictionary<string, Dependency>();
        }

        public Graph Graph { get; private set; }

        public Graph ReverseGraph { get; private set; }

        public Dictionary<string, Dependency> DependenciesDictionary { get; }

        public void Dump()
        {
            Console.WriteLine("direct");
            Console.WriteLine(Graph.Dump());
            Console.WriteLine();
            Console.WriteLine("reverse");
            Console.WriteLine(ReverseGraph.Dump());
        }

        public bool Build(Formulae formulae)
        {
            var xxx = formulae.Formulas.ToDictionary(x => x.Variable, y => y.Expression.GetVariables());

            foreach (var vardeps in xxx)
            {
                Dependency dep = null;
                if (!DependenciesDictionary.ContainsKey(vardeps.Key.Name))
                {
                    dep = new Dependency(vardeps.Key);
                    DependenciesDictionary[vardeps.Key.Name] = dep;
                }

                dep = DependenciesDictionary[vardeps.Key.Name];

                if (vardeps.Value.Any())
                    foreach (var variable in vardeps.Value)
                    {
                        Dependency vardep = null;
                        if (!DependenciesDictionary.ContainsKey(variable.Name))
                        {
                            vardep = new Dependency(variable);
                            DependenciesDictionary[variable.Name] = vardep;
                        }

                        dep.AddDependency(DependenciesDictionary[variable.Name]);
                    }
            }

            Graph = new Graph();

            foreach (var dep in DependenciesDictionary) Graph.AddVertex(dep.Key);
            foreach (var dep in DependenciesDictionary)
            {
                var vertex = Graph.GetVertex(dep.Key);
                foreach (var x in dep.Value.Dependencies) vertex.AddVertice(Graph.GetVertex(x.Variable.Name));
            }

            var cyclic = Graph.CheckCycle();
            if (!cyclic) buildReverseGraph();

            return cyclic;
        }

        private void buildReverseGraph()
        {
            ReverseGraph = new Graph();
            foreach (var graphVertex in Graph.Vertexes) ReverseGraph.AddVertex(graphVertex.Name);

            foreach (var graphVertex in Graph.Vertexes)
            {
                var vertex = ReverseGraph.GetVertex(graphVertex.Name);
                foreach (var vertice in graphVertex.Vertices)
                    ReverseGraph.GetVertex(vertice.Target.Name).AddVertice(graphVertex);
            }
        }

        public List<Vertex> GetDependant(string name)
        {
            return ReverseGraph.GetVertex(name).Vertices.Select(x => x.Target).ToList();
        }
    }
}