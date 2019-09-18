using FlooringMastery.BLL;
using FlooringMastery.Data;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Response;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryTest
{
    [TestFixture]
    public class Test
    {
        OrderTestRepository repo = new OrderTestRepository();
        DateTime date = new DateTime();
        Orders order = new Orders();
        Manager manager = new Manager(new OrderTestRepository());

        [Test]
        public void CanLoadList()//run all together than wont passed
        {
            repo = new OrderTestRepository();

            List<Orders> orders = repo.LoadOrders(date);

            Assert.AreEqual(2, orders.Count);

            Orders check = orders.FirstOrDefault(o => o.OrderNumber == 1);

            Assert.AreEqual(1, check.OrderNumber);
            Assert.AreEqual("Wise", check.CustomerName);
            Assert.AreEqual("OH", check.State);
            Assert.AreEqual(6.25, check.TaxRate);
            Assert.AreEqual("Wood", check.ProductType);
            Assert.AreEqual(100, check.Area);
            Assert.AreEqual(5.15, check.CostPerSquareFoot);
            Assert.AreEqual(4.75, check.LaborCostPerSquareFoot);
            Assert.AreEqual(515.00, check.MaterialCost);
            Assert.AreEqual(475, check.LaborCost);
            Assert.AreEqual(61.88, check.Tax);
            Assert.AreEqual(1051.88, check.Total);
        }

        [Test]
        public void CheckMapper()
        {
             Orders order = new Orders()
             {
                 OrderNumber = 1,
                 CustomerName = "Wise",
                 State = "OH",
                 TaxRate = 6.25m,
                 ProductType = "Wood",
                 Area = 100.00m,
                 CostPerSquareFoot = 5.15m,
                 LaborCostPerSquareFoot = 4.75m
             };

            string check = "1,Wise,OH,6.25,Wood,100.00,5.15,4.75,515.00,475.00,61.88,1051.88";

            string expected = OrdersMapper.ToStringCSV(order);

            Assert.AreEqual(check, expected);
        }
        [Test]
        public void CanAdd()
        {
            repo.CreateOrder(date,order);

            List<Orders> orders = repo.LoadOrders(date);

            Assert.AreEqual(3, orders.Count);

            Orders check = orders.FirstOrDefault(o => o.OrderNumber == 2);

            Assert.AreEqual(2, check.OrderNumber);
            Assert.AreEqual("Tou", check.CustomerName);
            Assert.AreEqual("IN", check.State);
            Assert.AreEqual(6.25, check.TaxRate);
            Assert.AreEqual("Tile", check.ProductType);
            Assert.AreEqual(100, check.Area);
            Assert.AreEqual(5.15, check.CostPerSquareFoot);
            Assert.AreEqual(4.75, check.LaborCostPerSquareFoot);
            Assert.AreEqual(515.00, check.MaterialCost);
            Assert.AreEqual(475, check.LaborCost);
            Assert.AreEqual(61.88, check.Tax);
            Assert.AreEqual(1051.88, check.Total);

            repo.DeleteOrder(order);
        }

        [Test]
        public void CanDelete()
        {
            repo = new OrderTestRepository();
            Orders order = new Orders()
            {
                OrderNumber = 2
            };

            repo.DeleteOrder(order);

            List<Orders> orders = repo.LoadOrders(date);

            Assert.AreEqual(1, orders.Count);

            Orders check = orders.FirstOrDefault(o => o.OrderNumber == 1);

            Assert.AreEqual(1, check.OrderNumber);
            Assert.AreEqual("Wise", check.CustomerName);
            Assert.AreEqual("OH", check.State);
            Assert.AreEqual(6.25, check.TaxRate);
            Assert.AreEqual("Wood", check.ProductType);
            Assert.AreEqual(100, check.Area);
            Assert.AreEqual(5.15, check.CostPerSquareFoot);
            Assert.AreEqual(4.75, check.LaborCostPerSquareFoot);
            Assert.AreEqual(515.00, check.MaterialCost);
            Assert.AreEqual(475, check.LaborCost);
            Assert.AreEqual(61.88, check.Tax);
            Assert.AreEqual(1051.88, check.Total);

            repo.CreateOrder(date, order);
        }
        
        [Test]
        public void LoadResponse()
        {
            DateTime date = new DateTime(2020, 06, 06);

            List<Orders> orders = repo.LoadOrders(date);

            DisplayResponse response = manager.DisplayOrder(date);

            Assert.IsNotNull(response.Orders);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(date, response.Orders.FirstOrDefault(o => o.dateTime == date).dateTime);
        }

        [Test]
        public void CanEdit()
        {
            repo = new OrderTestRepository();
            List<Orders> orders = repo.LoadOrders(date);

            Orders order = orders[0];
            order.CustomerName = "Tom";
            order.State = "IN";
            
            repo.UpdateOrder(order);
            Assert.AreEqual(2, orders.Count);

            orders = repo.LoadOrders(date);
            Orders check = orders.FirstOrDefault(o => o.OrderNumber == 1);

            Assert.AreEqual(1, check.OrderNumber);
            Assert.AreEqual("Tom", check.CustomerName);
            Assert.AreEqual("IN", check.State);
            Assert.AreEqual(6.25, check.TaxRate);
            Assert.AreEqual("Wood", check.ProductType);
            Assert.AreEqual(100, check.Area);
            Assert.AreEqual(5.15, check.CostPerSquareFoot);
            Assert.AreEqual(4.75, check.LaborCostPerSquareFoot);
            Assert.AreEqual(515.00, check.MaterialCost);
            Assert.AreEqual(475, check.LaborCost);
            Assert.AreEqual(61.88, check.Tax);
            Assert.AreEqual(1051.88, check.Total);
        }

        [TestCase(1, true)]
        [TestCase(3, false)]
        public void ResponseDelete(int orderNumber, bool expectedResult)
        {
            Orders order = new Orders()
            {
                dateTime = new DateTime(2020, 06, 06),
                OrderNumber = orderNumber,
                CustomerName = "Wise",
                State = "OH",
                TaxRate = 6.25m,
                ProductType = "Wood",
                Area = 100.00m,
                CostPerSquareFoot = 5.15m,
                LaborCostPerSquareFoot = 4.75m
            };

            RemoveResponse response = manager.DeletOrder(order);

            Assert.AreEqual(expectedResult, response.Success);

            if(response.Success == true)
            {
                repo.CreateOrder(new DateTime(2020, 06, 06), order);
            }
        }

        [Test]
        public void CheckDate()
        {
            DateTime date = new DateTime(2020, 06, 06);

            List<Orders> orders = repo.LoadOrders(date);
            
            Assert.IsNotNull(orders);
            Assert.AreEqual(date, orders.FirstOrDefault(o => o.dateTime == date).dateTime);
        }

        [TestCase(1, false)]
        [TestCase(3, true)]
        public void ResponseAdd(int orderNumber, bool expectedResult)
        {
            Orders order = new Orders()
            {
                dateTime = new DateTime(2020, 06, 06),
                OrderNumber = orderNumber,
                CustomerName = "Wise",
                State = "OH",
                TaxRate = 6.25m,
                ProductType = "Wood",
                Area = 100.00m,
                CostPerSquareFoot = 5.15m,
                LaborCostPerSquareFoot = 4.75m
            };
            
            if (order.OrderNumber != orderNumber)
            {
                AddResponse response = manager.AddOrder(new DateTime(2020, 06, 06), order);
                Assert.AreEqual(expectedResult, response.Success);

                if (response.Success == true)
                {
                    repo.DeleteOrder(order);
                }
            }
            
        }
    }
}
