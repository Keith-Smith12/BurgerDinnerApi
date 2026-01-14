namespace Domain.Entities;

public class Burger
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public bool IsAvailable { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<BurgerIngredient> _ingredients = new();
    public IReadOnlyCollection<BurgerIngredient> Ingredients => _ingredients.AsReadOnly();

    public Burger(string name, string description, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        IsAvailable = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be greater than 0", nameof(price));
        
        Price = price;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddIngredient(Ingredient ingredient, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));

        var existingIngredient = _ingredients.FirstOrDefault(i => i.IngredientId == ingredient.Id);
        if (existingIngredient != null)
        {
            existingIngredient.UpdateQuantity(quantity);
        }
        else
        {
            _ingredients.Add(new BurgerIngredient(Id, ingredient.Id, quantity));
        }
        
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveIngredient(Guid ingredientId)
    {
        var ingredient = _ingredients.FirstOrDefault(i => i.IngredientId == ingredientId);
        if (ingredient != null)
        {
            _ingredients.Remove(ingredient);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
