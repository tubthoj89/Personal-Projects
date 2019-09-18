using FlooringMastery.Data;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class Manager
    {
        private IOrderRepository _orderRepository;
        public Manager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public DisplayResponse DisplayOrder(DateTime date)
        {
            DisplayResponse response = new DisplayResponse()
            {
                Orders = _orderRepository.LoadOrders(date)
            };

            if (response.Orders == null)
            {
                response.Success = false;
                response.Message = $"{date} is not a valid date";
                Console.ReadKey();
                return response;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public EditResponse EditOrder(Orders order)
        {
            EditResponse response = new EditResponse();
        

            if (_orderRepository.UpdateOrder(order))
            {
                response.Success = true;
                response.Order = order;
            }
            else
            {
                response.Success = false;
                response.Message = "Not valid";
            }
            return response;
        }

        public RemoveResponse DeletOrder(Orders order)
        {
            RemoveResponse response = new RemoveResponse();

            if (_orderRepository.DeleteOrder(order))
            {
                response.Success = true;
                response.Order = order;
            }
            else
            {
                response.Success = false;
                response.Message = "Not valid";
            }
            return response;
        }

        public AddResponse AddOrder(DateTime date, Orders order)
        {
            AddResponse response = new AddResponse();
            response.Order = _orderRepository.CreateOrder(date, order);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = "Not valid";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
    }
}
