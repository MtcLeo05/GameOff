using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private bool initializeDataIfNull;
    
    public Registry registry;

    [SerializeField]
    private string profileId;
    private GameData data;
    private List<IDataPersistence> dataPersistenceObjects = new();
    private FileDataHandler dataHandler;
    
    public static DataPersistenceManager INSTANCE { get; private set; }
    
    private void Awake() {
        if (INSTANCE != null) {
            Destroy(gameObject);
            return;
        }
        
        INSTANCE = this;
        DontDestroyOnLoad(gameObject);
        
        dataHandler = new FileDataHandler(Application.persistentDataPath);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
        SceneManager.sceneUnloaded += onSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
        SceneManager.sceneUnloaded -= onSceneUnloaded;
    }

    public void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataPersistenceObjects = findAllDataPersistenceObjects();
        loadGame();
    }

    public void onSceneUnloaded(Scene scene)
    {
        saveGame();
    }
    
    private void OnApplicationQuit()
    {
        saveGame();
    }

    private List<IDataPersistence> findAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> persistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        
        return new List<IDataPersistence>(persistenceObjects);
    }

    public void changeSelectedProfileId(string profileId)
    {
        this.profileId = profileId;
        loadGame();
    }
    
    public bool hasGameData()
    {
        foreach (var (key, value) in getAllData())
        {
            if (value != null)
            {
                return true;
            }
        }

        return false;
    }
    
    public void newGame()
    {
        data = new GameData
        {
            registry = registry
        };
        
        saveGame();
    }

    public void loadGame()
    {
        data = dataHandler.load(profileId);
        
        if (data == null)
        {
            if (initializeDataIfNull)
            {
                data = new();
            }
            else
            {
                return;
            }
        }
        
        data.registry = registry;
        foreach (IDataPersistence dataObj in dataPersistenceObjects)
        {
            dataObj.loadData(data);
        }
    }

    public void saveGame()
    {
        if (data == null)
        {
            return;
        }
        
        foreach (IDataPersistence dataObj in dataPersistenceObjects)
        {
            dataObj.saveData(ref data);
        }
        
        dataHandler.save(data, profileId);
    }

    public Dictionary<string, GameData> getAllData()
    {
        return dataHandler.loadAll();
    }
}
