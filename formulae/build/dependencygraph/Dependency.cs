using System.Collections.Generic;
using System.Linq;
using formulae.model;

namespace formulae.build.dependencygraph
{
    public class Dependency
    {
        public Dependency(Variable variable)
        {
            Variable = variable;
            Dependencies = new List<Dependency>();
        }

        public Dependency(Variable variable, List<Dependency> dependencies)
        {
            Variable = variable;
            Dependencies = dependencies;
        }

        public Variable Variable { get; set; }

        public List<Dependency> Dependencies { get; set; }


        public void AddDependency(Dependency dependency)
        {
            Dependencies.Add(dependency);
        }

        public override string ToString()
        {
            return $"{Variable.Name} => {string.Join(", ", Dependencies.Select(x => x.Variable.Name))}";
        }
    }
}