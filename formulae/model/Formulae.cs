using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public string Dump()
        {
            StringBuilder b = new StringBuilder();
            foreach (var formula in Formulas)
            {
                b.AppendLine(formula.Dump());
            }

            return b.ToString();
        }
    }
}