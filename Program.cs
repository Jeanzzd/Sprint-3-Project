using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sprint_3.Data;
using Sprint_3.Repository;
using Sprint_3.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<dbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"))
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Description = "API para cadastro de Ecommerce",

    });
}
    );


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsuario, UsuarioRepository>();
builder.Services.AddScoped<IPedidoProduto, PedidoProdutoRepository>();
builder.Services.AddScoped<IPedido, PedidoRepository>();
builder.Services.AddScoped<IProduto, ProdutoRepository>();



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
