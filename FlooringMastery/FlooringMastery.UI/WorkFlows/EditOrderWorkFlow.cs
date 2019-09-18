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
    public class EditOrderWorkFlow
    {
        public Orders order = new Orders();
        Manager orderManager = ManagerFactory.Create();
        UserIO io = new UserIO();

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("Edit Orders");
            Console.WriteLine(UserIO.Separator);
            order.dateTime = io.RemoveEditGetDate();
            List<Orders> orders = orderManager.DisplayOrder(order.dateTime).Orders;
            order = io.EditGettingOrderNumber(orders);
            if (order != null)
            {
                order = io.EditOrder(order);
                if (order != null)
                {
                    EditResponse response = orderManager.EditOrder(order);
                    if (response.Success)
                    {
                        Console.Clear();
                        UserIO.DisplayForCustomer(response.Order);
                        Console.ReadKey();
                        
                    }
                }
            }
        }
    }
}
