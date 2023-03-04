using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SpawnSettings spawnSettings;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetNumNodes(string value)
    {
        spawnSettings.NumNodes = int.Parse(value);

    }

    public void SetNumUsers(string value)
    {
        spawnSettings.NumUsers = int.Parse(value);

    }
}
