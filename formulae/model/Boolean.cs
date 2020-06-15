namespace formulae.model
{
    public class Boolean : IExpression
    {
        
        public bool Value { get; set; }
        public Boolean(bool value)
        {
            value = value;
        }
    }
}