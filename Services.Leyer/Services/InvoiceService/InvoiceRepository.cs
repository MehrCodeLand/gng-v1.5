using Dapper;
using Data.Leyer.DbContext;
using goolrang_sales_v1.Models;
using Services.Leyer.Localization;
using Services.Leyer.Responses.Structs;
using Services.Leyer.ViewModels.Invoice;

namespace Services.Leyer.Services.InvoiceService;
public class InvoiceRepository : IInvoiceRepository
{
    private readonly MyDbContext _db;
    public InvoiceRepository(MyDbContext myDb)
    {
        _db = myDb;
    }
    public async Task<Responses<Invoice>> GetInvoiceById(int invoiceId)
    {
        var query = $"select * from Invoice where InvoiceId = {invoiceId}";

        using(var connection = _db.CreateConnection())
        {
            var result = await connection.QueryAsync(query);

            if(result.Count() == 1)
            {
                return new Responses<Invoice>();
            }

            return new Responses<Invoice>()
            {
                HasError = true,
                ErrorMessage = Messages.RecordNotFound
            };
        }
    }
    public async Task<Responses<Invoice>> GetAllInvoice()
    {
        var query = "select * from Invoice";

        using (var connection = _db.CreateConnection())
        {
            var result = await connection.QueryAsync<Invoice>(query);

            if (result.Count() > 0)
            {
                return new Responses<Invoice>()
                {
                    Data = result
                };
            }

            return new Responses<Invoice>()
            {
                HasError = true,
                ErrorMessage = Messages.NoDataReult
            };
        }
    }
    public async Task<Responses<Invoice>> DeleteById(int id)
    {
        if (id <= 0)
            return new Responses<Invoice>()
            {
                HasError = true,
                ErrorMessage = Messages.RecordNotFound
            };
        var query = $"delete_invoice_proc @invoiceId = {id}";

        using(var connection = _db.CreateConnection())
        {
            var result = await connection.QueryAsync(query);

            if(result.Any( x => x.ErrorCode != null))
            {
                return new Responses<Invoice>()
                {
                    HasError = true,
                    ErrorMessage = Messages.SomthingWrong
                };
            }
            return new Responses<Invoice>()
            {
                HasError = false,
            };
        }
    }
    public async Task<Responses<Invoice>> CreateInvoice( CreateInvoiceVm createInvoice)
    {
        var query = $"insert_invoice_proc " +
            $"@invoiceId = {createInvoice.InvoiceId} ," +
            $"@customerId = {createInvoice.CustomerId} ," +
            $"@invoiceDate = '{createInvoice.InvoiceDate}'," +
            $"@userId = {createInvoice.UserId}, " +
            $"@totalAmount = {createInvoice.TotalAmount} ";

        using ( var connection = _db.CreateConnection())
        {
            var result = await connection.QueryAsync(query);

            if(result.Any(x => x.ErrorMessage != null))
            {
                return new Responses<Invoice>()
                {
                    HasError = true,
                    Message = Messages.SomthingWrong
                };
            }
            return new Responses<Invoice>();
        }
    }
}
