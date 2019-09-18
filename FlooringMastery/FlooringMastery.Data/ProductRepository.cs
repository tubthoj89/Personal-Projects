using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public static class ProductRepository
    {
        public static List<Products> GetProducts()
        {
            List<Products> product = new List<Products>();
            try
            {
                using (StreamReader streamReader = new StreamReader("Product.txt"))
                {
                    string row = streamReader.ReadLine();
                    while ((row = streamReader.ReadLine()) != null)
                    {
                        Products products = ProductMapper.ToProduct(row);
                        product.Add(products);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Not valid");
            }
            return product;
        }
    }
}
