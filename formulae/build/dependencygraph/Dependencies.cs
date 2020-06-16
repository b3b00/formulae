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

        public DependencyGraph DependencyGraph { get; private set; }

        public DependencyGraph ReverseDependencyGraph { get; private set; }

        public Dictionary<string, Dependency> DependenciesDictionary { get; }

        public void Dump()
        {
            Console.WriteLine("direct");
            Console.WriteLine(DependencyGraph.Dump());
            Console.WriteLine();
            Console.WriteLine("reverse");
            Console.WriteLine(ReverseDependencyGraph.Dump());
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

            DependencyGraph = new DependencyGraph();

            foreach (var dep in DependenciesDictionary) DependencyGraph.AddVertex(dep.Key);
            foreach (var dep in DependenciesDictionary)
            {
                var vertex = DependencyGraph.GetVertex(dep.Key);
                foreach (var x in dep.Value.Dependencies) vertex.AddVertice(DependencyGraph.GetVertex(x.Variable.Name));
            }

            var cyclic = DependencyGraph.CheckCycle();
            if (!cyclic) buildReverseGraph();

            return cyclic;
        }

        private void buildReverseGraph()
        {
            ReverseDependencyGraph = new DependencyGraph();
            foreach (var graphVertex in DependencyGraph.Vertexes) ReverseDependencyGraph.AddVertex(graphVertex.Name);

            foreach (var graphVertex in DependencyGraph.Vertexes)
            {
                var vertex = ReverseDependencyGraph.GetVertex(graphVertex.Name);
                foreach (var vertice in graphVertex.Vertices)
                    ReverseDependencyGraph.GetVertex(vertice.Target.Name).AddVertice(graphVertex);
            }
        }

        public List<DependencyVertex> GetDependant(string name)
        {
            return ReverseDependencyGraph.GetVertex(name).Vertices.Select(x => x.Target).ToList();
        }
    }
}