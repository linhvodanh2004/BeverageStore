using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.DAL
{
    public class BeverageDAO
    {
        private BeverageStoreContext _context;
        public BeverageDAO()
        {
            _context = new BeverageStoreContext();
        }
        public List<Beverage> GetAllBeverages()
        {
            return _context.Beverages
                .Include(b => b.Category)
                .Include(b => b.Ingredients)
                .Include(b => b.OrderItems)
                .ToList();
        }
        public List<Beverage> GetBeveragesByCategoryName(String category)
        {
            var categoryId = _context.Categories
                .Where(c => c.Name == category)
                .Select(c => c.Id)
                .FirstOrDefault();
            
            var beverage = _context.Beverages
                .Where(b => b.CategoryId == categoryId)
                .Include(b => b.Category)
                .Include(b => b.Ingredients)
                .Include(b => b.OrderItems)
                .ToList();
            return beverage;
        }
        public List<Beverage> GetBeveragesByName(string searchString)
        {
            return _context.Beverages
                .Where(b => b.Name.Contains(searchString))
                .Include(b => b.Category)
                .Include(b => b.Ingredients)
                .Include(b => b.OrderItems)
                .ToList();
        }
        public Beverage GetBeverageById(int id)
        {
            return _context.Beverages.Include(b => b.Category)
                .Include(b => b.Ingredients)
                .Include(b => b.OrderItems).FirstOrDefault(b => b.Id == id);
        }
        public bool AddBeverage(Beverage beverage)
        {
            if (GetBeverageById(beverage.Id) == null)
            {
                _context.Beverages.Add(beverage);
                _context.SaveChanges();
                return true;
            }
            return false;

        }
        public bool UpdateBeverage(Beverage beverage)
        {
            if (GetBeverageById(beverage.Id) != null)
            {
                _context.Beverages.Update(beverage);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
