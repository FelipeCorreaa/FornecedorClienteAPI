using APIClienteFornecedor.Data;
using APIClienteFornecedor.Handlers;
using APIClienteFornecedor.Services.Tokren;
using Microsoft.EntityFrameworkCore;
using APIClienteFornecedor.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("SuaStringDeConexaoAqui"));
// Adicionar o serviço de autenticação
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<UsuarioHandler>();
builder.Services.AddScoped<CriarUsuarioCommandHandler>();

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("DefaultConnection"));

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
