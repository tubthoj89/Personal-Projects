using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FlooringMastery.Data
{
    public class OrderTestRepository : IOrderRepository
    {
        List<Orders> orders;

        public OrderTestRepository()
        {


            orders = new List<Orders>()
            {
                Order1,
                Order2
            };
        }
        static DateTime date = new DateTime(2020, 06, 06);

        private Orders Order1 = new Orders()
        {
            dateTime = date,
            OrderNumber = 1,
            CustomerName = "Wise",
            State = "OH",
            TaxRate = 6.25m,
            ProductType = "Wood",
            Area = 100.00m,
            CostPerSquareFoot = 5.15m,
            LaborCostPerSquareFoot = 4.75m
        };
        private Orders Order2 = new Orders()
        {
            dateTime = date,
            OrderNumber = 2,
            CustomerName = "Tou",
            State = "IN",
            TaxRate = 6.25m,
            ProductType = "Tile",
            Area = 100.00m,
            CostPerSquareFoot = 5.15m,
            LaborCostPerSquareFoot = 4.75m,
        };


        public Orders CreateOrder(DateTime date, Orders order)
        {
            orders.Add(order);
            return order;
        }

        public bool DeleteOrder(Orders order)
        {
            return orders.RemoveAll(o => o.OrderNumber == order.OrderNumber) == 1;
        }

        public List<Orders> LoadOrders(DateTime orderDate)
        {
            return orders;
        }

        public void SaveAllOrders(DateTime date, List<Orders> orders)
        {
            this.orders = orders;
        }

        public bool UpdateOrder(Orders order)
        {
            bool results = DeleteOrder(order);
            CreateOrder(order.dateTime, order);
            return results;
        }
    }
}
