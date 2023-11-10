using APIClienteFornecedor.Data;

using Microsoft.EntityFrameworkCore;
using APIClienteFornecedor.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using APIClienteFornecedor.Queries;
using APIClienteFornecedor.Handlers.Usuario;
using APIClienteFornecedor.Handlers.Produto;
using APIClienteFornecedor.Handlers.DetalhePedido;
using APIClienteFornecedor.Handlers.Pedido;
using APIClienteFornecedor.Repository;
using APIClienteFornecedor.Services.Tokren;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configurar o contexto do banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("SuaStringDeConexaoAqui"));

// Adicionar o serviço de autenticação
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<IConsultarUsuarioQueryHandler, ConsultarUsuarioQueryHandler>();

// Adicionar serviços relacionados a Produto
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<CriarProdutoCommandHandler>();
builder.Services.AddScoped<DeleteProdutoCommandHandler>();
builder.Services.AddScoped<AtualizarProdutoCommandHandler>();

// Adicionar serviços relacionados a Usuario
builder.Services.AddScoped<UsuarioHandler>();
builder.Services.AddScoped<CriarUsuarioCommandHandler>();
builder.Services.AddScoped<AtualizarUsuarioCommandHandler>();
builder.Services.AddScoped<DeleteUsuarioCommandHandler>();

// Adicionar serviços relacionados a DetalhePedido
builder.Services.AddScoped<AtualizaDetalhePedidoHandler>();
builder.Services.AddScoped<CriaDetalhePedidoHandler>();
builder.Services.AddScoped<DeletaDetalhePedidoHandler>();

// Adicionar serviços relacionados a Pedido
builder.Services.AddScoped<AtualizarPedidoCommandHandler>();
builder.Services.AddScoped<CriarPedidoCommandHandler>();
builder.Services.AddScoped<DeletarPedidoCommandHandler>();

// Configurar o Entity Framework
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql("DefaultConnection"));

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "MinhaAppAuthServer",
        ValidAudience = "MinhaAppCliente",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Chave123"))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
