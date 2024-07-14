using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartThisScene : MonoBehaviour
{
    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}