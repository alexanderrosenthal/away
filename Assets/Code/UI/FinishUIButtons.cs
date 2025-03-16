using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishUIButtons : MonoBehaviour
{
    public void RestartLevel()
    {
        GameObject.Find("SceneManager").GetComponent<SceneManager>().RestartScene();
    }

    // Update is called once per frame
    public void BackToMenu()
    {
        GameObject.Find("SceneManager").GetComponent<SceneManager>().LoadSceneByIndex(0);
    }
}