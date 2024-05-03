namespace WebApplication2.Models;

public class Warehouse
{
    public int IdWarehouse { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    
    public virtual ICollection<Product_Warehouse> Product_Warehouses { get; set; } = new List<Product_Warehouse>();
}