using DesignamolnlosningarLab1.Data;
using DesignamolnlosningarLab1.Endoints;
using Microsoft.EntityFrameworkCore;

namespace DesignamolnlosningarLab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // builder.Services.AddProblemDetails();
            // builder.Services.AddDbContext<AppDbContext>(options =>
            // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(
               builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            //app.UseExceptionHandler();

            app.UseHttpsRedirection();


            app.UseSwagger();
            app.UseSwaggerUI();



            app.MapProductEndpoints();

            //automatic migration
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}
