using Lab6TestTask.Data;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// ProductService.
/// Implement methods here.
/// </summary>
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _dbContext;

    public ProductService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetProductAsync()
    {
        return await _dbContext.Products.OrderByDescending(p => p.Price).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        int quantityAmount = 1000;
        int year = 2025;

        return await _dbContext.Products.Where(p=>p.Quantity>quantityAmount && p.ReceivedDate.Year==year).ToListAsync();
    }
}
