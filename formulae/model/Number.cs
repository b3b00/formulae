namespace formulae.model
{
    public class Number : IExpression
    {
        public double Value { get; set; }

        public Number(double value)
        {
            Value = value;
        }
    }
}