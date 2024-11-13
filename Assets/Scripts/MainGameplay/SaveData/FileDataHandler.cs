using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private readonly string filePath;

    private const string containerName = "container.kirakira";
    private const string levelName = "level.dokidoki";
    private const string playerName = "player.mochimochi";
    private const string cropName = "crop.puyopuyo";
    
    public FileDataHandler(string filePath)
    {
        this.filePath = filePath;
    }

    public GameData load(string id)
    {
        GameData loadedData = new GameData();
        
        loadContainer(id, ref loadedData);
        loadLevel(id, ref loadedData);
        loadPlayer(id, ref loadedData);
        loadCrop(id, ref loadedData);
        
        return loadedData;
    }
    public Dictionary<string, GameData> loadAll()
    {
        Dictionary<string, GameData> loadedData = new Dictionary<string, GameData>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(filePath).EnumerateDirectories();
        
        foreach (DirectoryInfo info in dirInfos)
        {
            string profileId = info.Name;
            
            string fullPath = Path.Combine(filePath, profileId);
            if (!File.Exists(fullPath)) continue;

            GameData profileData = load(profileId);

            if (profileData != null)
            {
                loadedData.Add(profileId, profileData);
            }
        }
        
        return loadedData;
    }

    public void save(GameData gameData, string id)
    {
        saveContainer(id, gameData);
        saveLevel(id, gameData);
        savePlayer(id, gameData);
        saveCrop(id, gameData);
    }

    private void saveContainer(string id, GameData savedData)
    {
        string path = Path.Combine(filePath, id, containerName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            
            string data = JsonUtility.ToJson(savedData.levelContainerData, true);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error while saving data: " + path + "\n" + e);
        }
    }
    private void saveLevel(string id, GameData savedData)
    {
        string path = Path.Combine(filePath, id, levelName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            
            string data = JsonUtility.ToJson(savedData.levelData, true);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error while saving data: " + path + "\n" + e);
        }
    }
    private void savePlayer(string id, GameData savedData)
    {
        string path = Path.Combine(filePath, id, playerName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            
            string data = JsonUtility.ToJson(savedData.playerData, true);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error while saving data: " + path + "\n" + e);
        }
    }
    private void saveCrop(string id, GameData savedData)
    {
        string path = Path.Combine(filePath, id, cropName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            
            string data = JsonUtility.ToJson(savedData.levelCropData, true);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error while saving data: " + path + "\n" + e);
        }
    }
    
    private void loadContainer(string id, ref GameData loadedData)
    {
        string path = Path.Combine(filePath, id, containerName);
        if (File.Exists(path))
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    string data;
                    using (var reader = new StreamReader(stream))
                    {
                        data = reader.ReadToEnd();
                    }
                    
                    loadedData.levelContainerData = JsonUtility.FromJson<LevelContainerData>(data);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error while loading data: " + path + "\n" + e);
            }
        }
    }
    private void loadLevel(string id, ref GameData loadedData)
    {
        string path = Path.Combine(filePath, id, levelName);
        if (File.Exists(path))
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    string data;
                    using (var reader = new StreamReader(stream))
                    {
                        data = reader.ReadToEnd();
                    }
                    
                    loadedData.levelData = JsonUtility.FromJson<LevelData>(data);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error while loading data: " + path + "\n" + e);
            }
        }
    }
    private void loadPlayer(string id, ref GameData loadedData)
    {
        string path = Path.Combine(filePath, id, playerName);
        if (File.Exists(path))
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    string data;
                    using (var reader = new StreamReader(stream))
                    {
                        data = reader.ReadToEnd();
                    }
                    
                    loadedData.playerData = JsonUtility.FromJson<PlayerData>(data);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error while loading data: " + path + "\n" + e);
            }
        }
    }
    private void loadCrop(string id, ref GameData loadedData)
    {
        string path = Path.Combine(filePath, id, cropName);
        if (File.Exists(path))
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    string data;
                    using (var reader = new StreamReader(stream))
                    {
                        data = reader.ReadToEnd();
                    }
                    
                    loadedData.levelCropData = JsonUtility.FromJson<LevelCropData>(data);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error while loading data: " + path + "\n" + e);
            }
        }
    }
} 