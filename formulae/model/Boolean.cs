using System.Collections.Generic;

namespace formulae.model
{
    public class Boolean : IExpression
    {
        
        public bool Value { get; set; }
        public Boolean(bool value)
        {
            value = value;
        }
        
        public List<Variable> GetVariables() => new List<Variable>();

        public FormulaType Type { get; set; } = FormulaType.Bool;
    }
}