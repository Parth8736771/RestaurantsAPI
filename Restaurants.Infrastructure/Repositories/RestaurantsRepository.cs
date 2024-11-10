using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

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

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = restaurantDbContext.Restaurants
                .Where(r => searchPhraseLower == null
                    || (r.Name.ToLower().Contains(searchPhraseLower)
                    || r.Description.ToLower().Contains(searchPhraseLower)));

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    { nameof(Restaurant.Name), r => r.Name },
                    { nameof(Restaurant.Description), r => r.Description },
                    {nameof(Restaurant.Category), r => r.Category }
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var restaurants = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (restaurants, totalCount);
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
