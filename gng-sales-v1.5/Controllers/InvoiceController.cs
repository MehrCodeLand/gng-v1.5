﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Leyer.Services.InvoiceService;
using Services.Leyer.ViewModels.Invoice;

namespace gng_sales_v1._5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceRepository _invoiceSer;

    public InvoiceController( IInvoiceRepository invoice)
    {
        _invoiceSer = invoice;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInvoice()
    {
        var response = await _invoiceSer.GetAllInvoice();
        if(response.HasError)
            return NotFound(response);


        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceVm createInvoiceVm)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _invoiceSer.CreateInvoice(createInvoiceVm);
        if(response.HasError)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteInvoiceById( int id)
    {
        var response = await _invoiceSer.DeleteById(id);
        if(response.HasError)
            return NotFound(response);


        return Ok(response);
    }
}
