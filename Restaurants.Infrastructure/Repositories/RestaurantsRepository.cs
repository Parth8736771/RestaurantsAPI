using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContext restaurantDbContext) : IRestaurantsRepository
    {
        public async Task<int> Create(Restaurant entity)
        {
            restaurantDbContext.Restaurants.Add(entity);
            await restaurantDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(Restaurant entity)
        {
            restaurantDbContext.Remove(entity);
            await restaurantDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await restaurantDbContext.Restaurants
                .Include(res=> res.Dishes).ToListAsync();
            
            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await restaurantDbContext.Restaurants
                .Include(res => res.Dishes)
                .FirstOrDefaultAsync(res => res.Id == id);
            
            return restaurant;
        }

        public async Task SaveChanges()
        {
            await restaurantDbContext.SaveChangesAsync();
        }
    }
}
