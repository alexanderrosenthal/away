using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> uiPrefabs; // Liste der UI-Elemente
    [SerializeField] public Canvas uiCanvas; // Das UI Canvas, unter dem das Element erscheinen soll

    public void SpawnUIPrefab(int choosenUI)
    {
        if (uiPrefabs != null && uiPrefabs.Count > 0 && uiCanvas != null)
        {
            SpawnUIElement(0); // Standardmäßig das erste Element instanziieren
        }
        else
        {
            Debug.LogError("UI Prefabs Liste ist leer oder Canvas nicht zugewiesen!");
        }
    }
    private void SpawnUIElement(int index)
    {
        if (index >= 0 && index < uiPrefabs.Count)
        {
            GameObject newUIElement = Instantiate(uiPrefabs[index]);
            newUIElement.transform.SetParent(uiCanvas.transform, false); // Setzt das Canvas als Parent, ohne die Skalierung zu beeinflussen
        }
        else
        {
            Debug.LogError("Ungültiger Index für UI Prefab!");
        }
    }
}