using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReverseAutoMapperDemo.Data;

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
        

    }
}
