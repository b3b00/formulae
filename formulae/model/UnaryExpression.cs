using System.Collections.Generic;
using formulae.build.parse;

namespace formulae.model
{
    public class UnaryExpression : IExpression
    {
        public UnaryExpression(FormulaToken operation, IExpression expression)
        {
            Operation = operation;
            Expression = expression;
        }

        public FormulaToken Operation { get; set; }

        public IExpression Expression { get; set; }

        public List<Variable> GetVariables()
        {
            return Expression.GetVariables();
        }

        public FormulaType Type { get; set; }
    }
}