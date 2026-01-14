namespace Domain.Entities;

public class BurgerIngredient
{
    public Guid BurgerId { get; private set; }
    public Guid IngredientId { get; private set; }
    public int Quantity { get; private set; }

    public Burger Burger { get; private set; } = null!;
    public Ingredient Ingredient { get; private set; } = null!;

    public BurgerIngredient(Guid burgerId, Guid ingredientId, int quantity)
    {
        BurgerId = burgerId;
        IngredientId = ingredientId;
        Quantity = quantity;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));
        
        Quantity = quantity;
    }
}
