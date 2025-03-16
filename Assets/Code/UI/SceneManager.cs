using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject Loadscreen;
    private Transform parent;
    private GameObject newLoadscreen;


    private void Awake()
    {
        parent = GameObject.Find("UICanvas").transform;

        if (GameObject.Find("GameManager").GetComponent<GameManager>().GameManagerInLevel)
        {
            InstantiateLoadingscreen();
        }
    }

    public void LoadSceneByIndex(int index)
    {
        InstantiateLoadingscreen();

        if (index >= 0 && index < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        }
        else
        {
            Debug.LogError("Ungültiger Szenenindex: " + index);
        }
    }

    public void RestartScene()
    {
        InstantiateLoadingscreen();

        //Speichern der HighscoreListe bevor das Level neu geladen wird.
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().SaveHighscoreList();

        // Debug.Log("Restarting scene");
        Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
    }

    public void EndGame()
    {
        Application.Quit(); // Beendet das Spiel

        // Falls im Editor getestet, das Spiel beenden
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void InstantiateLoadingscreen()
    {
        newLoadscreen = Instantiate(Loadscreen, parent);
    }
}