﻿using System;
using formulae.build.dependencygraph;
using formulae.build.parse;
using formulae.build.typecheck;
using formulae.engine;
using formulae.model;
using sly.lexer;
using sly.parser.generator;

namespace program
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parserInstance = new FormulaParser();
            var builder = new ParserBuilder<FormulaToken, IFormula>();
            var parserResult = builder.BuildParser(parserInstance, ParserType.EBNF_LL_RECURSIVE_DESCENT, "formulae");
            Console.WriteLine(parserResult.Result.Configuration.Dump());
            if (parserResult.IsOk)
            {
                var source = @"

ddd = aaa == 1 
aaa = bbb + 1
bbb = ccc + 2
ccc = 3 + 3 


";
                var fsm = (parserResult.Result.Lexer as GenericLexer<FormulaToken>).FSMBuilder.Fsm;
                
                var grpah = fsm.ToGraphViz();
                
                var t = parserResult.Result.Parse(source);
                if (t.IsOk)
                {
                    var form = t.Result as Formulae;
                    var depbuilder = new Dependencies();
                    depbuilder.Build(t.Result as Formulae);
                    depbuilder.Dump();
                    var checker = new TypeChecker();
                    checker.Type(form, depbuilder.DependenciesDictionary);

                    var formulaeEngine = new FormulaeEngine(depbuilder, form);
                    Console.WriteLine(formulaeEngine.ToString());
                    // Assert.Equal(6.0, formulaeEngine.GetValue("C"));
                    // Assert.Equal(8.0, formulaeEngine.GetValue("B"));
                    // Assert.Equal(9.0, formulaeEngine.GetValue("A"));
                    formulaeEngine.Set("C", 10.0);
                    // Assert.Equal(10.0, formulaeEngine.GetValue("C"));
                    // Assert.Equal(12.0, formulaeEngine.GetValue("B"));
                    // Assert.Equal(13.0, formulaeEngine.GetValue("A"));
                    Console.WriteLine(formulaeEngine.ToString());
                    ;
                }
                else
                {
                    t.Errors.ForEach(e => Console.WriteLine(e.ErrorMessage));
                }
            }
            else
            {
                parserResult.Errors.ForEach(e => Console.WriteLine(e.Level + " - " + e.Message));
                ;
            }
        }
    }
}