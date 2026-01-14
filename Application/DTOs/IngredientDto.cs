namespace Application.DTOs;

public class IngredientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal StockQuantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateIngredientDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal StockQuantity { get; set; }
    public string Unit { get; set; } = string.Empty;
}

public class UpdateIngredientDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? StockQuantity { get; set; }
}
