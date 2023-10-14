using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Leyer.Services.CustomerService;
using Services.Leyer.ViewModels.Customer;

namespace gng_sales_v1._5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _cutomerService;
        public CustomerController( ICustomerRepository customerRepository )
        {
            _cutomerService = customerRepository;
        }

        [HttpGet]
        [Route("GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var response = await _cutomerService.GetAllCustomer();
            if (response.HasError)
                return NotFound(response );

            return Ok(response);
        }

        [HttpGet]
        [Route("GetCustomerByID/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var response = await _cutomerService.GetUserById(id);
            if (response.HasError)
                return NotFound(response);

            return Ok(response);
        }
 
        [HttpPost]
        public async Task<IActionResult> GetAllCutomer([FromBody] CreateCustomerVm createCustomerVm )
        {
            if (!ModelState.IsValid)
                return NotFound(ModelState);

            var response = await _cutomerService.CreateCustomer(createCustomerVm); 
            if(response.HasError)
            {
                return NotFound(response); 
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer( [FromBody]DeleteCustomerVm deleteCustomer)
        {
            if (!ModelState.IsValid)
                return NotFound(ModelState);

            var response = await _cutomerService.DeleteCustomer(deleteCustomer);
            if (response.HasError)
                return NotFound(response);

            return Ok(response);
        }
    }
}
