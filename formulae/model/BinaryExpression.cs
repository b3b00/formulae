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
        public string Dump()
        {
            var dump = Left.Dump();
            switch (Operation)
            {
                case FormulaToken.EQ:
                {
                    dump += " == ";
                    break;
                }
                case FormulaToken.GT:
                {
                    dump += " > ";
                    break;
                }
                case FormulaToken.GTE:
                {
                    dump += " >= ";
                    break;
                }
                case FormulaToken.LT:
                {
                    dump += " < ";
                    break;
                }
                case FormulaToken.LTE:
                {
                    dump += " <= ";
                    break;
                }
                case FormulaToken.NEQ:
                {
                    dump += " != ";
                    break;
                }
                case FormulaToken.PLUS:
                {
                    dump += " + ";
                    break;
                }
                case FormulaToken.MINUS:
                {
                    dump += " - ";
                    break;
                }
                case FormulaToken.DIV:
                {
                    dump += " / ";
                    break;
                }
                case FormulaToken.TIMES:
                {
                    dump += " * ";
                    break;
                }
                case FormulaToken.OR:
                {
                    dump += " || ";
                    break;
                }
                case FormulaToken.AND:
                {
                    dump += " && ";
                    break;
                }
            }

            return "("+dump + Right.Dump()+")";
        }

        public bool References(string variableName)
        {
            return Left.References(variableName) || Right.References(variableName);
        }
    }
}