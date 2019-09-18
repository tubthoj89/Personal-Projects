using FlooringMastery.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class ManagerFactory
    {
        public static Manager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode)
            {
                case "Order":
                    return new Manager(new OrderRepository("Orders_06012013.txt"));
                case "Test":
                    return new Manager(new OrderTestRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");

            }
        }
    }
}
