using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.DAL
{
    public class SeatDAO
    {
        private BeverageStoreContext _context;
        public SeatDAO()
        {
            _context = new BeverageStoreContext();
        }
        public List<Seat> GetSeatList()
        {
            return _context.Seats
                .Include(s => s.Orders)
                .ToList();
        }
        public Seat GetSeatById(int id)
        {
            return _context.Seats
                .Include(s => s.Orders)
                .FirstOrDefault(s => s.Id == id);
        }
        public bool AddSeat(Seat seat)
        {
            if (GetSeatById(seat.Id) == null)
            {
                _context.Seats.Add(seat);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool RemoveSeat(Seat seat)
        {
            if (GetSeatById(seat.Id) != null)
            {
                _context.Seats.Remove(seat);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateSeat(Seat seat)
        {
            if (GetSeatById(seat.Id) != null)
            {
                _context.Seats.Update(seat);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
