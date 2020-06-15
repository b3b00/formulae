using System.Collections.Generic;

namespace formulae.model
{
    public class Formulae : IFormula
    {
        public List<IFormula> Formulas { get; set; }

        public Formulae(List<IFormula> formulas)
        {
            Formulas = formulas;
        }
    }
}