using DesignamolnlosningarLab1.Data;
using DesignamolnlosningarLab1.Data.Entities;
using DesignamolnlosningarLab1.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesignamolnlosningarLab1.Endoints
{
    public static class ProductEndpoints
    {
        public static WebApplication MapProductEndpoints(this WebApplication app)
        {

            app.MapGet("/api/products", (AppDbContext db) =>
            {
                var products = db.Products.ToList();
                return Results.Ok(products);
            });

            app.MapGet("/api/products/{id}", async (int id, AppDbContext db) =>
            {
                var product = await db.Products.FindAsync(id);

                if (product == null)
                    return Results.NotFound();
                return Results.Ok(product);
            });

            app.MapPut("/api/products/{id}", (int id, Product updateProduct, AppDbContext db) =>
            {
                var product = db.Products.Find(id);

                if (product == null)
                    return Results.NotFound();

                product.ProductName = updateProduct.ProductName;
                product.ProductPrice = updateProduct.ProductPrice;

                db.SaveChanges();

                return Results.Ok(product);
            });
            app.MapPost("/api/products", (Product product, AppDbContext db)
            =>
            {
                db.Products.Add(product);
                db.SaveChanges();
                return Results.Ok(product);
            });

            app.MapDelete("/api/products/{id}", (int id, AppDbContext db) =>
            {
                var product = db.Products.Find(id);

                if (product == null)
                    return Results.NotFound();
                db.Products.Remove(product);
                db.SaveChanges();

                return Results.Ok();
            });

            app.MapPost("/api/upload", async ([FromForm] IFormFile file, BlobService blobService) =>
             {
                 if (file == null || file.Length == 0)
                     return Results.BadRequest("No file uploaded");

                 var url = await blobService.UploadFileAsync(file);
                 return Results.Ok(url);

             });

            return app;
        }
    }


}
