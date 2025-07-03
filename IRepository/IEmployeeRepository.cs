using Employee.DTOs;

namespace Employee.Repo
{
    public interface IEmployeeRepository
    {
        List<EmployeeModel> GetAll();
        EmployeeModel GetById(int id);
        void Add(EmployeeModel employee);
        void Update(EmployeeModel employee);
        void Delete(int id);
    }
}
