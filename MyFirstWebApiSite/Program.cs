using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyFirstWebApiSite;
using NLog.Web;
using Repositories;
using Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseNLog();
builder.Services.AddDbContext<AdoNetMarketContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString"]));

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddTransient<IRatingRepository, RatingRepository>();
builder.Services.AddTransient<IRatingService, RatingService>();

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("key").Value);

builder.Services
    .AddAuthentication(option =>
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme

   )
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents()
        {

            //get cookie value
            OnMessageReceived = context =>
            {
                var a = "";
                context.Request.Cookies.TryGetValue("X-Access-Token", out a);
                context.Token = a;
                return Task.CompletedTask;
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware();
app.UseErrorMiddleware();
app.UseStaticFiles();
app.MapControllers();

app.Run();
