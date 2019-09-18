using FlooringMastery.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class ProductStateManager
    {

        public List<Products> DisplayProduct()
        {
            List<Products> products = ProductRepository.GetProducts();
            return products;
        }

        public List<Tax> DisplayTax()
        {
            List<Tax> tax = StateRepository.GetTaxes();
            return tax;
        }
    }
}
