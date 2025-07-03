using Employee.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Repo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;
        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Employeedb");
        }

        public List<EmployeeModel> GetAll()
        {
            var employees = new List<EmployeeModel>();
            using var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT * FROM Employee", con);
            con.Open();
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                employees.Add(new EmployeeModel
                {
                    Id = (int)rdr["Id"],
                    Name = rdr["Name"].ToString(),
                    Department = rdr["Department"].ToString(),
                    Salary = Convert.ToDecimal(rdr["Salary"])
                });
            }
            return employees;
        }

        public EmployeeModel GetById(int id)
        {
            using var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT * FROM Employee WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new EmployeeModel
                {
                    Id = (int)rdr["Id"],
                    Name = rdr["Name"].ToString(),
                    Department = rdr["Department"].ToString(),
                    Salary = Convert.ToDecimal(rdr["Salary"])
                };
            }
            return null;
        }

        public void Add(EmployeeModel employee)
        {
            using var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("INSERT INTO Employee (Name, Department, Salary) VALUES (@Name, @Dept, @Salary)", con);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Dept", employee.Department);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void Update(EmployeeModel employee)
        {
            using var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("UPDATE Employee SET Name = @Name, Department = @Dept, Salary = @Salary WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", employee.Id);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Dept", employee.Department);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("DELETE FROM Employee WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}

