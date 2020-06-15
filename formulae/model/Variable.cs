namespace formulae.model
{
    public class Variable : IExpression
    {
        public string Name { get; set; }

        public Variable(string name)
        {
            Name = name;
        }
    }
} 