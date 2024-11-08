using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")] [SerializeField]
    private string fileName;
    
    private GameData data;
    private List<IDataPersistence> dataPersistenceObjects = new();
    private FileDataHandler dataHandler;
    
    public static DataPersistenceManager INSTANCE { get; private set; }
    
    private void Awake() {
        if (INSTANCE != null) {
            Debug.LogWarning("Found more than one Data Persistence Manager");
        }
        
        INSTANCE = this;
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistenceObjects = findAllDataPersistenceObjects();
        loadGame();
    }

    private List<IDataPersistence> findAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> persistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        
        return new List<IDataPersistence>(persistenceObjects);
    }

    private void OnApplicationQuit()
    {
        saveGame();
    }

    public void newGame()
    {
        data = new GameData();
    }

    public void loadGame()
    {
        data = dataHandler.load();
        
        if (data == null)
        {
            newGame();
        }

        foreach (IDataPersistence dataObj in dataPersistenceObjects)
        {
            dataObj.loadData(data);
        }
    }

    public void saveGame()
    {
        foreach (IDataPersistence dataObj in dataPersistenceObjects)
        {
            dataObj.saveData(ref data);
        }
        
        dataHandler.save(data);
    }
}
