using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sprint_3.Data;
using Sprint_3.Repository;
using Sprint_3.Repository.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração do JWT
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var json = System.Text.Json.JsonSerializer.Serialize(new
            {
                message = "Você precisa estar autenticado para acessar este recurso."
            });

            return context.Response.WriteAsync(json);
        },
        OnForbidden = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var json = System.Text.Json.JsonSerializer.Serialize(new
            {
                message = "Você não tem permissão para acessar este recurso."
            });

            return context.Response.WriteAsync(json);
        }
    };
});

// Configuração do DbContext com Oracle
builder.Services.AddDbContext<dbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"))
);

// Configuração de controllers, health checks e Swagger
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

builder.Services.AddSwaggerGen(options =>
{
    

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira apenas o token JWT. O prefixo 'Bearer' será adicionado automaticamente."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configuração dos repositórios
builder.Services.AddScoped<IUsuario, UsuarioRepository>();
builder.Services.AddScoped<IPedidoProduto, PedidoProdutoRepository>();
builder.Services.AddScoped<IPedido, PedidoRepository>();
builder.Services.AddScoped<IProduto, ProdutoRepository>();

var app = builder.Build();

// Middlewares
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Rotas
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();

public partial class Program { }
