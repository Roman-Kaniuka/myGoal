using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myGoal.Domain.Dto.Role;
using myGoal.Domain.Entity;
using myGoal.Domain.Interfaces.Services;
using myGoal.Domain.Result;

namespace myGoal.Api.Controllers;

[ApiController]
//[Authorize]
[Consumes(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    //POST
        
    /// <summary>
    /// Створення ролі
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST
    ///     {
    ///         "name": "Admin",
    ///     }
    /// </remarks>
    /// <response code="200">Якщо роль було створено</response>
    /// <response code="400">Якщо роль не було створено</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> CreateRole([FromBody]CreateRoleDto dto)
    {
        var response = await _roleService.CreateRoleAsync(dto);
        if (response.IsSeccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    //DELETE 
    
    /// <summary>
    /// Видалення ролі
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо роль було видалено</response>
    /// <response code="400">Якщо роль не було видалено</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> DeleteRole(long id)
    {
        var response = await _roleService.DeleteRoleAsync(id);
        if (response.IsSeccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    //PUT
        
    /// <summary>
    /// Оновлення ролі
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT
    ///     {
    ///         "id": "1",
    ///         "name": "Admin",
    ///     }
    /// </remarks>
    /// <response code="200">Якщо роль була оновлена</response>
    /// <response code="400">Якщо роль не була оновлена</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> UpdateRole([FromBody]RoleDto dto)
    {
        var response = await _roleService.UpdateRoleAsync(dto);
        if (response.IsSeccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    //POST
        
    /// <summary>
    /// Присвоєння ролі користувачу
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST
    ///     {
    ///         "login": "User #1",
    ///         "roleName": "Admin"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо роль було присвоєно</response>
    /// <response code="400">Якщо роль не було присвоєно</response>
    [HttpPost("addRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> AddRoleForUser([FromBody]UserRoleDto dto)
    {
        var response = await _roleService.AddRoleForUserAsync(dto);
        if (response.IsSeccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}