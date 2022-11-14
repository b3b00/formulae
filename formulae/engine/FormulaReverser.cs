using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using formulae.build.parse;
using formulae.model;

namespace formulae.engine
{
    public class FormulaReverser : IFormulaReverser
    {

        public Dictionary<FormulaToken, FormulaToken> Opposites = new Dictionary<FormulaToken, FormulaToken>()
        {
            { FormulaToken.EQ, FormulaToken.NEQ },
            { FormulaToken.LTE, FormulaToken.GT },
            { FormulaToken.GTE, FormulaToken.LT },
            { FormulaToken.PLUS, FormulaToken.MINUS },
            { FormulaToken.MINUS, FormulaToken.PLUS },
            { FormulaToken.TIMES, FormulaToken.DIV },
            { FormulaToken.DIV, FormulaToken.TIMES },
            { FormulaToken.AND, FormulaToken.OR },
            { FormulaToken.OR, FormulaToken.AND },
        };

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
                if (formula.Expression is BinaryExpression bin)
                {
                    if (bin.Left.References(variable.Name) && !bin.Right.References(variable.Name))
                    {
                        body = new BinaryExpression(formula.Variable, Opposites[bin.Operation], bin.Right);
                    }
                    else if (bin.Right.References(variable.Name) && !bin.Left.References(variable.Name))
                    {
                        body = new BinaryExpression(bin.Left, Opposites[bin.Operation], formula.Variable);
                    }
                    else
                    {
                        throw new NotImplementedException("not implemented");
                    }
                }

                if (formula.Expression is UnaryExpression unary)
                {
                    if (unary.Expression.References(variable.Name))
                    {
                        switch (unary.Operation)
                        {
                            case FormulaToken.MINUS:
                            {
                                body = new UnaryExpression(FormulaToken.MINUS, formula.Variable);
                                break;
                            }
                            case FormulaToken.NOT:
                            {
                                body = new UnaryExpression(FormulaToken.NOT, formula.Variable);
                                break;
                            }
                        } 
                    }
                }
                
            }

            return new Formula(head, body);
        }
    }
}