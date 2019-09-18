using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class TaxMapper
    {
        public static Tax ToTax(string row)
        {
            Tax t = new Tax();
            string[] fields = row.Split(',');
            t.StateAbbreviation = fields[0];
            t.StateName = fields[1];
            t.TaxRate = decimal.Parse(fields[2]);
            return t;
        }

        public static string ToCSV(Tax tax)
        {
            string row = $"{tax.StateAbbreviation},{tax.StateName},{tax.TaxRate}";
            return row;
        }
    }
}
