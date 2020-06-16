namespace formulae.model
{
    public class Assignment : IFormula
    {
        public Assignment(Variable variable, IExpression expression)
        {
            Variable = variable;
            Expression = expression;
        }

        public Variable Variable { get; set; }

        public IExpression Expression { get; set; }

        public FormulaType Type { get; set; }
    }
}