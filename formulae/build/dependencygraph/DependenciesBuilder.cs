using System;
using System.Collections.Generic;
using System.Linq;
using formulae.model;

namespace formulae.build.dependencygraph
{
    public class DependenciesBuilder
    {

        public Dictionary<string,Dependency> Dependencies { get; }

        public DependenciesBuilder()
        {
            Dependencies = new Dictionary<string, Dependency>();
        }

        public void Dump()
        {
            foreach (var dep in Dependencies)
            {
                Console.WriteLine(dep.Value.ToString());
            }
        }

        public void Build(Formulae formulae)
        {
            var xxx = formulae.Formulas.ToDictionary(x => x.Variable, y => y.Expression.GetVariables());

            foreach (var vardeps in xxx)
            {
                Dependency dep = null;
                if (!Dependencies.ContainsKey(vardeps.Key.Name))
                {
                    dep = new Dependency(vardeps.Key);
                    Dependencies[vardeps.Key.Name] = dep;
                }

                dep = Dependencies[vardeps.Key.Name];
                
                if (vardeps.Value.Any())
                {
                    foreach (var variable in vardeps.Value)
                    {
                        Dependency vardep = null;
                        if (!Dependencies.ContainsKey(variable.Name))
                        {
                            vardep = new Dependency(variable);
                            Dependencies[variable.Name] = vardep;
                        }
                        
                        dep.AddDependency(Dependencies[variable.Name]);
                    }
                }
            }

            Graph graph = new Graph();
            
            foreach (var dep in Dependencies)
            {
                graph.AddVertex(dep.Key);
            }
            foreach (var dep in Dependencies)
            {
                var vertex = graph.GetVertex(dep.Key);
                foreach (var x in dep.Value.Dependencies)
                {
                    vertex.AddVertice(graph.GetVertex(x.Variable.Name));
                }
            }

            var cyclic = graph.CheckCycle();

        }
        
        


    }
}