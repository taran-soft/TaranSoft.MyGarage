﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaranSoft.MyGarage.Contracts;
using TaranSoft.MyGarage.Contracts.Request;
using TaranSoft.MyGarage.Contracts.Common;
using TaranSoft.MyGarage.Services.Interfaces;

namespace MyGarage.Controllers;

[Authorize]
[ApiController]
[Route("api/cars")]
public class CarsController : AuthorizedApiController
{
    private readonly ICarsService _carsService;

    public CarsController(ICarsService carsService)
    {
        _carsService = carsService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] SearchOptions options)
    {
        var items = await _carsService.Search(options.Take, options.Skip);

        return Ok(new PageOf<CarDto>
        {
            Items = items,
            Total = items.Count
        });
        
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CarDto car)
    {
        if (CurrentUserId != null) car.CreatedBy = new Guid(CurrentUserId);

        var id = await _carsService.Create(car);
        return CreatedAtAction(nameof(Get), new { id }, car);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCar(Guid id, [FromBody] UpdateCarRequest request)
    {
        await _carsService.Update(id,
            new CarDto
            {
                Id = id,
                Model = request.Model,
                Year = request.Year,
                ImageId = request.ImageId
            });
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _carsService.Delete(id);
        return NoContent();
    }
}