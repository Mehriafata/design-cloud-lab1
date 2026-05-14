using Azure.Identity;
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
            var keyVaultName = "mf-lab1-kv-786";
            var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");

            builder.Configuration.AddAzureKeyVault(
                keyVaultUri, new DefaultAzureCredential());

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
            app.MapGet("/", () => Results.Redirect("/swagger"));

            /*automatic migration
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }*/

            app.Run();
        }
    }
}
