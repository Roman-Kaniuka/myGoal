using Microsoft.AspNetCore.Mvc;

namespace myGoal.Api.Controllers;

public class ReportController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}