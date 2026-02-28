using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace eTickets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

           builder.Services.AddDbContext<AppDbContext>(options =>
           {

               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
           });

            builder.Services.AddScoped<IActorServices, ActorServices>();
            builder.Services.AddScoped<IProducerService, ProducerService>();
            builder.Services.AddScoped<ICinemaService, CinemaService>();
            builder.Services.AddScoped<IMovieService, MovieService>();




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
                pattern: "{controller=Movies}/{action=Index}/{id?}");


            AppDbInitializer.Seed(app);

            app.Run();
        }
    }
}
