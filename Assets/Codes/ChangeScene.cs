using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "Player1":
                SceneManager.LoadScene("player1");
                break;
            case "Player1,2":
                SceneManager.LoadScene("player1,2");
                break;
            case "Ai":
                SceneManager.LoadScene("ai");
                break;
            case "Computer":
                SceneManager.LoadScene("computer");
                break;
            case "Menubtn":
                SceneManager.LoadScene("Menu");
                Time.timeScale = 1;
                break;

        }
    }
    public void ExitScene()
    {
        Application.Quit();
    }
}
