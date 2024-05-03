namespace WebApplication2.Models;

public class Product
{
    public int IdProduct { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double? Price { get; set; } = null!;

    public virtual ICollection<Order> ProductOrders { get; set; } = new List<Order>();
    public virtual ICollection<Product_Warehouse> Product_Warehouses { get; set; } = new List<Product_Warehouse>();
}