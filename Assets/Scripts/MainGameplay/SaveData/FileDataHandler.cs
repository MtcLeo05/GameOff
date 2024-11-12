using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string filePath;
    private string fileName;

    public FileDataHandler(string filePath, string fileName)
    {
        this.filePath = filePath;
        this.fileName = fileName;
    }

    public GameData load(string id)
    {
        string path = Path.Combine(filePath, id, fileName);
        GameData loadedData = null;

        if (File.Exists(path))
        {
            try
            {
                string data;
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        data = reader.ReadToEnd();
                    }
                    
                    loadedData = JsonUtility.FromJson<GameData>(data);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error while loading data: " + path + "\n" + e);
            }
        }
        
        return loadedData;
    }

    public Dictionary<string, GameData> loadAll()
    {
        Dictionary<string, GameData> loadedData = new Dictionary<string, GameData>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(filePath).EnumerateDirectories();
        
        foreach (DirectoryInfo info in dirInfos)
        {
            string profileId = info.Name;
            
            string fullPath = Path.Combine(filePath, profileId, fileName);
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
        string path = Path.Combine(filePath, id, fileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            
            string data = JsonUtility.ToJson(gameData, true);

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
} 