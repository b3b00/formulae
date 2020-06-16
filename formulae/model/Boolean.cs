using System.Collections.Generic;

namespace formulae.model
{
    public class Boolean : IExpression
    {
        public Boolean(bool value)
        {
            value = value;
        }

        public bool Value { get; set; }

        public List<Variable> GetVariables()
        {
            return new List<Variable>();
        }

        public FormulaType Type { get; set; } = FormulaType.Bool;
    }
}