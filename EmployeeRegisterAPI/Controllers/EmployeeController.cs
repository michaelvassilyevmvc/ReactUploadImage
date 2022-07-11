using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeRegisterAPI.Data;
using Microsoft.AspNetCore.Mvc;
using EmployeeRegisterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRegisterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _conext;
        public EmployeeController(EmployeeDbContext conext)
        {
            _conext = conext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployees()
        {
            return await _conext.Employees.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> PostEmployeeModel(EmployeeModel employeeModel)
        {
            _conext.Employees.Add(employeeModel);
            await _conext.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeModel", new { id = employeeModel.EmployeeID }, employeeModel);
        }

        [HttpDelete]
        public async Task<ActionResult<EmployeeModel>> DeleteEmployeeModel(int id)
        {
            var employeeModel = await _conext.Employees.FindAsync(id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            _conext.Employees.Remove(employeeModel);
            return employeeModel;
        }

        private bool EmployeeModelExists(int id)
        {
            return _conext.Employees.Any(e => e.EmployeeID == id);
        }
    }
}