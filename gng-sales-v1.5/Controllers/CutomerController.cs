using Data.Leyer.Models.ViewModels.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Leyer.Services.CustomerService;

namespace gng_sales_v1._5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CutomerController : ControllerBase
    {
        private readonly ICustomerRepository _cutomerService;
        public CutomerController( ICustomerRepository customerRepository )
        {
            _cutomerService = customerRepository;
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
