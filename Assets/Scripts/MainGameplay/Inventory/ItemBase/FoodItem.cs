using UnityEngine;

[CreateAssetMenu(menuName = "Items/Food")]
public class FoodItem: Item
{
    public float foodAmount;
    
    public override bool use(ref InventoryItem item, GameObject target, GameObject source)
    {
        if (!source) return false;
        var playerHealth = source.GetComponent<PlayerHealth>();
        if(!playerHealth) return false;
        if(playerHealth.stamina >= playerHealth.maxStamina - foodAmount) return false;
        
        item.increaseCount(-1);

        playerHealth.stamina += foodAmount;
        
        return true;
    }
}