using System.Collections.Generic;
using System.Linq;

namespace formulae.model
{
    public class Formulae : IFormula
    {
        public List<Assignment> Formulas { get; set; }

        public Formulae(List<IFormula> formulas)
        {
            Formulas = formulas.Cast<Assignment>().ToList();
        }
        
        public FormulaType Type { get; set; }
    }
}