using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadscreenManager : MonoBehaviour
{
    [SerializeField] private GameObject Loadscreen;
    [SerializeField] private Transform parent;
    private GameObject newLoadscreen;


    private void Awake()
    {
        if (GameObject.Find("GameManager") != null)
        {
            HandleLoadingscreen(true);
        }
    }

    public void LoadSceneByIndex(int index)
    {
        HandleLoadingscreen(true);

        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            Debug.LogError("Ungültiger Szenenindex: " + index);
        }
    }

    public void HandleLoadingscreen(bool activ)
    {
        if (activ)
        {
            newLoadscreen = Instantiate(Loadscreen, parent);
        }
        else
        {
            //Loadscreen off
            Destroy(newLoadscreen);
        }
    }
}