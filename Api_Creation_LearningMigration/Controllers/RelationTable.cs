using Api_Creation_LearningMigration.Context;
using Api_Creation_LearningMigration.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Creation_LearningMigration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RelationTable : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly ApplicationDbContext _context;
        public RelationTable(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        //public ActionResult AddEmployee(Employee employee)
        //{
        //    if(employee == null)
        //    {
        //        return BadRequest("Required Fields");
        //    }
        //    var result = _context.AddAsync(employee.Department);
        //    _context.SaveChanges();
        //    employee.DeptID = result.ID;
        //    _context.AddAsync(employee);
        //    _context.SaveChanges();

        //    return Ok(employee);
        //}

        [HttpPost]
        public async Task<IActionResult> PostEmployeeWithDepartment([FromBody] Employee employeeWithDepartmentDto)
        {
            if (employeeWithDepartmentDto == null)
            {
                return BadRequest("Employee data is null");
            }

            // Check if the department already exists
            var existingDepartment = await _context.DepartmentsTbl
                .FirstOrDefaultAsync(d => d.Id == employeeWithDepartmentDto.DeptID);

            if (existingDepartment == null)
            {
                // Department does not exist, so create a new one
                if (employeeWithDepartmentDto.Department == null)
                {
                    return BadRequest("Department data is required to create a new department.");
                }

                var newDepartment = new Department
                {
                    DeptName = employeeWithDepartmentDto.Department.DeptName
                };

                _context.DepartmentsTbl.Add(newDepartment);
                await _context.SaveChangesAsync();

                // Update the DeptID to the newly created department's ID
                employeeWithDepartmentDto.DeptID = newDepartment.Id;
            }
            else
            {
                // Department exists, use the existing one
                employeeWithDepartmentDto.DeptID = existingDepartment.Id;
            }

            // Map DTO to entity
            var employee = new Employee
            {
                EmpName = employeeWithDepartmentDto.EmpName,
                Salary = employeeWithDepartmentDto.Salary,
                City = employeeWithDepartmentDto.City,
                DeptID = employeeWithDepartmentDto.DeptID
            };

            _context.EmployeeTbl.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmpID }, employee);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.EmployeeTbl.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }
    }
}
