using GenFu;
using Internationalization.Models;

namespace Internationalization.Services;

public class EmployeesDataService
{
    private readonly ICollection<EmployeeViewModel> _employees;
    public EmployeesDataService()
    {
        A.Configure<EmployeeViewModel>()
            .Fill(x => x.Size, () =>
            {
                return (decimal)(2.3 - A.Random.NextDouble());
            })
            .Fill(x => x.DateOfBirth, () =>
            {
                return DateTime.Now.AddYears(-50).AddDays(A.Random.Next(365 * 30));
            })
            .Fill(x => x.Email, x =>
            {
                return $"{x.Firstname?.ToLower()}.{x.Lastname?.ToLower()}@contoso.org";
            });
        _employees = A.ListOf<EmployeeViewModel>(72);
    }

    public IEnumerable<EmployeeViewModel> GetAll() => _employees;

    public IEnumerable<EmployeeViewModel> GetAll(Func<EmployeeViewModel, bool> predicate) => _employees.Where(predicate);

    public void Add(EmployeeViewModel model) => _employees.Add(model);

    public void Remove(EmployeeViewModel model) => _employees.Remove(model);

    public int NewId() => _employees.Max(m => m.Number + 1);
}