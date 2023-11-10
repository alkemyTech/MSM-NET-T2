using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VirtualWallet.DataAccess;
using VirtualWallet.Repository;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services;
using VirtualWallet.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db connection
builder.Services.AddDbContext<VirtualWalletDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("VirtualWalletConnection"));
});

// Scoped Repo - Services
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionRepository>();
builder.Services.AddScoped<ICatalogueRepository, CatalogueRepository>();
builder.Services.AddScoped<IFixedTermRepository, FixedTermRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<CatalogueService>();
builder.Services.AddScoped<FixedTermService>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
