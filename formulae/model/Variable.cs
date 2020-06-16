using System.Collections.Generic;

namespace formulae.model
{
    public class Variable : IExpression
    {
        public Variable(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public List<Variable> GetVariables()
        {
            return new List<Variable> {this};
        }

        public FormulaType Type { get; set; }

        public string ToString()
        {
            return Name;
        }
    }
}