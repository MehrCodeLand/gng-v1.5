using Data.Leyer.Models.ViewModels.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Leyer.Services.CustomerService;

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
        [Route("getCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var response = await _cutomerService.GetAllCustomer();
            if (response.ErrorCode < 0)
                return NotFound("Error "+ response.ErrorMessage );


            return Ok(response.Data);
        }

        [HttpGet]
        [Route("getCustomerByID/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var response = await _cutomerService.GetUserById(id);
            if (response.ErrorCode < 0)
                return NotFound(response.ErrorMessage);



            return Ok("customer" + response.Data);
        }
 
        [HttpPost]
        [Route("createCutomer")]
        public async Task<IActionResult> GetAllCutomer([FromBody] CreateCustomerVm createCustomerVm )
        {
            var response = await _cutomerService.CreateCustomer(createCustomerVm); 
            if(response.ErrorCode < 0)
            {
                return NotFound(response.ErrorMessage); 
            }

            return Ok(response.Message + " Done");
        }


        [HttpDelete]
        [Route("deleteCustomer")]
        public async Task<IActionResult> DeleteCustomer( [FromBody]DeleteCustomerVm deleteCustomer)
        {
            var responce = await _cutomerService.DeleteCustomer(deleteCustomer);
            if (responce.ErrorCode < 0)
                return NotFound("error " + responce.ErrorMessage);


            return Ok($"customer deleted");
        }


    }
}
