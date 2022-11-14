using System.Collections.Generic;

namespace formulae.model
{
    public class Number : IExpression
    {
        public Number(double value)
        {
            Value = value;
        }

        public double Value { get; set; }

        public List<Variable> GetVariables()
        {
            return new List<Variable>();
        }
        
        public bool References(string variableName)
        {
            return false;
        }

        public FormulaType Type { get; set; } = FormulaType.Number;
    }
}