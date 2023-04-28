using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Venna.Data;
using Venna.Helpers;

var builder = WebApplication.CreateBuilder(args);

//services//

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<JollyContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SecureLinkin")));
builder.Services.AddSwaggerGen();
builder.Services.Configure<Jwt>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IHelpers, Helpers>();
builder.Services.AddScoped<IHelpers, Helpers>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value)),
        ValidIssuer = builder.Configuration.GetSection("JWT:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("JWT:Audiance").Value
    };
    x.Events = new JwtBearerEvents()
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["Token"];
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(x =>
{
    x.WithOrigins("https://localhost:7116")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();