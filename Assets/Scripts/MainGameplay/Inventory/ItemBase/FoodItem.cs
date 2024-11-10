using UnityEngine;

[CreateAssetMenu(menuName = "Items/Food")]
public class FoodItem: Item
{
    public float foodAmount;


    public override bool use(ref InventoryItem item, GameObject target)
    {
        item.increaseCount(-1);

        if (item.count <= 0)
        {
            Destroy(item.gameObject);
        }

        return true;
    }
}