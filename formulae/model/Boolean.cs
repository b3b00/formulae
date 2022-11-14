using System.Collections.Generic;

namespace formulae.model
{
    public class Boolean : IExpression
    {
        public Boolean(bool value)
        {
            Value = value;
        }

        public bool Value { get; set; }

        public List<Variable> GetVariables()
        {
            return new List<Variable>();
        }

        public bool References(string variableName)
        {
            return false;
        }

        public FormulaType Type { get; set; } = FormulaType.Bool;
        public string Dump()
        {
            return Value.ToString();
        }
    }
}