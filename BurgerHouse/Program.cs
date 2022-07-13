using BurgerHouse.Services.AuthService;
using BurgerHouse.Services.CategoriesService;
using BurgerHouse.Services.ConfirmService;
using BurgerHouse.Services.OrdersService;
using BurgerHouse.Services.WorkersService;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var constring = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<ApplicationDbContext>(opt => 
{

    opt.UseSqlServer(constring);

}
    
    );

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ICategoriesService, CategoriesService>();
builder.Services.AddTransient<IConfirmService, ConfirmService>();
builder.Services.AddTransient<IOrdersService, OrdersService>();
builder.Services.AddTransient<IWorkersService, WorkersService>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["AuthSettings:Issuer"],

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"])),
        ValidateLifetime = false,
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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
