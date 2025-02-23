
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void LoadSceneByIndex(int index)
    {
        Debug.Log("print" + index);
        
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            Debug.LogError("Ungültiger Szenenindex: " + index);
        }
    }
}