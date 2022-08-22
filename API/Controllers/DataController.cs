using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public DataController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryParameters? queryParameters)
    {
        var data = await _unitOfWork.Lessons.GetAllAsync(queryParameters);
        return Ok(data);
    }
}