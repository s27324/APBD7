namespace WebApplication2.Models;

public class Order
{
    public int IdOrder { get; set; }
    public int? Amount { get; set; } = null!;
    public DateTime? CreatedAt { get; set; } = null!;
    public DateTime FulfilledAt { get; set; }
    public virtual ICollection<Product_Warehouse> ProductsInWarehouse { get; set; } = new List<Product_Warehouse>();
    public virtual Product Product { get; set; } = null!;
}