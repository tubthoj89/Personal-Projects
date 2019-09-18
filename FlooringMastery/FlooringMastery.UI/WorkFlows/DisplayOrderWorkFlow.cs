using FlooringMastery.BLL;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI.WorkFlows
{
    public class DisplayOrderWorkFlow
    {
        public void Execute()
        {
            bool keepRunning = true;
            do
            {
                Console.Clear();
                Manager order = ManagerFactory.Create();
                Console.WriteLine("Displaying an Order");
                Console.WriteLine(UserIO.Separator);
                Console.WriteLine("Please enter a Date:");
                string userInput = "MM/dd/yyyy";
                DateTime date = DateTime.Parse("06-01-2013");
                if (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.WriteLine("Please enter format in {0}", userInput);
                    Console.WriteLine("Press any key to continue....");
                    Console.ReadKey();
                    return;
                }
                DisplayResponse response = order.DisplayOrder(date);
                if (response.Success)
                {
                    UserIO.DisplayOrderDetail(response.Orders);
                    Console.ReadKey();
                }
                return;                
            } while (keepRunning);
        }
    }
}
