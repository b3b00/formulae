using System.Collections.Generic;
using System.Linq;

namespace formulae.model
{
    public class Formulae : IFormula
    {
        public Formulae(List<IFormula> formulas)
        {
            Formulas = formulas.Cast<Assignment>().ToList();
        }

        public List<Assignment> Formulas { get; set; }

        public FormulaType Type { get; set; }
    }
}