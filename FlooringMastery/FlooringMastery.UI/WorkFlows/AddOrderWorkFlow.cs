using FlooringMastery.BLL;
using FlooringMastery.Data;
using FlooringMastery.Models.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI.WorkFlows
{
    public class AddOrderWorkFlow
    {
        public Orders order = new Orders();
        public Tax tax = new Tax();
        public Products product = new Products();
        Manager orderManager = ManagerFactory.Create();
        UserIO io = new UserIO();

        public void Execute()
        { 
            Console.Clear();
            Console.WriteLine("Adding Orders");
            Console.WriteLine("Please fill out the Information");
            Console.WriteLine(UserIO.Separator);
            order.dateTime = io.AddGetDate();
            order.CustomerName = io.GettingName();
            tax = io.GettingState();
            product = io.GettingProduct();
            order.Area = io.GettingArea();           
            io.AddAssigningValue(order);            
            UserIO.DisplayForCustomer(order);
            order = io.AddingOrders(order);
            if (order != null)
            {
                AddResponse response = orderManager.AddOrder(order.dateTime, order);
                if (response.Success)
                {
                    Console.WriteLine("Order have been process");
                    Console.ReadKey();
                }
            }
        }
    }
}

