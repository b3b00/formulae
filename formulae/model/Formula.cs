namespace formulae.model
{
    public class Formula : IFormula
    {
        public Formula(Variable variable, IExpression expression)
        {
            Variable = variable;
            Expression = expression;
        }

        public Variable Variable { get; set; }

        public IExpression Expression { get; set; }

        public FormulaType Type { get; set; }
    }
}