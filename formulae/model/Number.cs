using System.Collections.Generic;

namespace formulae.model
{
    public class Number : IExpression
    {
        public double Value { get; set; }

        public Number(double value)
        {
            Value = value;
        }

        public List<Variable> GetVariables() => new List<Variable>();
    }
}