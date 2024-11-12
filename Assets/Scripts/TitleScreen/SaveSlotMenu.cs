using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlotMenu: Menu
{
    [SerializeField] private TitleScreen titleScreen;
    [SerializeField] private UnityEngine.UI.Button backButton;
    
    private SaveSlot[] slots;

    private bool isLoading;

    private void Awake()
    {
        slots = GetComponentsInChildren<SaveSlot>();
    }

    public void onBackClicked()
    {
        titleScreen.activateMenu();
        deactivateMenu();
    }
    
    public void activateMenu(bool isLoading)
    {
        gameObject.SetActive(true);
        var allData = DataPersistenceManager.INSTANCE.getAllData();

        this.isLoading = isLoading;

        GameObject firstSelected = backButton.gameObject;
        foreach (var slot in slots)
        {
            allData.TryGetValue(slot.getProfileId(), out var profileData);
            slot.setData(profileData);

            if (profileData == null && this.isLoading)
            {
                slot.setInteractable(false);
                continue;
            }
            
            slot.setInteractable(true);
            if (firstSelected.Equals(backButton.gameObject))
            {
                firstSelected = slot.gameObject;
            }
        }

        StartCoroutine(setFirstSelected(firstSelected));
    }

    public void onSaveSlotClicked(SaveSlot slot)
    {
        disableMenuButtons();
        DataPersistenceManager.INSTANCE.changeSelectedProfileId(slot.getProfileId());
        if(!isLoading) DataPersistenceManager.INSTANCE.newGame();
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void disableMenuButtons()
    {
        foreach (var slot in slots)
        {
            slot.setInteractable(false);
        }
        
        backButton.interactable = false;
    }
    
    public void deactivateMenu()
    {
        gameObject.SetActive(false);
    }
}