using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TestAspDotNet.Data;
using TestAspDotNet.Repository;
using TestAspDotNet.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext <TestAspDotNetDbContext> (Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Transient);
builder.Services.AddTransient<TestAspDotNet.Repository.IAccount, AccountRepository>(p => new AccountRepository(builder.Services.BuildServiceProvider().GetService<TestAspDotNetDbContext>()));
builder.Services.AddTransient<TestAspDotNet.Repository.IUser, UserRepository>(p => new UserRepository(builder.Services.BuildServiceProvider().GetService<TestAspDotNetDbContext>()));
builder.Services.AddTransient<TestAspDotNet.Repository.IPost, PostRepository>(p => new PostRepository(builder.Services.BuildServiceProvider().GetService<TestAspDotNetDbContext>()));
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
