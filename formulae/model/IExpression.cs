using System.Collections.Generic;

namespace formulae.model
{
    public interface IExpression : IFormula
    {
        List<Variable> GetVariables();
    }
}