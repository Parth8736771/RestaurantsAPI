using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantsDbContext restaurantsDbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish dish)
        {
            restaurantsDbContext.Dishes.Add(dish);
            await restaurantsDbContext.SaveChangesAsync();
            return dish.Id;
        }

        public async Task Delete(List<Dish> dishes)
        {
            restaurantsDbContext.Dishes.RemoveRange(dishes);
            await restaurantsDbContext.SaveChangesAsync();
        }
    }
}
