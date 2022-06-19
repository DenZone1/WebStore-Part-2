using WebStore.Domain.Entities;
using WebStore.WebAPI.Clients.Base;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Clients.Employees;

public class EmployeesClient : BaseClient, IEmployeesData
{
    public EmployeesClient(HttpClient Client, string Address/*специально ошибка!*/)
        : base(Client, "api/employees")
    {
        
    }

    public int GetCount() { throw new NotImplementedException(); }
    public IEnumerable<Employee> GetAll() { throw new NotImplementedException(); }
    public IEnumerable<Employee> Get(int Skip, int Take) { throw new NotImplementedException(); }
    public Employee? GetById(int Id) { throw new NotImplementedException(); }
    public int Add(Employee employee) { throw new NotImplementedException(); }
    public bool Edit(Employee employee) { throw new NotImplementedException(); }
    public bool Delete(int Id) { throw new NotImplementedException(); }

}
