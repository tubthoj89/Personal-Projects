
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class Orders 
    {
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string State { get; set; }
        public decimal TaxRate { get; set; }
        public string ProductType { get; set; }
        public decimal Area { get; set; }
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }        
        public DateTime dateTime { get; set; }

        public decimal MaterialCost
        {
            get
            {
                return decimal.Round(Area * CostPerSquareFoot,2);
            }
        }

        public decimal LaborCost
        {
            get
            {
               return decimal.Round(Area * LaborCostPerSquareFoot,2);
            }
        }

        public decimal Tax
        {
            get
            {
                return decimal.Round((MaterialCost + LaborCost) * (TaxRate / 100),2);
            }
        }

        public decimal Total
        {
            get
            {
                return MaterialCost + LaborCost + Tax;
            }
        }
    }
}
