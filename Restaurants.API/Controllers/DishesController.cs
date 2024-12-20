﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDishes;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetAllFromRestaurant;
using Restaurants.Application.Dishes.Queries.GetByIdFromRestaurant;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants/{restaurantId}/dishes")]
[Authorize]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute]int restaurantId, CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        var dishId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetByIdFromRestaurant), new { restaurantId,  dishId }, null);
    }

    [HttpGet]
    [Authorize(Policy = PolicyNames.Atleast20)]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllFromRestaurant([FromRoute]int restaurantId)
    {
        var dishes = await mediator.Send(new GetAllFromRestaurantQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetByIdFromRestaurant([FromRoute]int restaurantId, int dishId)
    {
        var dish = await mediator.Send(new GetByIdFromRestaurantQuery(restaurantId, dishId));
        return Ok(dish);
    }

    [HttpDelete]
    public async Task DeleteDishFromRestaurant([FromRoute]int restaurantId)
    {
        await mediator.Send(new DeleteDishCommand(restaurantId));
    }
}
