using System;
using TMPro;
using UnityEngine;

public class SaveSlot: MonoBehaviour
{
    [SerializeField] 
    private string profileId;
    
    [SerializeField] 
    private GameObject hasDataObj, noDataObj;
    
    [SerializeField]
    private TextMeshProUGUI dayCountText;

    [SerializeField]
    private UnityEngine.UI.Button button;

    private void Awake()
    {
        button = GetComponent<UnityEngine.UI.Button>();
    }

    public void setData(GameData data)
    {
        if (data == null)
        {
            noDataObj.SetActive(true);
            hasDataObj.SetActive(false);
            return;
        }
        
        noDataObj.SetActive(false);
        hasDataObj.SetActive(true);
        
        dayCountText.text = "DAY COUNT: " + data.levelData.dayCount;
    }

    public string getProfileId()
    {
        return profileId;
    }

    public void setInteractable(bool interactable)
    {
        button.interactable = interactable;
    }
}