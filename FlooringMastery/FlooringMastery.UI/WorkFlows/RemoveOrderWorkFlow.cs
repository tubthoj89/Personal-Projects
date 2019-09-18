using FlooringMastery.BLL;
using FlooringMastery.Data;
using FlooringMastery.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI.WorkFlows
{
    public class RemoveOrderWorkFlow
    {
        public Orders order = new Orders();
        public DateTime date = new DateTime();
        public int orderNumber = 0;
        UserIO io = new UserIO();

        Manager manager = ManagerFactory.Create();

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("Removing Orders");
            Console.WriteLine(UserIO.Separator);
            order.dateTime = io.RemoveEditGetDate();
            List<Orders> orders = manager.DisplayOrder(order.dateTime).Orders;
            order = io.DeleteGettingOrderNumer(orders);
            if (order != null)
            {
                UserIO.DisplayForCustomer(order);
                order = io.DeletingOrder(order);
                if (order != null)
                {
                    RemoveResponse response = manager.DeletOrder(order);
                    if (response.Success)
                    {
                        Console.WriteLine("Order have been deleted.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Failure");
                        Console.WriteLine(response.Message);
                        Console.ReadKey();
                    }
                    
                }
            }
        }
    }
}

    

