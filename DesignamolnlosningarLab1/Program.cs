using Azure.Identity;
using DesignamolnlosningarLab1.Data;
using DesignamolnlosningarLab1.Endoints;
using DesignamolnlosningarLab1.Services;
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
            builder.Services.AddScoped<BlobService>();

            builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(
               builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();



            app.UseHttpsRedirection();


            app.UseSwagger();
            app.UseSwaggerUI();



            app.MapProductEndpoints();
            app.MapGet("/", () => Results.Redirect("/swagger"));



            app.Run();
        }
    }
}
