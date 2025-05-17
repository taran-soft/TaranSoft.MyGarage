﻿using Microsoft.AspNetCore.Mvc;
using TaranSoft.MyGarage.Contracts.Dto;
using TaranSoft.MyGarage.Contracts.Request;
using TaranSoft.MyGarage.Repository.Interfaces;
using TaranSoft.MyGarage.Services.Interfaces;

namespace MyGarage.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : AuthorizedApiController
{
    private readonly IUsersService _usersService;
    private readonly IUserRepository _userRepository;
    
    public AuthController(IUsersService usersService, IUserRepository userRepository) 
    {
        _usersService = usersService;
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Health()
    {
        return Ok();
    }

    [HttpPost("token")]
    public async Task<IActionResult> Token([FromBody] LoginModel model)
    {
        try 
        {
            var token = await _usersService.GetToken(model.Email, model.Password);

            var user = await _userRepository.GetByEmail(model.Email);

            return Ok(new
            {
                Token = token,
                ExpiresIn = 3600,
                UserId = user.Id,
                UserName = user.Nickname
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] UserCreateRequest request)
    {
        try
        {
            await _usersService.Register(new UserDto
            {
                Email = request.Email,
                Password = request.Password,
                Nickname = request.Nickname
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Created(
            "User has been created successfully", 
            StatusCodes.Status201Created);
    }
}