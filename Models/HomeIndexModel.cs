using System.ComponentModel.DataAnnotations;

namespace Internationalization.Models;

public class HomeIndexViewModel
{
    public int Count { get; set; }

    public int Page { get; set; }

    public int Pages { get; set; }

    public int PageSize { get; set; }

    public EmployeeViewModel Template { get; } = new EmployeeViewModel();

    public IEnumerable<EmployeeViewModel>? Employees { get; set; }
}

public class EmployeeViewModel
{
    [Display(Name = "Number")]
    public int Number { get; set; }

    [Display(Name = "Firstname")]
    public string? Firstname { get; set; }

    [Display(Name = "Lastname")]
    public string? Lastname { get; set; }

    [Display(Name = "Department")]
    public string? Department { get; set; }

    [Display(Name = "Phone")]
    public string? Phone { get; set; }

    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Display(Name = "Date of birth")]
    public DateTime DateOfBirth { get; set; }

    [Display(Name = "Size")]
    public decimal Size { get; set; }

    [Display(Name = "Salery")]
    public decimal Salery { get; set; }
}