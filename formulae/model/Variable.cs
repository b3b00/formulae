using System.Collections.Generic;
using System.Security.Cryptography;

namespace formulae.model
{
    public class Variable : IExpression
    {
        public string Name { get; set; }

        public Variable(string name)
        {
            Name = name;
        }

        public List<Variable> GetVariables() => new List<Variable>() {this};

        public string ToString() => Name;
    }
}
    