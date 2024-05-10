using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myGoal.Domain.Dto.Report;
using myGoal.Domain.Entity;
using myGoal.Domain.Interfaces.Services;
using myGoal.Domain.Result;

namespace myGoal.Api.Controllers;
[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    // GET 
    
    /// <summary>
    /// Отримання звіту за id
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо звіт був знайдений за id</response>
    /// <response code="400">Якщо звіт не був знайдений за id</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetReport(long id)
    {
        var response = await _reportService.GetReportByIdAsync(id);
        if (response.IsSeccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    //GET user report
    
    /// <summary>
    /// Отримання звіту за userId
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET
    ///     {
    ///         "userId": "1"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо звіт був знайдений за userId</response>
    /// <response code="400">Якщо звіт не був знайдений за userId</response>
    [HttpGet("reports/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetUserReport(long userId)
    {
        var response = await _reportService.GetReportsAsync(userId);
        if (response.IsSeccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    //DELETE 
    
    /// <summary>
    /// Видалення звіту
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELTE
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо звіт був видалений</response>
    /// <response code="400">Якщо звіт не був видалений</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReportDto>>> DeleteReport (long id)
        {
            var response = await _reportService.DeleteReportAsync(id);
            if (response.IsSeccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        //POST
        
        /// <summary>
        /// Створення звіту
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST
        ///     {
        ///         "name": "Report #1",
        ///         "description": "Test report",
        ///         "userId": "1"
        ///     }
        /// </remarks>
        /// <response code="200">Якщо звіт був створений</response>
        /// <response code="400">Якщо звіт не був створений</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReportDto>>> CreateReport([FromBody]CreateReportDto dto)
        {
            var response = await _reportService.CreateReportAsync(dto);
            if (response.IsSeccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        //PUT
        
        /// <summary>
        /// Оновлення звіту
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT
        ///     {
        ///         "id": "1",
        ///         "name": "Report #1",
        ///         "description": "Test report"
        ///     }
        /// </remarks>
        /// <response code="200">Якщо звіт був оновлений</response>
        /// <response code="400">Якщо звіт не був оновлений</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReportDto>>> UpdateReport([FromBody]UpdateReportDto dto)
        {
            var response = await _reportService.UpdateReportAsync(dto);
            if (response.IsSeccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
}