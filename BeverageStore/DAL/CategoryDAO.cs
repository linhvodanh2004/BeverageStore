using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.DAL
{
    public class CategoryDAO
    {
        private BeverageStoreContext _context;

        public CategoryDAO()
        {
            _context = new BeverageStoreContext();
        }
        public List<Category> GetAllBeverages()
        {
            return _context.Categories
                .Include(c => c.Beverages)
                .ToList();
        }
        public Category GetCategoryById(int id)
        {
            return _context.Categories
                .Include(c => c.Beverages)
                .FirstOrDefault(c => c.Id == id);
        }
        public bool AddCategory(Category category)
        {
            if (GetCategoryById(category.Id) == null)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return true;
            }
            return false;

        }
        public bool RemoveCategory(Category category)
        {
            var beverages = new BeverageDAO().GetBeveragesByCategoryName(category.Name);
            if (GetCategoryById(category.Id) != null
                && beverages == null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateCategory(Category category)
        {
            if (GetCategoryById(category.Id) != null)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
