using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartThisScene : MonoBehaviour
{    public void RestartScene()
    {
        //Speichern der HighscoreListe bevor das Level neu geladen wird.
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().SaveHighscoreList();
        
        // Debug.Log("Restarting scene");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}