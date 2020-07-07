using System;
using System.Collections.Generic;
using UnityEngine;

public enum Screen
{
    home,
    gameplay,
    skins,
    pause,
    records,
    gameOver
}

public class UIManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] Canvas home = null;
    [SerializeField] Canvas gameplay = null;
    [SerializeField] Canvas skins = null;
    [SerializeField] Canvas pause = null;
    [SerializeField] Canvas records = null;
    [SerializeField] Canvas gameOver = null;

    public Dictionary<Screen, Canvas> screens;

    private void Start()
    {
        screens = new Dictionary<Screen, Canvas>();
        screens.Add(Screen.home, home);
        screens.Add(Screen.gameplay, gameplay);
        screens.Add(Screen.skins, skins);
        screens.Add(Screen.pause, pause);
        screens.Add(Screen.records, records);
        screens.Add(Screen.gameOver, gameOver);
        OpenScreen(Screen.home);
    }

    public void OpenScreen(Screen screenToOpen)
    {
        foreach (Screen screen in Enum.GetValues(typeof(Screen)))
        {
            if (screens[screen] == null) continue;
            screens[screen].gameObject.SetActive(false);
            //screens[screen].enabled = false;
        }
        screens[screenToOpen].gameObject.SetActive(true);
        //screens[screenToOpen].enabled = true;
    }

    public void HomeScreen()
    {
        OpenScreen(Screen.home);
        FindObjectOfType<GameManager>().OnHomeScreen();
    }

    public void PauseGame(bool value)
    {
        if (value)
        {
            OpenScreen(Screen.pause);
            Time.timeScale = 0;
        }
        else
        {
            OpenScreen(Screen.gameplay);
            Time.timeScale = 1;
        }
    }

    public void SkinsScreen()
    {
        OpenScreen(Screen.skins);
    }

    public void RecordsScreen()
    {
        OpenScreen(Screen.records);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

