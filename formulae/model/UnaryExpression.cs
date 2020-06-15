using formulae.build;

namespace formulae.model
{
    public class UnaryExpression : IExpression
    {
        public FormulaToken Operation { get; set; }
        
        public IFormula Expression { get; set; }

        public UnaryExpression(FormulaToken operation, IFormula expression)
        {
            Operation = operation;
            Expression = expression;
        }
        
        
    }
}