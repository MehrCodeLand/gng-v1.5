using goolrang_sales_v1.Models;
using Services.Leyer.Responses.Structs;
using Services.Leyer.ViewModels.Invoice;

namespace Services.Leyer.Services.InvoiceService;

public interface IInvoiceRepository
{
    Task<Responses<Invoice>> GetAllInvoice();
    Task<Responses<Invoice>> DeleteById(int id);
    Task<Responses<Invoice>> CreateInvoice(CreateInvoiceVm createInvoice);
}