using TestCompanyV_2.DataAccess;
using TestCompanyV_2.Models;

namespace TestCompanyV_2.Domain
{
    public interface IDEmployee
    {
        public Task<List<Employee>> GetEmployees();
        public Task<Employee> GetEmployeeById(int? id);
    }

    public class DEmployee : IDEmployee
    {
        private IDAEmployee _employee;
        public DEmployee(IDAEmployee employee)
        {
            _employee = employee;
        }

        public async Task<Employee> GetEmployeeById(int? id)
        {
            Employee employee = await _employee.GetEmployeeById(id);

            if (employee == null)
                return new Employee();

            employee.employee_salary_per_year = GetSalaryEmployeePerYear(employee.employee_salary);
            return employee;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees = await _employee.GetEmployeesAsync();

            if (employees == null || !employees.Any())
                return new List<Employee>();

            foreach (var item in employees)
            {
                item.employee_salary_per_year = GetSalaryEmployeePerYear(item.employee_salary);
            }

            return employees;
        }

        private int? GetSalaryEmployeePerYear(int? salary) 
        {
            return (salary == null) ? 0 : salary * 12;
        }
    }
}
