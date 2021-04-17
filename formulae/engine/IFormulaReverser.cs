using formulae.model;

namespace formulae.engine
{
    public interface IFormulaReverser
    {
        Formula Reverse(Formula formula, Variable variable);
    }
}