namespace WebApplication2.Models;

public class Product_Warehouse
{
    public int IdProductWarehouse { get; set; }
    public int? Amount { get; set; } = null!;
    public double? Price { get; set; } = null!;
    public DateTime? CreatedAt { get; set; } = null!;
    
    public virtual Product Product { get; set; } = null!;
    public virtual Order Order { get; set; } = null!;
    public virtual Warehouse Warehouse { get; set; } = null!;
    
}