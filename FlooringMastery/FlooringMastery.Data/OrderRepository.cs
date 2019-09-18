using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class OrderRepository : IOrderRepository
    {
        public string FileName;
        public OrderRepository(string filename)
        {
            FileName = filename;
        }
        public List<Orders> orders = new List<Orders>();
        public Orders order = new Orders();
        public DateTime dates = new DateTime();
        public Tax tax = new Tax();
        public Products product = new Products();

        public List<Orders> LoadOrders(DateTime date)//question about edit and remove load cause it just create one if it doesn't exist
        {
            List<Orders> orders = new List<Orders>();
            FileName = "Orders_" + date.Month.ToString("d2") + date.Day.ToString("d2") + date.Year + ".txt";
            
            if (!File.Exists(FileName))
            {
                File.Create(FileName).Close();                
                return orders;
            }
            using (StreamReader streamReader = new StreamReader(FileName))
                {                    
                    string row = streamReader.ReadLine();
                    while ((row = streamReader.ReadLine()) != null)
                    {
                        Orders o = OrdersMapper.ToOrder(row);
                        o.dateTime = date;
                        orders.Add(o);
                    }
                }
                       
            return orders;
        }

        public void SaveAllOrders(DateTime date, List<Orders> orders)
        {
            string FileName = "Orders_" + date.Month.ToString("d2") + date.Day.ToString("d2") + date.Year + ".txt";
            using (StreamWriter streamWriter = new StreamWriter(FileName))
            {
                streamWriter.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                foreach (Orders custOrder in orders)
                {
                    streamWriter.WriteLine(OrdersMapper.ToStringCSV(custOrder));
                }
            }
        }

        public Orders CreateOrder(DateTime date, Orders order)
        {
            var orders = LoadOrders(date);
            int number = 0;
            foreach (Orders o in orders)
            {
                if (o.OrderNumber > number)
                {
                    number = o.OrderNumber;
                }
            }
            number += 1;

            order.OrderNumber = number;
            order.dateTime = date;
            orders.Add(order);
            SaveAllOrders(date, orders);
            return order;
        }

        public bool UpdateOrder(Orders order)
        {
         
            List<Orders> orders = LoadOrders(order.dateTime);
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].OrderNumber == order.OrderNumber)
                {
                     orders[i] = order;
                     SaveAllOrders(order.dateTime, orders);
                    return true;
                } 
            }
            return false;
        }

        public bool DeleteOrder(Orders order)
        {
            List<Orders> orders = LoadOrders(order.dateTime);
            if (orders != null)
            {
                orders.RemoveAll(o => o.OrderNumber == order.OrderNumber);
                SaveAllOrders(order.dateTime, orders);
                return true;
            }
            return false;
        }
    }
}
