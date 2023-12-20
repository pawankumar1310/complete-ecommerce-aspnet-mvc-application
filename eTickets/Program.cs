using eTickets.Data;
using eTickets.Data.Cart;
using eTickets.Data.IServices;
using eTickets.Data.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//DbContext Configuration
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStrings")));
//services Configuration
builder.Services.AddScoped<IActorService, ActorsService>();
builder.Services.AddScoped<IProducerService, ProducerService>();
builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IOrderService, OrderService>();

//Add busession
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movie}/{action=Index}/{id?}");

//Seed database
AppDbInitializer.Seed(app);

app.Run();
