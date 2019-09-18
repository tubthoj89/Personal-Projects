using FlooringMastery.UI.WorkFlows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public class Menu
    {
        public static void Run()
        {
            bool keeprunning = true;
            while (keeprunning)
            {
                DisplayMenu();
                keeprunning = Choice();
            }
        }
        
        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Floor Mastery");
            Console.WriteLine(UserIO.Separator);
            Console.WriteLine("1. Display All Orders");
            Console.WriteLine("2. Add An Order");
            Console.WriteLine("3. Edit An Order");
            Console.WriteLine("4. Remove An Order");

            Console.WriteLine("\nQ to Quit!");
            Console.WriteLine(UserIO.Separator);
            Console.WriteLine("\nEnter Choice:");
        }

        private static bool Choice()
        {
            string userinput = Console.ReadLine();

            switch (userinput)
            {
                case "1":
                    DisplayOrderWorkFlow displayOrder = new DisplayOrderWorkFlow();
                    displayOrder.Execute();
                    break;
                case "2":
                    AddOrderWorkFlow addOrder = new AddOrderWorkFlow();
                    addOrder.Execute();
                    break;
                case "3":
                    EditOrderWorkFlow editOrder = new EditOrderWorkFlow();
                    editOrder.Execute();
                    break;
                case "4":
                    RemoveOrderWorkFlow removeOrder = new RemoveOrderWorkFlow();
                    removeOrder.Execute();
                    break;
                case "Q":
                    return false;
                default:
                    Console.WriteLine("Try again!!!!");
                    Console.WriteLine("Press Enter to continue....");
                    Console.ReadKey();
                    break;
            }
            return true;
        }
    }
}
