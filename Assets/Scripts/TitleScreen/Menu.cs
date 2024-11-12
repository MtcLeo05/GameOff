using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu: MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;

    protected virtual void OnEnable()
    {
        StartCoroutine(setFirstSelected(firstSelected));
    }

    protected IEnumerator setFirstSelected(GameObject o)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(o);
    }
}
