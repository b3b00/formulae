using System;
using System.Collections.Generic;
using System.Resources;
using formulae.build.parse;
using formulae.engine;
using formulae.model;
using sly.parser.generator;
using Xunit;

namespace FormulaeTests
{
    public class ReverserTests
    {
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
    }
}