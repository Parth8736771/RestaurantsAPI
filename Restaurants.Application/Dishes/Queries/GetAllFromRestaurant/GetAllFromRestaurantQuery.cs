using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetAllFromRestaurant
{
    public class GetAllFromRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get; set; } = restaurantId;
    }
}
