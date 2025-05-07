using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            context.ChangeTracker.Clear();

            foreach (var line in order.Lines)
            {
                context.Attach(line.Product);
            }

            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving order: {ex.Message}");

                throw;
            }
        }
    }
}