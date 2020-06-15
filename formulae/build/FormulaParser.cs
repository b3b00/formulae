using sly.lexer;
using sly.parser.generator;
using formulae.model;
using System.Collections.Generic;

namespace formulae.build
{


    public class FormulaParser
    {

        [Operation((int)FormulaToken.PLUS, Affix.InFix, Associativity.Right, 10)]
        [Operation("MINUS", Affix.InFix, Associativity.Left, 10)]
        public IFormula BinaryTermExpression(IFormula left, Token<FormulaToken> operation, IFormula right)
        {
            IFormula result = null;
            switch (operation.TokenID)
            {
                case FormulaToken.PLUS:
                    {
                        result = null;
                        break;
                    }
                case FormulaToken.MINUS:
                    {
                        result = null;
                        break;
                    }
            }

            return result;
        }


        [Operation((int)FormulaToken.TIMES, Affix.InFix, Associativity.Right, 50)]
        [Operation("DIV", Affix.InFix, Associativity.Left, 50)]
        public IFormula BinaryFactorExpression(IFormula left, Token<FormulaToken> operation, IFormula right)
        {
            IFormula result = null;
            switch (operation.TokenID)
            {
                case FormulaToken.TIMES:
                    {
                        result = null;
                        break;
                    }
                case FormulaToken.DIV:
                    {
                        result = null;
                        break;
                    }
            }

            return result;
        }


        [Operation((int)FormulaToken.MINUS, Affix.PreFix, Associativity.Right, 100)]
        public IFormula MinusPreFixExpression(Token<FormulaToken> operation, IFormula value)
        {
            return value;
        }

        [Operation((int)FormulaToken.NOT, Affix.PreFix, Associativity.Right, 100)]
        public IFormula NotExpression(Token<FormulaToken> operation, IFormula value)
        {
            return value;
        }



        [Operand]
        [Production("double_value : DOUBLE")]
        public IFormula OperandIFormula(Token<FormulaToken> value)
        {
            return null;
        }

        [Operand]
        [Production("int_value : INT")]
        public IFormula OperandInt(Token<FormulaToken> value)
        {
            return null;
        }

        [Operand]
        [Production("group_value : LPAREN FormulaParser_expressions RPAREN")]
        public IFormula OperandParens(Token<FormulaToken> lparen, IFormula value, Token<FormulaToken> rparen)
        {
            return value;
        }

        [Operand]
        [Production("id_value: ID")]
        public IFormula Id(Token<FormulaToken> id)
        {
            return null;
        }

        


        [Production("formula : ID SET[d] FormulaParser_expressions [ WIN_EOL | IX_EOL | MAC_EOL ] [d]")]
        public IFormula Formula(Token<FormulaToken> id, IFormula expression) {
            return null;
        }

        [Production("emptyLine : [ WIN_EOL | IX_EOL | MAC_EOL ] [d]")]
        public IFormula EmptyLine() {
            return null;
        }

        [Production("formulae : [ emptyLine | formula ] *")] 
        public IFormula Formulae(List<IFormula> formulas) {
            return null;
        }



    }
}