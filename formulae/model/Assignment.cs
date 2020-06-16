namespace formulae.model
{
    public class Assignment : IFormula
    {
        public Variable Variable { get; set; }
        
        public IExpression Expression { get; set; }

        public Assignment(Variable variable, IExpression expression)
        {
            Variable = variable;
            Expression = expression;
        }
        
        public FormulaType Type { get; set; }
    }
}