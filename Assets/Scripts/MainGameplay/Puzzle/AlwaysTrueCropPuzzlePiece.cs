
public class AlwaysTrueCropPuzzlePiece: CropPuzzlePiece
{
    public override bool isComplete()
    {
        return true;
    }

    public override void saveData(ref GameData data)
    {}

    public override void loadData(GameData data)
    {
        completed = true;
    }

    public override void complete(InventoryItem newItem)
    {}
}