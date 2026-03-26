using Employee.Api.Infrastructures;
using Microsoft.AspNetCore.Mvc;

public class MasterController : ControllerBase
{
    private readonly EmployeeRepository _repo;

    public MasterController(EmployeeRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("all-employees")]
    public async Task<IActionResult> GetEmployee()
    {
        var data = await _repo.GetEmployee();
        return Ok(data);
    }
}