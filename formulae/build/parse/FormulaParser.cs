using System.Collections.Generic;
using System.Linq;
using formulae.model;
using sly.lexer;
using sly.parser.generator;

namespace formulae.build.parse
{
    public class FormulaParser
    {
        [Operation((int) FormulaToken.PLUS, Affix.InFix, Associativity.Right, 10)]
        [Operation("MINUS", Affix.InFix, Associativity.Left, 10)]
        public IFormula BinaryTermExpression(IExpression left, Token<FormulaToken> operation, IExpression right)
        {
            return new BinaryExpression(left, operation.TokenID, right);
        }


        [Operation((int) FormulaToken.TIMES, Affix.InFix, Associativity.Right, 50)]
        [Operation("DIV", Affix.InFix, Associativity.Left, 50)]
        public IFormula BinaryFactorExpression(IExpression left, Token<FormulaToken> operation, IExpression right)
        {
            return new BinaryExpression(left, operation.TokenID, right);
        }


        [Operation((int) FormulaToken.MINUS, Affix.PreFix, Associativity.Right, 100)]
        public IFormula MinusPreFixExpression(Token<FormulaToken> operation, IExpression value)
        {
            return new UnaryExpression(FormulaToken.MINUS, value);
        }

        [Operation((int) FormulaToken.NOT, Affix.PreFix, Associativity.Right, 100)]
        public IFormula NotExpression(Token<FormulaToken> operation, IExpression value)
        {
            return new UnaryExpression(FormulaToken.NOT, value);
        }


        [Operand]
        [Production("double_value : DOUBLE")]
        public IFormula OperandIFormula(Token<FormulaToken> value)
        {
            return new Number(value.DoubleValue);
        }

        [Operand]
        [Production("int_value : INT")]
        public IFormula OperandInt(Token<FormulaToken> value)
        {
            return new Number(value.DoubleValue);
        }

        [Operand]
        [Production("group_value : LPAREN[d] FormulaParser_expressions RPAREN[d]")]
        public IFormula OperandParens(IExpression value)
        {
            return value;
        }

        [Operand]
        [Production("id_value: ID")]
        public IFormula Id(Token<FormulaToken> id)
        {
            return new Variable(id.Value);
        }

        [Operand]
        [Production("bool_value: [ TRUE | FALSE ]")]
        public IFormula Bool(Token<FormulaToken> boolean)
        {
            var v = false;
            bool.TryParse(boolean.Value, out v);
            return new Boolean(v);
        }


        [Production("formula : ID SET[d] FormulaParser_expressions [ WIN_EOL | IX_EOL ] [d]")]
        public IFormula Formula(Token<FormulaToken> id, IExpression expression)
        {
            return new Assignment(new Variable(id.Value), expression);
        }

        [Production("emptyLine : [ WIN_EOL | IX_EOL ] [d]")]
        public IFormula EmptyLine()
        {
            return null;
        }

        [Production("formulae : [ emptyLine | formula ] *")]
        public IFormula Formulae(List<IFormula> formulas)
        {
            return new Formulae(formulas.Where(x => x != null).ToList());
        }
    }
}