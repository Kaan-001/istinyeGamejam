using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public string gameScene;
    public string menuScene;

    public void StartingGameButton()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void BackMenuButton()
    {
        SceneManager.LoadScene(menuScene);
    }
}
