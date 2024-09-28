using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommand(int restaurantId) : IRequest
    {
        public int RestaurantId { get; set; } = restaurantId;
    }
}
