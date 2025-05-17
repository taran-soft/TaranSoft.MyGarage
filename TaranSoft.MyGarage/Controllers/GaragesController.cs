using Microsoft.AspNetCore.Mvc;
using TaranSoft.MyGarage.Contracts;
using TaranSoft.MyGarage.Contracts.Common;
using TaranSoft.MyGarage.Contracts.Dto;
using TaranSoft.MyGarage.Services.Interfaces;

namespace MyGarage.Controllers;

//[Authorize]
[ApiController]
[Route("api/garages")]
public class GaragesController : AuthorizedApiController
{
    private readonly IGarageService _service;

    public GaragesController(IGarageService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] SearchOptions options)
    {
        var items = await _service.Search(options.Take, options.Skip);

        return Ok(new PageOf<GarageDto>
        {
            Items = items,
            Total = items.Count
        });
        
    }
}