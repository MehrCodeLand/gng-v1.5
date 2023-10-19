using Data.Leyer.DbContext;
using Services.Leyer.Services.CategoryServices;
using Services.Leyer.Services.CustomerService;
using Services.Leyer.Services.InvoiceService;
using Services.Leyer.Services.ProductService;
using Services.Leyer.Services.UserService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors();

builder.Services.AddControllers();

builder.Services.AddSingleton<MyDbContext>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository ,  CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
