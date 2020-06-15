using System;
using formulae.build.dependencygraph;
using formulae.build.parse;
using formulae.build.typecheck;
using formulae.model;
using sly.parser.generator;

namespace program
{
    class Program
    {
        static void Main(string[] args)
        {
            var parserInstance = new FormulaParser();
            var builder = new ParserBuilder<FormulaToken, IFormula>();
            var parserResult = builder.BuildParser(parserInstance, ParserType.EBNF_LL_RECURSIVE_DESCENT, "formulae");
            Console.WriteLine(parserResult.Result.Configuration.Dump());
            if (parserResult.IsOk) {
                string source = @"
A = B + 1
B = C + 2
# oublie moi
C = 3 + 3 / A

";
                var t = parserResult.Result.Parse(source);
                if (t.IsOk)
                {
                    Formulae form = t.Result as Formulae;
                    DependenciesBuilder depbuilder = new DependenciesBuilder();
                    depbuilder.Build(t.Result as Formulae);
                    depbuilder.Dump();
                    TypeChecker checker = new TypeChecker();
                    checker.Type(form, depbuilder.Dependencies);
                    Console.WriteLine("OK !");
                }
                else {
                    t.Errors.ForEach(e => Console.WriteLine(e.ErrorMessage));
                }
            }
            else {
                parserResult.Errors.ForEach(e => Console.WriteLine(e.Level+" - "+e.Message));
                ;
            }

        }
    }
}
