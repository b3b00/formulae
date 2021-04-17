using System.Collections.Generic;

namespace formulae.model
{
    public class Variable : IExpression
    {
        public Variable(string name)
        {
            Name = name;
        }
        
        public Variable(string name, FormulaType type)
        {
            Name = name;
            Type = type;
        }

        public Variable(Variable variable) : this(variable.Name, variable.Type)
        {
        }
        
        public string Name { get; set; }

        public List<Variable> GetVariables()
        {
            return new List<Variable> {this};
        }

        public FormulaType Type { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}