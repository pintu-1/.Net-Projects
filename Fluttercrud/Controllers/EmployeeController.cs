using Fluttercrud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Fluttercrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public Response GetAllEmployees()
        {
            if (_configuration == null)
            {
              
                return new Response
                {
                    StatusCode = 500, // Internal Server Error
                    StatusMessage = "Configuration is missing",
                    listEmployee = null
                };
            }
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection").ToString());
            Response response = new Response();
            DAL dal = new DAL();
            response = dal.GetAllEmployees(connection);        
            return response;
        }

        [HttpPost]
        [Route("PostEmployee")]
        public Response PostEmployee(Employee employee) {

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CrudConnection").ToString());
            Response response = new Response();
            DAL dal = new DAL();
            response = dal.PostEmployees(connection, employee);
            return response;

        }

    }
}
