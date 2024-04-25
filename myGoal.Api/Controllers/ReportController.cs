using Microsoft.AspNetCore.Mvc;
using myGoal.Domain.Dto.Report;
using myGoal.Domain.Interfaces.Services;
using myGoal.Domain.Result;

namespace myGoal.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
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
}