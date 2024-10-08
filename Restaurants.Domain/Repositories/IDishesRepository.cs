﻿using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish Dish);
        Task Delete(List<Dish> dishes);
    }
}
