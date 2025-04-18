using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.DAL
{
    public class IngredientDAO
    {
        private BeverageStoreContext _context;
        public IngredientDAO()
        {
            _context = new BeverageStoreContext();
        }

        public List<Ingredient> GetAllIngredients()
        {
            return _context.Ingredients
                .Include(i => i.Beverages)
                .ToList();
        }

        public Ingredient GetIngredientById(int id)
        {
            return _context.Ingredients
                .Include(i => i.Beverages)
                .FirstOrDefault(i => i.Id == id);
        }
      
        public bool AddIngredient(Ingredient ingredient)
        {
            if (GetIngredientById(ingredient.Id) == null)
            {
                _context.Ingredients.Add(ingredient);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateIngredient(Ingredient ingredient)
        {
            if (GetIngredientById(ingredient.Id) != null)
            {
                _context.Ingredients.Update(ingredient);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
