using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class ProductMapper
    {
        public static Products ToProduct(string row)
        {
            Products p = new Products();
            string[] fields = row.Split(',');
            p.ProductType = fields[0];
            p.CostPerSquareFoot = decimal.Parse(fields[1]);
            p.LaborCostPerSquareFoot = decimal.Parse(fields[2]);
            return p;
        }

        public static string ToProductCSV(Products products)
        {
            string row = $"{products.ProductType},{products.CostPerSquareFoot},{products.LaborCostPerSquareFoot}";
            return row;
        }
    }
}
