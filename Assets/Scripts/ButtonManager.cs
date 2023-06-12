using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector fade;

    enum State
    {
        GameStart, Exit, MainMenu
    }

    private State currentState = State.GameStart;


    public void Choice()
    {
        switch (currentState)
        {
            case State.GameStart:
                GameStart();
                break;
            case State.Exit:
                Exit();
                break;
            case State.MainMenu:
                ToMainMenu();
                break;
        }
    }
    
    public void ExitButton()
    {
        Time.timeScale = 1;
        currentState = State.Exit;
        fade.Play();
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void MainMenuButton()
    {
        Time.timeScale = 1;
        currentState = State.MainMenu;
        fade.Play();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Title");
    }

    public void GameStartButton()
    {
        currentState = State.GameStart;
        fade.Play();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Ingame");
    }
}
