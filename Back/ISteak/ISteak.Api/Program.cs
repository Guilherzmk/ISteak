using ISteak.Api;
using ISteak.Core.Addresses;
using ISteak.Core.Customer;
using ISteak.Core.Products;
using ISteak.Core.Stores;
using ISteak.Core.Users;
using ISteak.Repositories.Addresses;
using ISteak.Repositories.Customers;
using ISteak.Repositories.Products;
using ISteak.Repositories.Shared.Sql;
using ISteak.Repositories.Stores;
using ISteak.Repositories.Users;
using ISteak.Services.Addresses;
using ISteak.Services.Customers;
using ISteak.Services.Products;
using ISteak.Services.Stores;
using ISteak.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{


    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                    }
                });
});
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<SqlConnectionProvider>(_ => new SqlConnectionProvider("workstation id=ISteakDb.mssql.somee.com;packet size=4096;user id=Guilherzmk_SQLLogin_1;pwd=7ukhyzaowk;data source=ISteakDb.mssql.somee.com;persist security info=False;initial catalog=ISteakDb"));

builder.Services.AddTransient<IAddressRepository, AddressRepository>();
builder.Services.AddTransient<IStoreRepository, StoreRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddTransient<IAddressService, AddressService>();
builder.Services.AddTransient<IStoreService, StoreService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IProductService, ProductService>();





var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseRouting();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints
        .MapControllers()
        .RequireAuthorization();
});

app.Run();
