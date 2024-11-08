using UnityEngine;

[CreateAssetMenu(menuName = "Items/Food")]
public class FoodItem: Item
{
    public float foodAmount;


    public override void use(ref InventoryItem item)
    {
        item.increaseCount(-1);

        if (item.count <= 0)
        {
            Destroy(item.gameObject);
        }
    }
}