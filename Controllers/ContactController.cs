using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Internationalization.Models;
using Internationalization.Services;

namespace Internationalization.Controllers;

public class ContactController : Controller
{
    private readonly ILogger<ContactController> _logger;

    public ContactController(ILogger<ContactController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Second()
    {
        return View();
    }
}
