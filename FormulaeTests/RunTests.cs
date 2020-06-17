using System;
using formulae.build.dependencygraph;
using formulae.build.parse;
using formulae.build.typecheck;
using formulae.engine;
using formulae.model;
using sly.parser.generator;
using Xunit;

namespace FormulaeTests
{
    public class RunTests
    {
        [Fact]
        public void PreRunTest()
        {
            var parserInstance = new FormulaParser();
            var builder = new ParserBuilder<FormulaToken, IFormula>();
            var parserResult = builder.BuildParser(parserInstance, ParserType.EBNF_LL_RECURSIVE_DESCENT, "formulae");
            Console.WriteLine(parserResult.Result.Configuration.Dump());
            Assert.True(parserResult.IsOk);
            var source = @"

aaa = bbb + 1
bbb = ccc + 2
ccc = 3 + 3 


";
            var t = parserResult.Result.Parse(source);
            Assert.True(t.IsOk);
            
            var form = t.Result as Formulae;
            var depbuilder = new Dependencies();
            bool isCyclic = depbuilder.Build(t.Result as Formulae);
            Assert.False(isCyclic);
            var checker = new TypeChecker();
            checker.Type(form, depbuilder.DependenciesDictionary);
            
        }
        
        [Fact]
        public void RunTest()
        {
            var parserInstance = new FormulaParser();
            var builder = new ParserBuilder<FormulaToken, IFormula>();
            var parserResult = builder.BuildParser(parserInstance, ParserType.EBNF_LL_RECURSIVE_DESCENT, "formulae");
            Console.WriteLine(parserResult.Result.Configuration.Dump());
            Assert.True(parserResult.IsOk);
            var source = @"

aaa = bbb + 1
bbb = ccc + 2
ccc = 3 + 3 


";
            var t = parserResult.Result.Parse(source);
            Assert.True(t.IsOk);
            
            var form = t.Result as Formulae;
            var depBuilder = new Dependencies();
            bool isCyclic = depBuilder.Build(t.Result as Formulae);
            Assert.False(isCyclic);
            var checker = new TypeChecker();
            checker.Type(form, depBuilder.DependenciesDictionary);
            
            var formulaeEngine = new FormulaeEngine(depBuilder, form);
            Assert.Equal(6.0, formulaeEngine.GetValue("ccc"));
            Assert.Equal(8.0, formulaeEngine.GetValue("bbb"));
            Assert.Equal(9.0, formulaeEngine.GetValue("aaa"));
            formulaeEngine.Set("ccc", 10.0);
            Assert.Equal(10.0, formulaeEngine.GetValue("ccc"));
            Assert.Equal(12.0, formulaeEngine.GetValue("bbb"));
            Assert.Equal(13.0, formulaeEngine.GetValue("aaa"));
            
        }
    }
}