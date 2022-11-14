using System.Collections.Generic;
using formulae.build.parse;

namespace formulae.model
{
    public class OperationStrings
    {
        public static Dictionary<FormulaToken, string> Strings = new Dictionary<FormulaToken, string>()
        {
            { FormulaToken.EQ, "==" },
            { FormulaToken.NEQ, "!=" },
            { FormulaToken.GT, ">" },
            { FormulaToken.GTE, ">=" },
            { FormulaToken.LT, "<" },
            { FormulaToken.LTE, "<=" },
            { FormulaToken.PLUS, "+" },
            { FormulaToken.MINUS, "-" },
            { FormulaToken.DIV, "/" },
            { FormulaToken.TIMES, "*" },
            { FormulaToken.NOT, "!" },
            { FormulaToken.OR, "||" },
            { FormulaToken.AND, "&&" },
        };
    }
}