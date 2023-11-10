using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
//builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
//builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<ICatalogueRepository, CatalogueRepository>();
builder.Services.AddScoped<ICatalogueService, CatalogueService>();

builder.Services.AddScoped<ITransactionsRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

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
