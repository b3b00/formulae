using formulae.build.parse;
using formulae.engine;
using formulae.model;
using sly.parser;
using sly.parser.generator;
using Xunit;

namespace FormulaeTests
{
    public class ReverserTests
    {
        private Parser<FormulaToken,IFormula> Parser { get; set; }
        public void BuildParser() {
            if (Parser == null)
            {
                var parserInstance = new FormulaParser();
                var builder = new ParserBuilder<FormulaToken, IFormula>();
                var parserResult =
                    builder.BuildParser(parserInstance, ParserType.EBNF_LL_RECURSIVE_DESCENT, "formulae");
                Assert.True(parserResult.IsOk);
                Parser = parserResult.Result;
            }
        }
    

        [Theory()]
        [InlineData(@"
y = x
","x","y")]
        [InlineData(@"
y = -x
","x","(-y)")]
        [InlineData(@"
y = x + 2
","x","(y - 2)")]
        public void Reverser(string expression, string variable, string expected)
        {
            BuildParser();
            var formulas = Parser.Parse(expression);
            Assert.True(formulas.IsOk);
            var formulae = formulas.Result as Formulae;
            Assert.Single(formulae.Formulas);
            Formula formula = formulae.Formulas[0];
            Assert.NotNull(formula);
                    
                    
            var reverser = new FormulaReverser();
            var reversed = reverser.Reverse(formula,"x");
            Assert.NotNull(reversed);
            Assert.Equal(variable,reversed.Variable.Name);
            Assert.Equal(expected,reversed.Expression.Dump());
        }
        
      

    }
}