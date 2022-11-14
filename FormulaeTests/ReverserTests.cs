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
    
    
        [Fact]
        public void BuildParserTest()
        {
            var parserInstance = new FormulaParser();
            var builder = new ParserBuilder<FormulaToken, IFormula>();
            var parserResult = builder.BuildParser(parserInstance, ParserType.EBNF_LL_RECURSIVE_DESCENT, "formulae");
            Assert.True(parserResult.IsOk);
        }

        [Fact]
        public void MinimalTest()
        {
            var formula = new Formula(new Variable("x", FormulaType.Number), new Variable("y", FormulaType.Number));
            var reverser = new FormulaReverser();
            var reversed = reverser.Reverse(formula,"y");
            Assert.Equal("y",reversed.Variable.Name);
            
            Variable body = reversed.Expression as Variable;
            Assert.NotNull(body);
            Assert.Equal("x",body.Name);
        }
        
        [Fact]
                public void BinaryTest()
                {
                    BuildParser();
                    var formulas = Parser.Parse(@"
y = x  + 2
");
                    Assert.True(formulas.IsOk);
                    var formulae = formulas.Result as Formulae;
                    Assert.Single(formulae.Formulas);
                    Formula formula = formulae.Formulas[0];
                    Assert.NotNull(formula);
                    
                    
                     var reverser = new FormulaReverser();
                     var reversed = reverser.Reverse(formula,"x");
                     Assert.NotNull(reversed);
                     Assert.Equal("y",reversed.Variable.Name);
                     Assert.IsType<BinaryExpression>(reversed.Expression);
                     var binary = reversed.Expression as BinaryExpression;

                     //
                     // Variable body = reversed.Expression as Variable;
                     // Assert.NotNull(body);
                     // Assert.Equal("x",body.Name);
                }

                [Fact]
                public void UnaryTest()
                {
                    BuildParser();
                    var formulas = Parser.Parse(@"
y = -x
");
                    Assert.True(formulas.IsOk);
                    var formulae = formulas.Result as Formulae;
                    Assert.Single(formulae.Formulas);
                    Formula formula = formulae.Formulas[0];
                    Assert.NotNull(formula);


                    var reverser = new FormulaReverser();
                    var reversed = reverser.Reverse(formula, "x");
                    Assert.NotNull(reversed);
                    Assert.Equal("x", reversed.Variable.Name);
                    Assert.IsType<UnaryExpression>(reversed.Expression);
                    var unary = reversed.Expression as UnaryExpression;
                    Assert.Equal(FormulaToken.MINUS,unary.Operation);
                    Assert.IsType<Variable>(unary.Expression);
                    Assert.Equal("y",(unary.Expression as Variable).Name);

                    //
                    // Variable body = reversed.Expression as Variable;
                    // Assert.NotNull(body);
                    // Assert.Equal("x",body.Name);
                }
    }
}