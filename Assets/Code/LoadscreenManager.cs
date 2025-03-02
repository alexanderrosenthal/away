
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class LoadscreenManager : MonoBehaviour
{
    [SerializeField] private GameObject Loadscreen;
    [SerializeField] private Transform parent;
    private GameObject newLoadscreen;

    public void LoadSceneByIndex(int index)
    {
        Debug.Log("print" + index);

        HandleLoadingscreen(true);

        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
            HandleLoadingscreen(false);
        }
        else
        {
            Debug.LogError("Ungültiger Szenenindex: " + index);
        }
    }

    private void HandleLoadingscreen(bool activ)
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