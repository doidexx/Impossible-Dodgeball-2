using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    List<TabButton> tabButtons;
    [SerializeField] Sprite active = null;
    [SerializeField] Sprite hover = null;
    [SerializeField] Sprite inactive = null;
    TabButton selected = null;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();

        tabButtons.Add(button);
    }

    public void OnTabClick(TabButton button)
    {
        selected = button;
        ResetTabButtons();
        if (!active) return;
        button.background.sprite = active;
        if (button.window == null) return;
        button.window.SetActive(true);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabButtons();
        if (!hover) return;
        if (selected == null || selected != button)
            button.background.sprite = hover;
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabButtons();
        if (!inactive) return;
        if (selected == button) return;
        button.background.sprite = inactive;
    }

    private void ResetTabButtons()
    {
        foreach (TabButton button in tabButtons)
        {
            if (button == selected) continue;
            button.background.sprite = inactive;
            if (button.window == null) return;
            button.window.SetActive(false);
        }
    }
}
