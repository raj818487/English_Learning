using EnglishLearning.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public ActionResult<ApiResponse<object>> Get() => Ok(ApiResponse<object>.Ok(new { Status = "Healthy" }));
}
