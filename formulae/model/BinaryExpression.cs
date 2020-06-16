using System.Collections.Generic;
using System.Linq;
using formulae.build.parse;

namespace formulae.model
{
    public class BinaryExpression : IExpression
    {
        public FormulaToken Operation { get; set; }
        
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }

        public BinaryExpression(IExpression left, FormulaToken operation, IExpression right)
        {
            Operation = operation;
            Right = right;
            Left = left;
        }

        public List<Variable> GetVariables()
        {
            return Left.GetVariables().Concat(Right.GetVariables()).ToList();
        }
        
        public FormulaType Type { get; set; }
    }
}