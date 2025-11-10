using BugStore.Data;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Customers;

public class CustomerHandler
{
    private readonly AppDbContext _db;

    public CustomerHandler(AppDbContext db) => _db = db;

    // CREATE
    public async Task<Responses.Customers.Create> Handle(Requests.Customers.Create request)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            BirthDate = request.BirthDate
        };

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();

        return new Responses.Customers.Create
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            BirthDate = customer.BirthDate
        };
    }

    // UPDATE
    public async Task<Responses.Customers.Update?> Handle(Requests.Customers.Update request)
    {
        var customer = await _db.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (customer == null)
            return null;

        customer.Name = request.Name;
        customer.Email = request.Email;
        customer.Phone = request.Phone;
        customer.BirthDate = request.BirthDate;

        _db.Customers.Update(customer);
        await _db.SaveChangesAsync();

        return new Responses.Customers.Update
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            BirthDate = customer.BirthDate
        };
    }

    // DELETE
    public async Task<Responses.Customers.Delete> Handle(Requests.Customers.Delete request)
    {
        var customer = await _db.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (customer == null)
            return new Responses.Customers.Delete { Success = false, Message = "Cliente não encontrado." };

        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync();

        return new Responses.Customers.Delete { Success = true, Message = "Cliente removido com sucesso." };
    }

    // GET (todos)
    public async Task<List<Responses.Customers.Get>> Handle(Requests.Customers.Get request)
    {
        var customers = await _db.Customers.AsNoTracking().ToListAsync();

        return customers.Select(x => new Responses.Customers.Get
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
            Phone = x.Phone,
            BirthDate = x.BirthDate
        }).ToList();
    }

    // GET BY ID
    public async Task<Responses.Customers.GetById?> Handle(Requests.Customers.GetById request)
    {
        var customer = await _db.Customers.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (customer == null)
            return null;

        return new Responses.Customers.GetById
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            BirthDate = customer.BirthDate
        };
    }
}
