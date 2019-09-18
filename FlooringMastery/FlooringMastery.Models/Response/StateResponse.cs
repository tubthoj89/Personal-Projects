using FlooringMastery.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Response
{
    public class StateResponse : Response
    {
        public List<Tax> StateNames{ get; set; }
        public decimal TaxRate { get; set; }
        public string StateAbbreviation { get; set; }
    }
}
