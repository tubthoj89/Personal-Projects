using FlooringMastery.BLL;
using FlooringMastery.Data;
using FlooringMastery.Models.Response;
using FlooringMastery.UI.WorkFlows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public class UserIO
    {
        public const string Separator = "===========================================================================================";       
        public DateTime date = new DateTime();
        public string stateName = "";
        public string name = "";
        public string productType = "";
        public decimal area = 0;
        public Tax tax = new Tax();
        public Products product = new Products();
        ProductStateManager orderManager = new ProductStateManager();
        public int orderNumber = 0;

        public Orders EditOrder(Orders order)//ordering data missing coming here and deleted order for parameter
        {
            Console.Clear();
            Console.WriteLine($"Name {order.CustomerName}:");
            Console.WriteLine("Press Enter to skip or Enter anything to name change.");
            var newName = Console.ReadLine();
            if (newName != "")
            {
                order.CustomerName = GettingName();
            }
            else
            {
                newName = order.CustomerName;
            }
            Console.WriteLine($"State {order.State}:");
            Console.WriteLine("Press Enter to skip or Enter anything to state change.");
            string newState = Console.ReadLine();
            if (newState != "")
            {
                tax = GettingState();
                order.State = tax.StateAbbreviation;
                order.TaxRate = tax.TaxRate;
                //tax = GettingState();
            }
            else
            {
                newState = tax.ToString();
            }
            Console.WriteLine($"Product {order.ProductType}:");
            Console.WriteLine("Press Enter to skip or Enter anything to product change.");
            string newProductType = Console.ReadLine();
            if (newProductType != "")
            {
                product = GettingProduct();
                order.ProductType = product.ProductType;
                order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
                order.CostPerSquareFoot = product.CostPerSquareFoot;
            }
            else
            {
                newProductType = product.ToString();
            }
            Console.WriteLine($"Area {order.Area}:");
            Console.WriteLine("Press Enter to skip or Enter anything to area change.");
            var newUserArea = Console.ReadLine();
            if (newUserArea != "")
            {
                order.Area = GettingArea();
            }
            else
            {
                newUserArea = order.Area.ToString();
            }
            DisplayForCustomer(order);

            bool keepRunning = true;
            string answer;
            do
            {
                Console.WriteLine("Answer Y/N to edit order");
                answer = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(answer))
                {
                    Console.WriteLine("Must be (Y/N)");
                    Console.ReadKey();
                    continue;
                }
                if (answer != "N" && answer != "Y")
                {
                    Console.WriteLine("Must be (Y/N)");
                    Console.ReadKey();
                    continue;
                }
                if (answer == "N")
                {
                    keepRunning = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Order have been process");
                    Console.ReadKey();
                    return order;
                }
            } while (keepRunning);
            return null;
        }

        public DateTime RemoveEditGetDate()
        {
            bool keepRunning = true;
            do
            {
                Console.Write("Order Date:");
                string userDate = "MM/dd/yyyy";
                DateTime.TryParse(userDate, out date);
                if (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.WriteLine("Please enter format in {0}", userDate);
                    Console.WriteLine("Press any key to continue....");
                    Console.ReadKey();
                    continue;
                }
                if (date.Year <= 2012)
                {
                    Console.WriteLine("Date need to be future");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    keepRunning = false;
                }
            } while (keepRunning);
            return date;
        }

        public static void DisplayOrderDetail(List<Orders> orders)
        {
            foreach (var item in orders)
            {
                DisplayForCustomer(item);
            }
        }
        
        public static void DisplayForCustomer(Orders Order)
        {

            Console.WriteLine(Separator);
            Console.WriteLine($"OrderNumber: {Order.OrderNumber}");
            Console.WriteLine($"CustomerName: {Order.CustomerName}");
            Console.WriteLine($"State: {Order.State}");
            Console.WriteLine($"Product: {Order.ProductType}");
            Console.WriteLine($"Materials: {Order.MaterialCost:c}");
            Console.WriteLine($"Labor: {Order.LaborCost:c}");
            Console.WriteLine($"Tax: {Order.Tax:c}");
            Console.WriteLine($"Total: {Order.Total:c}");

        }

        public Orders AddAssigningValue(Orders o)
        {
            o.Area = area;
            o.CustomerName = name;
            o.ProductType = productType;
            o.State = stateName;
            o.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
            o.TaxRate = tax.TaxRate;
            o.CostPerSquareFoot = product.CostPerSquareFoot;
            return o;
        }

        public Orders AddingOrders(Orders order)
        {
            bool keepRunning = true;
            string answer;
            do
            {
                
                Console.WriteLine("Answer Y/N to process order");
                answer = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(answer))
                {
                    Console.WriteLine("Must be (Y/N)");
                    Console.ReadKey();
                    continue;
                }
                if (answer != "N" && answer != "Y")
                {
                    Console.WriteLine("Must be (Y/N)");
                    Console.ReadKey();
                    continue;
                }
                if (answer == "N")
                {
                    Console.WriteLine("Order not place");
                    Console.ReadKey();
                    return null;
                }
                else
                {
                    keepRunning = false;
                }
            } while (keepRunning);
            return order;
        }

        public decimal GettingArea()
        {
            bool keepRunning = true;
            do
            {
                Console.Clear();
                Console.Write("Area:");
                decimal.TryParse(Console.ReadLine(), out area);
                if (area < 100)
                {
                    Console.WriteLine("Our minimum is 100m");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    keepRunning = false;
                }
            } while (keepRunning);
            return area;
        }

        public Products GettingProduct()//need validation
        {
            bool keepRunning = true;
            do
            {
                Console.Clear();
                List<Products> products = orderManager.DisplayProduct();
                string line = "{0,-15}{1,-15}{2,15}";
                Console.WriteLine(line, "Product Type", "Cost Per Square Foot", "Labor Cost Per Square Foot");
                Console.WriteLine(UserIO.Separator);
                foreach (var item in products)
                {
                    Console.WriteLine(line, item.ProductType, item.CostPerSquareFoot, item.LaborCostPerSquareFoot);
                }

                Console.WriteLine(UserIO.Separator);
                Console.Write("Choice:");
                productType = Console.ReadLine();

                foreach (var items in products)
                {
                    if (items.ProductType == productType)
                    {
                        product = items;
                        keepRunning = false;
                        return product;
                    }
                }
                if (productType != product.ProductType)
                {
                    Console.WriteLine("Not Valid");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }
            } while (keepRunning);
            return null;
        }

        public Tax GettingState()
        {
            bool keepRunning = true;
            do
            {
                Console.Clear();
                List<Tax> taxes = orderManager.DisplayTax();
                Console.WriteLine("State");
                Console.WriteLine(UserIO.Separator);

                foreach (var item in taxes)
                {
                    Console.WriteLine($"{item.StateAbbreviation},{item.StateName}");
                }

                Console.WriteLine(UserIO.Separator);
                Console.Write("Choice:");
                stateName = Console.ReadLine();

                foreach (var items in taxes)
                {
                    if (items.StateAbbreviation == stateName || items.StateName == stateName)
                    {
                        tax = items;
                        keepRunning = false;
                        return tax; 
                    }
                }
                if (stateName != tax.StateName && stateName != tax.StateName)
                {
                    Console.WriteLine("State not available or Spelling is Wrong");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }
            } while (keepRunning);
            return null;
        }

        public string GettingName()
        {
            bool keepRunning = true;
            do
            {
                Console.Clear();
                Console.Write("Name:");
                name = Console.ReadLine();
                bool result = name.All(n => Char.IsLetterOrDigit(n) || n == ' ' || n == ',' || n == '.');
                if (result == false)
                {
                    Console.WriteLine("Sorry those input is not valid");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    keepRunning = true;
                    continue;
                }
                else
                {
                    keepRunning = false;
                    break;
                }
            } while (keepRunning);
            return name;
        }

        public DateTime AddGetDate()
        {
            bool keepRunning = true;
            do
            {
                Console.Write("Order Date:");
                string userDate = "MM/dd/yyyy";
                DateTime.TryParse(userDate, out date);
                if (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.WriteLine("Please enter format in {0}", userDate);
                    Console.WriteLine("Press any key to continue....");
                    Console.ReadKey();
                    continue;
                }
                if (date <= DateTime.Now)
                {
                    Console.WriteLine("Date need to be future");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    keepRunning = false;
                    break;
                }
            } while (keepRunning);
            return date;
        }

        public Orders EditGettingOrderNumber(List<Orders> orders)
        {
            bool keepRunning = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Please enter your order number.");
                Console.WriteLine(UserIO.Separator);
                Console.Write("#OrderNumber:");
                if (!int.TryParse(Console.ReadLine(), out orderNumber))
                {
                    Console.WriteLine("Needs to be a number.");
                    Console.ReadKey();
                    continue;
                }
                if (orderNumber < 1)
                {
                    Console.Write("Cannot be 0! Press any key to continue.....");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    return orders.FirstOrDefault(o => o.OrderNumber == orderNumber);

                }

            } while (keepRunning);
            return null;
        }

        public Orders DeleteGettingOrderNumer(List<Orders> orders)
        {
            bool keepRunning = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Please enter your order number.");
                Console.WriteLine(UserIO.Separator);
                Console.Write("#OrderNumber:");
                if (!int.TryParse(Console.ReadLine(), out orderNumber))
                {
                    Console.WriteLine("Need to be a number!");
                    Console.ReadKey();
                    continue;
                }
                if (orderNumber < 1)
                {
                    Console.Write("Cannot be 0! Press any key to continue.....");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    return orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
                }
            } while (keepRunning);
            return null;
        }

        public Orders DeletingOrder(Orders order)
        {
            bool keepRunning = true;
            string answer;
            do
            {
                Console.WriteLine("Answer Y/N to edit order.");
                answer = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(answer))
                {
                    Console.WriteLine("Must be (Y/N)");
                    Console.ReadKey();
                    continue;
                }
                if (answer != "N" && answer != "Y")
                {
                    Console.WriteLine("Must be (Y/N)");
                    Console.ReadKey();
                    continue;
                }
                if (answer == "N")
                {
                    Console.WriteLine("Order not deleted");
                    Console.ReadKey();
                    keepRunning = false;
                }
                else
                {
                    return order;
                }
            } while (keepRunning);
            return null;
        }

    }
}
