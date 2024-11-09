using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class NewGameButton : MonoBehaviour
{
    UnityEngine.UI.Button button;
    void Start()
    {
        button = GetComponent<UnityEngine.UI.Button>();

        button.onClick.AddListener(buttonClick);
    }

    void buttonClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
