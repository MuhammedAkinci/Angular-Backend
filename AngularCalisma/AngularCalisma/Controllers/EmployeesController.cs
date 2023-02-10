using AngularCalisma.Data;
using AngularCalisma.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace AngularCalisma.Controllers
{
    [ApiController]
    [Route("api/Employees")]
    public class EmployeesController : Controller
    {
        private readonly DBContext _dBContext;
        public EmployeesController(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _dBContext.Employees.ToListAsync();
            await _dBContext.SaveChangesAsync();
            return Ok(employees);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<IEnumerable<Employee>>> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _dBContext.Employees.AddAsync(employeeRequest);
            await _dBContext.SaveChangesAsync();
            return Ok(employeeRequest);
        }
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee([FromRoute] Guid Id)
        {
            var employee = await _dBContext.Employees.FirstOrDefaultAsync(x => x.Id == Id); 
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult<IEnumerable<Employee>>> UpdateEmployee( Guid id, [FromBody] Employee updateEmployeeRequest)
        {
           
            var updateUser = await _dBContext.Employees.FindAsync(id);
            updateUser.Name = updateEmployeeRequest.Name;
            updateUser.Email = updateEmployeeRequest.Email;
            updateUser.Phone = updateEmployeeRequest.Phone;
            updateUser.Salary = updateEmployeeRequest.Salary;
            updateUser.Department = updateEmployeeRequest.Department;
            await _dBContext.SaveChangesAsync();
            return Ok(updateUser);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult<IEnumerable<Employee>>> DeleteEmployee([FromRoute] Guid id )
        {
            var employee = await _dBContext.Employees.FindAsync(id);
            if(employee == null) 
            {
                return NotFound();
            }
            _dBContext.Employees.Remove(employee);  
            await _dBContext.SaveChangesAsync();
            return Ok(employee);

        }
    }
}
