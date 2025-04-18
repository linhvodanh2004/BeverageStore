using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.DAL
{
    public class OrderDAO
    {
        private BeverageStoreContext _context;
        public OrderDAO()
        {
            _context = new BeverageStoreContext();
        }
        public List<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Seat)
                .ToList();
        }
        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Seat)
                .FirstOrDefault(o => o.Id == id);
        }
        public bool AddOrder(Order order)
        {
            if (GetOrderById(order.Id) == null)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateOrder(Order order)
        {
            if (GetOrderById(order.Id) != null)
            {
                _context.Orders.Update(order);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
