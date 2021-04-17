using System;
using formulae.build.parse;
using formulae.model;

namespace formulae.engine
{
    public class FormulaReverser : IFormulaReverser
    {

        public Formula Reverse(Formula formula, string name)
        {
            return Reverse(formula, new Variable(name));
        }

        private FormulaToken Reverse(FormulaToken op)
        {
            switch(op) { 
                    case FormulaToken.PLUS : return FormulaToken.MINUS;
                    case FormulaToken.MINUS : return FormulaToken.PLUS;
                    case FormulaToken.TIMES : return FormulaToken.DIV;
                    case FormulaToken.DIV : return FormulaToken.TIMES;
                    default :  throw new FormulaException("");
                }   
        }
        public Formula Reverse(Formula formula, Variable variable)
        {
            var head = new Variable(variable);
            IExpression body = null; 
            if (formula.Expression is Variable v && v.Name == variable.Name)
            {
                body = formula.Variable;
            }
            else
            {
                // TODO
                // bin.other_operand(string x) = l'operand du binary qui ne contient pas x)
                //      -> si les 2 contiennent  x alors ????
                // si body binary : construire un binary : head reverse(bin.op) bin.other_operand
            }

            return new Formula(head, body);
        }
    }
}