using formulae.build.parse;
using System.Collections.Generic;

namespace formulae.model
{
    public class UnaryExpression : IExpression
    {
        public FormulaToken Operation { get; set; }
        
        public IExpression Expression { get; set; }

        public UnaryExpression(FormulaToken operation, IExpression expression)
        {
            Operation = operation;
            Expression = expression;
        }
        
        public List<Variable> GetVariables() => Expression.GetVariables();
        
        
    }
}