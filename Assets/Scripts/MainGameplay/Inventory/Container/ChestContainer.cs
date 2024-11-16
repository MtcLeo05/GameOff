
public class ChestContainer: ContainerBase
{
    public InventoryManager inventory;
    
    public override void openInventory()
    {
        inventory.inventoryHud.SetActive(true);
    }

    public override void close()
    {
        base.close();
        inventory.inventoryHud.SetActive(false);
    }
}