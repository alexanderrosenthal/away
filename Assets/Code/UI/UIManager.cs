using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> uiPrefabs; // Liste der UI-Elemente
    [SerializeField] public Canvas uiCanvas; // Das UI Canvas, unter dem das Element erscheinen soll

    private Transform searchedUI;

    public void SpawnUIPrefab(int choosenUI)
    {
        if (uiPrefabs != null && uiPrefabs.Count > 0 && uiCanvas != null)
        {
            SpawnUIElement(choosenUI); // Standardmäßig das erste Element instanziieren
        }
        else
        {
            Debug.LogError("UI Prefabs Liste ist leer oder Canvas nicht zugewiesen!");
        }
    }

    private void SpawnUIElement(int index)
    {
        GameObject newUIElement = Instantiate(uiPrefabs[index]);
        newUIElement.transform.SetParent(uiCanvas.transform, false); // Setzt das Canvas als Parent, ohne die Skalierung zu beeinflussen
    }

    public void KillUI(string searchedUIName)
    {
        SearchUI(searchedUIName);

        if (!searchedUI)
        {
            Debug.Log("No UI with Name" + searchedUIName + "Found");
        }
        else
        {
            Destroy(searchedUI.gameObject); // Hier wird das GameObject zerstört
            return;
        }
    }

    public Transform FindUI(string searchedUIName)
    {
        SearchUI(searchedUIName);

        if (!searchedUI)
        {
            Debug.Log("No UI with Name" + searchedUIName + "Found");
            return null; // Falls kein UI-Element gefunden wurde
        }
        else
        {
            return searchedUI;
        }
    }

    public void ActivateUI(string searchedUIName)
    {
        SearchUI(searchedUIName);
        searchedUI.gameObject.SetActive(true);
    }


    public void DeActivateUI(string searchedUIName)
    {
        SearchUI(searchedUIName);
        searchedUI.gameObject.SetActive(false);
    }

    private void SearchUI(string searchedUIName)
    {
        Transform UITransform = uiCanvas.transform;

        for (int i = 0; i < UITransform.childCount; i++)
        {
            if (UITransform.GetChild(i).name == searchedUIName)
            {
                searchedUI = UITransform.GetChild(i);

                break;
            }
        }
    }
}