using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using formulae.build.dependencygraph;
using formulae.build.parse;
using formulae.model;
using Boolean = formulae.model.Boolean;

namespace formulae.engine
{
    public class FormulaeEngine
    {
        private readonly Graph DependencyGraph;

        private readonly Formulae Formulae;
        private readonly Graph ReverseDependencyGraph;

        private readonly Dictionary<string, object> State;

        public FormulaeEngine(Dependencies dependencies, Formulae formulae)
        {
            ReverseDependencyGraph = dependencies.ReverseGraph;
            DependencyGraph = dependencies.Graph;

            Formulae = formulae;
            State = new Dictionary<string, object>();
            InitializeState();
        }

        private void InitializeState()
        {
            foreach (var vertex in DependencyGraph.Vertexes) State[vertex.Name] = null;

            foreach (var vertex in DependencyGraph.Vertexes.Where(x => x.IsIndependant))
            {
                var revertex = ReverseDependencyGraph.GetVertex(vertex.Name);
                Propagate(revertex);
            }

            ;
        }


        public void Set(string name, object value)
        {
            if (State.ContainsKey(name))
            {
                var formula = Formulae.Formulas.First(x => x.Variable.Name == name);
                if (formula.Type == FormulaType.Bool && !(value is bool)) throw new Exception("bard type");
                if (formula.Type == FormulaType.Number && !(value is double)) throw new Exception("bard type");

                State[name] = value;
                var vertex = ReverseDependencyGraph.GetVertex(name);
                //Evaluate(vertex);
                Propagate(vertex, false);
            }
        }

        public string ToString()
        {
            var builder = new StringBuilder();
            foreach (var state in State) builder.AppendLine($"{state.Key} = {state.Value}");

            return builder.ToString();
        }

        public object GetValue(string name)
        {
            object value = null;
            State.TryGetValue(name, out value);
            return value;
        }


        #region expressions evaluation

        private void Propagate(Vertex vertex, bool evaluateVertex = true)
        {
            if (evaluateVertex) Evaluate(vertex);

            foreach (var vertice in vertex.Vertices)
            {
                var target = ReverseDependencyGraph.GetVertex(vertice.Target.Name);
                Propagate(target);
            }
        }

        private object Evaluate(BinaryExpression binary)
        {
            var left = Evaluate(binary.Left);
            var right = Evaluate(binary.Right);

            var leftComparable = (IComparable) left;
            var rightComparable = (IComparable) right;
            var comparison = leftComparable.CompareTo(rightComparable);

            switch (binary.Operation)
            {
                case FormulaToken.MINUS:
                {
                    return (double) left - (double) right;
                }
                case FormulaToken.PLUS:
                {
                    return (double) left + (double) right;
                }
                case FormulaToken.TIMES:
                {
                    return (double) left * (double) right;
                }
                case FormulaToken.DIV:
                {
                    return (double) left / (double) right;
                }
                case FormulaToken.LT:
                {
                    return comparison < 0;
                }
                case FormulaToken.LTE:
                {
                    return comparison <= 0;
                }
                case FormulaToken.GT:
                {
                    return comparison > 0;
                }
                case FormulaToken.GTE:
                {
                    return comparison >= 0;
                }
                case FormulaToken.EQ:
                {
                    return comparison == 0;
                }
                case FormulaToken.NEQ:
                {
                    return comparison != 0;
                }
                default:
                {
                    return null;
                }
            }
        }

        private void Evaluate(Vertex vertex)
        {
            object value = null;
            var formula = Formulae.Formulas.FirstOrDefault(x => x.Variable.Name == vertex.Name);
            if (formula != null) value = Evaluate(formula.Expression);
            State[vertex.Name] = value;
        }

        private object Evaluate(Number number)
        {
            return number.Value;
        }

        private object Evaluate(Boolean boolean)
        {
            return boolean.Value;
        }

        private object Evaluate(Variable variable)
        {
            if (State.ContainsKey(variable.Name)) return State[variable.Name];

            return null;
        }

        private object Evaluate(IExpression expression)
        {
            return expression switch
            {
                UnaryExpression unary => Evaluate(unary),
                BinaryExpression binary => Evaluate(binary),
                Variable variable => Evaluate(variable),
                Number number => Evaluate(number),
                Boolean boolean => Evaluate(boolean),
                _ => null
            };
        }

        private object Evaluate(UnaryExpression unary)
        {
            var val = Evaluate(unary.Expression);
            switch (unary.Operation)
            {
                case FormulaToken.MINUS:
                {
                    return -(double) val;
                }
                case FormulaToken.NOT:
                {
                    return !(bool) val;
                }
                default:
                {
                    return null;
                }
            }
        }

        #endregion
    }
}