using System;
using System.Collections.Generic;
using UnityEngine;

namespace ID.Core
{
    public enum Screen
    {
        Home,
        Gameplay,
        Skins,
        Pause,
        Records,
        GameOver
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
            screens.Add(Screen.Home, home);
            screens.Add(Screen.Gameplay, gameplay);
            screens.Add(Screen.Skins, skins);
            screens.Add(Screen.Pause, pause);
            screens.Add(Screen.Records, records);
            screens.Add(Screen.GameOver, gameOver);
            OpenScreen(Screen.Home);
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
            OpenScreen(Screen.Home);
            FindObjectOfType<GameManager>().OnHomeScreen();
        }

        public void PauseGame(bool value)
        {
            if (value)
            {
                OpenScreen(Screen.Pause);
                Time.timeScale = 0;
            }
            else
            {
                OpenScreen(Screen.Gameplay);
                Time.timeScale = 1;
            }
        }

        public void SkinsScreen()
        {
            OpenScreen(Screen.Skins);
        }

        public void RecordsScreen()
        {
            OpenScreen(Screen.Records);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}