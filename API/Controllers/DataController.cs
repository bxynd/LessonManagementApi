using Application.Interfaces;
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
    public async Task<IActionResult> GetAll()
    {
        var data = await _unitOfWork.Lessons.GetAllAsync();
        return Ok(data);
    }
}