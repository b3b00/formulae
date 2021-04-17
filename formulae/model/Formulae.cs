using System.Collections.Generic;
using System.Linq;

namespace formulae.model
{
    public class Formulae : IFormula
    {
        public Formulae(List<IFormula> formulas)
        {
            Formulas = formulas.Cast<Formula>().ToList();
        }

        public List<Formula> Formulas { get; set; }

        public FormulaType Type { get; set; }
    }
}