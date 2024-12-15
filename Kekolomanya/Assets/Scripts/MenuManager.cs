using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public string gameScene;
    public string menuScene;
    public string PlayAgainScene;
    public Enemy enemy;
    public GameObject Door;

    void Start()
    {
        PlayerPunch.attackanim = false;
        KekoWcScene.Fight = true;    
    }

    public void StartingGameButton()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(PlayAgainScene);
    }

    public void BackMenuButton()
    {
        SceneManager.LoadScene(menuScene);
    }
}
