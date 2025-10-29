using BugStore.Data;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Products;

public class ProductHandler
{
    private readonly AppDbContext _db;
    public ProductHandler(AppDbContext db) => _db = db;

    // CREATE
    public async Task<Responses.Products.Create> Handle(Requests.Products.Create request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Slug = request.Slug,
            Price = request.Price
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        return new Responses.Products.Create
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Slug = product.Slug,
            Price = product.Price
        };
    }

    // UPDATE
    public async Task<Responses.Products.Update?> Handle(Requests.Products.Update request)
    {
        var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (product == null)
            return null;

        product.Title = request.Title;
        product.Description = request.Description;
        product.Slug = request.Slug;
        product.Price = request.Price;

        _db.Products.Update(product);
        await _db.SaveChangesAsync();

        return new Responses.Products.Update
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Slug = product.Slug,
            Price = product.Price
        };
    }

    // DELETE
    public async Task<Responses.Products.Delete> Handle(Requests.Products.Delete request)
    {
        var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (product == null)
            return new Responses.Products.Delete { Success = false, Message = "Produto não encontrado." };

        _db.Products.Remove(product);
        await _db.SaveChangesAsync();

        return new Responses.Products.Delete { Success = true, Message = "Produto removido com sucesso." };
    }

    // GET (todos)
    public async Task<List<Responses.Products.Get>> Handle(Requests.Products.Get request)
    {
        var products = await _db.Products.AsNoTracking().ToListAsync();

        return products.Select(x => new Responses.Products.Get
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Slug = x.Slug,
            Price = x.Price
        }).ToList();
    }

    // GET BY ID
    public async Task<Responses.Products.GetById?> Handle(Requests.Products.GetById request)
    {
        var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (product == null)
            return null;

        return new Responses.Products.GetById
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Slug = product.Slug,
            Price = product.Price
        };
    }
}
