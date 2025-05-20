using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReverseAutoMapperDemo.Data;
using ReverseAutoMapperDemo.DTOs;
using ReverseAutoMapperDemo.Models;

namespace ReverseAutoMapperDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly EmployeeDBContext _context;

        public EmployeesController(EmployeeDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Employees/GetEmployees
        // Retrieves all employees with their address details
        [HttpGet("GetEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .Include(e => e.Address)
                .ToListAsync();

            var employeeDTOs = _mapper.Map<List<EmployeeDTO>>(employees);

            return Ok(employeeDTOs);

        }

        // GET: api/Employees/GetEmployee/1
        // Retrieves a single employee by ID with address details
        [HttpGet("GetEmployee/{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee([FromRoute] int id)
        {

            // Find employee by ID and include Address data
            var employee = await _context.Employees
                .AsNoTracking()
                .Include(e => e.Address)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound($"Employee with ID {id} not found.");

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return Ok(employeeDTO);

        }


        // POST: api/Employees/AddEmployee
        // Creates a new employee record from the provided EmployeeDTO
        [HttpPost("AddEmployee")]
        public async Task<ActionResult<EmployeeDTO>> AddEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            if(employeeDTO == null)
                return BadRequest("Employee data is null.");

            //Converting the incoming EmployeeDTO to an Employee entity
            var employee = _mapper.Map<Employee>(employeeDTO);

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            // Map the newly created Employee entity back to EmployeeDTO (includes generated ID)
            var createEmployeeDTO =_mapper.Map<EmployeeDTO>(employee);

            return Ok(createEmployeeDTO);

        }

        // PUT: api/Employees/UpdateEmployee/1
        // Updates an existing employee's details based on the provided EmployeeDTO
        [HttpPut("UpdateEmployee/{id}")]
        public async Task<ActionResult<EmployeeDTO>> updateEmployee([FromRoute] int id, [FromForm] EmployeeDTO employeeDTO)
        {
            if (id != employeeDTO.EmployeeId)
                return BadRequest("Employee ID mismatch");

            //Retrieve existing employee.
            var existingEmployee = await _context.Employees
                .Include(e => e.Address)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (existingEmployee == null)
                return NotFound($"Employee with ID {id} not found.");

            _mapper.Map(employeeDTO, existingEmployee);

            try
            {
                // Save the updated record in the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound($"Employee with ID {id} no longer exists.");
                }
                else
                {
                    // Return a 500 status with an error message on exception
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
                
            }

            var updateEmployeeDTO = _mapper.Map<EmployeeDTO>(existingEmployee);

            return Ok(updateEmployeeDTO);

        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

    }
}
