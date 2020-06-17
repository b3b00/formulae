using System;
using System.Resources;
using formulae.build.parse;
using formulae.model;
using sly.parser.generator;
using Xunit;

namespace FormulaeTests
{
    public class CompileTests
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
        public void ParseTest()
        {
            var parserInstance = new FormulaParser();
            var builder = new ParserBuilder<FormulaToken, IFormula>();
            var parserResult = builder.BuildParser(parserInstance, ParserType.EBNF_LL_RECURSIVE_DESCENT, "formulae");
            Assert.True(parserResult.IsOk);
            var source = @"

aaa = bbb + 1
bbb = ccc + 2
ccc = 3 + 3 


";
            var t = parserResult.Result.Parse(source);
            Assert.True(t.IsOk);
            
        }
    }
}