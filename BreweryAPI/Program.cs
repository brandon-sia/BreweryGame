
using BreweryAPI.Data;
using BreweryAPI.Interfaces;
using BreweryAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace BreweryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IBeerRepository, BeerRepository>();
            builder.Services.AddScoped<IBreweryRepository, BreweryRepository>();
            builder.Services.AddScoped<IWholesalerRepository, WholeSalerRepository>();
            builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }

    public class Startup
    {
        public void ConfigureLogging(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole(); // Add console logging provider
                                             // Add other providers as needed
            });


        }

    }
}
