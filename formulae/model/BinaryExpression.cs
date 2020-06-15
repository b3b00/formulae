using formulae.build;

namespace formulae.model
{
    public class BinaryExpression : IExpression
    {
        public FormulaToken Operation { get; set; }
        
        public IFormula Left { get; set; }
        public IFormula Right { get; set; }

        public BinaryExpression(IFormula left, FormulaToken operation, IFormula right)
        {
            Operation = operation;
            Right = right;
            Left = left;
        }
    }
}