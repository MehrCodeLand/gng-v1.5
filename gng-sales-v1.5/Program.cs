using Data.Leyer.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Services.Leyer.Services.CategoryServices;
using Services.Leyer.Services.CustomerService;
using Services.Leyer.Services.ProductService;
using Services.Leyer.Services.UserService;
using Services.Leyer.Services.ValidationService;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; ;
//    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}
//).AddJwtBearer(x =>
//{
//    x.TokenValidationParameters = new TokenValidationParameters
//    {

//    };
//});
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<DapperDbContext>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IValidationRepository , ValidationRepository>();
builder.Services.AddScoped<ICustomerRepository ,  CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
