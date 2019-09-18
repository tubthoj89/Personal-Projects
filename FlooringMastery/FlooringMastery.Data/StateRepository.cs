using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public static class StateRepository
    {
        public static List<Tax> GetTaxes()
        {
            List<Tax> taxes = new List<Tax>();
            try
            {
                using (StreamReader streamReader = new StreamReader("Tax.txt"))
                {
                    string row = streamReader.ReadLine();
                    while ((row = streamReader.ReadLine()) != null)
                    {
                        Tax tax = TaxMapper.ToTax(row);
                        taxes.Add(tax);
                    }                    
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Not valid");
            }
            return taxes;
        }
    }
}
