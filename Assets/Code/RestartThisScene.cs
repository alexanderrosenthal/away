using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartThisScene : MonoBehaviour
{
    public void RestartScene()
    {
        Debug.Log("Restarting scene");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}