using Lab6TestTask.Data;
using Lab6TestTask.Enums;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// WarehouseService.
/// Implement methods here.
/// </summary>
public class WarehouseService : IWarehouseService
{
    private readonly ApplicationDbContext _dbContext;

    public WarehouseService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Warehouse> GetWarehouseAsync()
    {
        var readyWarehousesWithValues = await _dbContext.Warehouses.
            Select(w => new {
                Warehouse = w,
                TotalValue = w.Products
                .Where(p => p.Status == ProductStatus.ReadyForDistribution)
                .Sum(p => p.Price * p.Quantity)
            })
            .ToListAsync();

        return readyWarehousesWithValues.OrderByDescending(x => x.TotalValue).Select(x => x.Warehouse).FirstOrDefault();
    }

    public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
    {
        DateTime startDate = new DateTime(2025, 4, 1);
        DateTime endDate = new DateTime(2025, 6, 30);

        return await _dbContext.Warehouses.Where(w => w.Products.Any(p => p.ReceivedDate >= startDate && p.ReceivedDate <= endDate)).ToListAsync();
    }
}
