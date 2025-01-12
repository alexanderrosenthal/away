using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartThisScene : MonoBehaviour
{
    [Header("To be set per Scene")]
    [SerializeField] private GameObject ScoreManager;

    public void RestartScene()
    {
        //Speichern der HighscoreListe bevor das Level neu geladen wird.
        ScoreManager.GetComponent<ScoreManager>().SaveHighscoreList();
        
        // Debug.Log("Restarting scene");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}