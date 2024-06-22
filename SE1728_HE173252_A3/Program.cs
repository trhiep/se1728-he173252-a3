using Microsoft.EntityFrameworkCore;
using SE1728_HE173252_A3.Models;
using SE1728_SignalR_CRUD;

namespace SE1728_HE173252_A3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBContext")));
            builder.Services.AddSession();
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.UseSession();

            app.MapHub<SignalRHub>("/SignalRHub");

            app.Run();
        }
    }
}