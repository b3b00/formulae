using System;
using System.Collections.Generic;
using System.Linq;
using formulae.build.dependencygraph;
using formulae.build.parse;
using formulae.model;
using Boolean = formulae.model.Boolean;

namespace formulae.build.typecheck
{
    public class TypeChecker
    {
        public Dictionary<string, FormulaType> VariableTypes { get; private set; }

        private FormulaType GetType(IExpression expression)
        {
            return expression switch
            {
                Boolean b => FormulaType.Bool,
                Number n => FormulaType.Number,
                UnaryExpression u => GetType(u.Expression),
                BinaryExpression b => GetBinaryOperationType(b),
                Variable v => VariableTypes.ContainsKey(v.Name) ? VariableTypes[v.Name] : FormulaType.Error,
                _ => FormulaType.Error
            };
        }

        private FormulaType GetBinaryOperationType(BinaryExpression expression)
        {
            switch (expression.Operation)
            {
                case FormulaToken.GT:
                case FormulaToken.LT:
                case FormulaToken.GTE:
                case FormulaToken.LTE:
                {
                    // TODO : assert left and right are the same (number)
                    var leftType = GetType(expression.Left);
                    var rightType = GetType(expression.Left);
                    if (leftType != rightType)
                        throw new Exception($"unknown operation {leftType} {expression.Operation} {rightType}");

                    if (leftType != FormulaType.Number)
                        throw new Exception($"unknown operation {leftType} {expression.Operation} {rightType}");

                    expression.Type = FormulaType.Bool;
                    return FormulaType.Bool;
                }
                case FormulaToken.EQ:
                case FormulaToken.NEQ:
                {
                    var leftType = GetType(expression.Left);
                    var rightType = GetType(expression.Left);
                    if (leftType != rightType)
                        throw new Exception($"unknown operation {leftType} {expression.Operation} {rightType}");
                    expression.Type = FormulaType.Bool;
                    return FormulaType.Bool;
                }
                case FormulaToken.PLUS:
                case FormulaToken.MINUS:
                case FormulaToken.TIMES:
                case FormulaToken.DIV:
                {
                    expression.Type = FormulaType.Number;
                    return FormulaType.Number;
                }
                default:
                {
                    expression.Type = FormulaType.Error;
                    return FormulaType.Error;
                }
            }
        }


        public bool CanbeTyped(Dependency dependency)
        {
            if (dependency.Dependencies == null || !dependency.Dependencies.Any()) return true;

            var canVars = dependency.Dependencies.Select(x => VariableTypes.ContainsKey(x.Variable.Name)).ToList();

            return canVars.Aggregate((z, y) => z && y);
        }

        public void Type(Formulae formulae, Dictionary<string, Dependency> dependencies)
        {
            var dependenciesWork = new Dictionary<string, Dependency>();
            foreach (var keyValuePair in dependencies) dependenciesWork[keyValuePair.Key] = keyValuePair.Value;

            VariableTypes = new Dictionary<string, FormulaType>();

            while (dependenciesWork.Any())
            {
                var first = dependenciesWork.Values.First(x => CanbeTyped(x));
                var formula = formulae.Formulas.First(x => x.Variable.Name == first.Variable.Name);
                var type = GetType(formula.Expression);
                formula.Type = type;
                VariableTypes[first.Variable.Name] = type;
                dependenciesWork.Remove(first.Variable.Name);
            }
        }
    }
}