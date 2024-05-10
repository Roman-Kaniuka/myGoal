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