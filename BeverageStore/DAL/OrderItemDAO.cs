using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.DAL
{
    public class OrderItemDAO
    {
        private BeverageStoreContext _context;
        public OrderItemDAO()
        {
            _context = new BeverageStoreContext();
        }
        public List<OrderItem> GetOrderItemByOrderId(int OrderId)
        {
            return _context.OrderItems
                .Include(o => o.Beverage)
                .Include(o => o.Order)
                .Where(o => o.OrderId == OrderId).ToList();
        }
        public OrderItem GetOrderItemById(int id)
        {
            return _context.OrderItems
                .Include(o => o.Beverage)
                .Include(o => o.Order)
                .FirstOrDefault(o => o.Id == id);
        }
        public bool AddOrderItem(OrderItem orderItem)
        {
            if (GetOrderItemById(orderItem.Id) == null)
            {
                _context.OrderItems.Add(orderItem);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateOrderItem(OrderItem orderItem)
        {
            if (GetOrderItemById(orderItem.Id) != null)
            {
                _context.OrderItems.Update(orderItem);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
