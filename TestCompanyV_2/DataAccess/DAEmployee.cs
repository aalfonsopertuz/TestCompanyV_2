using System.Text.Json;
using TestCompanyV_2.Domain;
using TestCompanyV_2.Models;

namespace TestCompanyV_2.DataAccess
{
    public interface IDAEmployee
    {
        public Task<List<Employee>> GetEmployeesAsync();
        public Task<Employee> GetEmployeeById(int? id);
    }
    public class DAEmployee : IDAEmployee
    {
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            try
            {
                List<Employee> employees = new();
                const string url = "https://dummy.restapiexample.com/api/v1/employees";
                string responseBody = await ResponseBodyEndPoint(url);

                JsonDataList dataEndPoint = JsonSerializer.Deserialize<JsonDataList>(responseBody) ?? new JsonDataList();
                if (dataEndPoint != null && dataEndPoint.status == "success")
                {
                    employees = dataEndPoint.data ?? new List<Employee>();
                }

                return employees;

            }
            catch (Exception)
            {
                throw;
            }

        }
        
        public async Task<Employee> GetEmployeeById(int? id)
        {
            Employee employee = new();
            if (id == null || id == 0)
                return employee;

            string url = $"https://dummy.restapiexample.com/api/v1/employee/{id}";
            string responseBody = await ResponseBodyEndPoint(url);
            JsonData dataEndPoint = JsonSerializer.Deserialize<JsonData>(responseBody) ?? new JsonData();

            if (dataEndPoint != null && dataEndPoint.status == "success")
            {
                employee = dataEndPoint.data ?? new Employee();
            }

            return employee;
        }

        private async Task<string> ResponseBodyEndPoint(string url)
        {
            try
            {
                string responseBody = string.Empty;
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(responseBody))
                        return responseBody;

                    return responseBody;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
