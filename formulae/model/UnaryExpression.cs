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
        public string Dump()
        {
            var dump = "";
            switch (Operation)
            {
                case FormulaToken.NOT:
                {
                    dump = "!";
                    break;
                }
                case FormulaToken.MINUS:
                {
                    dump = "-";
                    break;
                }
            }

            return $"({OperationStrings.Strings[Operation]}{Expression.Dump()})";
        }

        public bool References(string variableName)
        {
            return Expression.References(variableName);
        }
    }
}