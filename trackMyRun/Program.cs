
using Microsoft.EntityFrameworkCore;
using trackMyRun.DbEntities;

namespace trackMyRun
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connString = builder.Configuration.GetConnectionString("TrackMyRunDB" ?? "");

            builder.Services.AddDbContext<TrackMyRunContext>(
                options => options.UseMySql(connString, ServerVersion.AutoDetect(connString))
                );
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
}