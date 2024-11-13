using System;

public class GameData
{
    
    [NonSerialized] public Registry registry;
    
    public LevelContainerData levelContainerData;
    public LevelData levelData;
    public PlayerData playerData;
    public LevelCropData levelCropData;

    public GameData()
    {
        levelContainerData = new LevelContainerData();
        levelData = new LevelData();
        playerData = new PlayerData();
        levelCropData = new LevelCropData();
    }
}