using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : Menu
{
    [SerializeField] private SaveSlotMenu saveSlotMenu;
    
    [SerializeField] private UnityEngine.UI.Button newGameButton, loadGameButton;

    private void Start()
    {
        if (!DataPersistenceManager.INSTANCE.hasGameData())
        {
            loadGameButton.interactable = false;
        }
    }

    public void onNewGame()
    {
        saveSlotMenu.activateMenu(false);
        deactivateMenu();
    }

    public void onLoadGame()
    {
        saveSlotMenu.activateMenu(true);
        deactivateMenu();
    }
    
    public void activateMenu()
    {
        gameObject.SetActive(true);
    }

    public void deactivateMenu()
    {
        gameObject.SetActive(false);
    }
}
