using FlooringMastery.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Orders> LoadOrders(DateTime orderDate);
        void SaveAllOrders(DateTime date, List<Orders> orders);
        Orders CreateOrder(DateTime date, Orders order);
        bool UpdateOrder(Orders order);
        bool DeleteOrder(Orders order);
    }
}
