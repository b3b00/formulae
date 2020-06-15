using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using formulae.model;
using sly.lexer;

namespace formulae.build.dependencygraph
{
    public class Dependency
    {
        public Variable Variable {get; set;}
        
        public List<Dependency> Dependencies { get; set; }

        
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
        
        

        public void AddDependency(Dependency dependency) => Dependencies.Add(dependency);

        public string ToString()
        {
            return $"{Variable.Name} => {string.Join(", ", Dependencies.Select(x => x.Variable.Name))}";
        }
        
    }
}