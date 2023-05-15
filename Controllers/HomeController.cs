using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Internationalization.Models;
using Internationalization.Services;

namespace Internationalization.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly EmployeesDataService _service;

    public HomeController(ILogger<HomeController> logger,
        EmployeesDataService service)
    {
        _logger = logger;
        _service = service;
    }

    public IActionResult Index(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var allEmplyees = _service.GetAll();
        var count = allEmplyees.Count();
        var pages = count % pageSize == 0
            ? count / pageSize
            : (count / pageSize) + 1;
        var employees = allEmplyees
            .Skip(pageSize * (page - 1))
            .Take(pageSize);
        var model = new HomeIndexViewModel
        {
            Count = count,
            Page = page,
            Pages = pages,
            PageSize = pageSize,
            Employees = employees
        };
        return View(model);
    }

    [Route("/{employeeNumber}/details")]
    public IActionResult Details (int employeeNumber)
    {
        return View(nameof(Privacy));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
