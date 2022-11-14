using System.Collections.Generic;
using System.Linq;
using formulae.build.parse;

namespace formulae.model
{
    public class BinaryExpression : IExpression
    {
        public BinaryExpression(IExpression left, FormulaToken operation, IExpression right)
        {
            Operation = operation;
            Right = right;
            Left = left;
        }

        public FormulaToken Operation { get; set; }

        public IExpression Left { get; set; }
        public IExpression Right { get; set; }

        public List<Variable> GetVariables()
        {
            return Left.GetVariables().Concat(Right.GetVariables()).ToList();
        }

        public FormulaType Type { get; set; }
        
        public bool References(string variableName)
        {
            return Left.References(variableName) || Right.References(variableName);
        }
    }
}