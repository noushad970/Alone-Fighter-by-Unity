using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool startGame;
    public static MainMenu instance;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        instance = this;
    }
    public void OnStartButton()
    {
        Debug.Log("Starting");
        startGame = true;
        SceneManager.LoadScene("Gameplay");
    }
    public void OnQuitButton()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
