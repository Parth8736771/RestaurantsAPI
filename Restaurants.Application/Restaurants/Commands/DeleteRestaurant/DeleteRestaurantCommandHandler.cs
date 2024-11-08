﻿using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(IRestaurantsRepository restaurantRepository, ILogger<DeleteRestaurantCommandHandler> _logger, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" Deleting Restaurant with id : {RestaurantId} and {@Restaurant}", request.Id, request);
            var restaurant = await restaurantRepository.GetByIdAsync(request.Id);
            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
                throw new ForbidException();

            await restaurantRepository.Delete(restaurant);
        }
    }
}
