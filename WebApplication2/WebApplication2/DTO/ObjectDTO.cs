﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTO;

public class ObjectDTO
{
    [Required]
    public int IdProduct { get; set; }
    [Required]
    public int IdWarehouse { get; set; }
    [Range(1, Int32.MaxValue)]
    public int Amount { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}