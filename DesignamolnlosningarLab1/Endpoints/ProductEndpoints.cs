using DesignamolnlosningarLab1.Data;
using DesignamolnlosningarLab1.Data.Entities;

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

            return app;
        }
    }


}
