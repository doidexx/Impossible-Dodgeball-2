using System.Collections.Generic;
using UnityEngine;

namespace ID.UI
{
    public class TabManager : MonoBehaviour
    {
        private List<TabButton> _tabButtons;
        [SerializeField] private Sprite active = null;
        [SerializeField] private Sprite hover = null;
        [SerializeField] private Sprite inactive = null;
        private TabButton _selected = null;

        public void Subscribe(TabButton button)
        {
            if (_tabButtons == null)
                _tabButtons = new List<TabButton>();

            _tabButtons.Add(button);
        }

        public void OnTabClick(TabButton button)
        {
            _selected = button;
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
            if (_selected == null || _selected != button)
                button.background.sprite = hover;
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabButtons();
            if (!inactive) return;
            if (_selected == button) return;
            button.background.sprite = inactive;
        }

        private void ResetTabButtons()
        {
            foreach (TabButton button in _tabButtons)
            {
                if (button == _selected) continue;
                button.background.sprite = inactive;
                if (button.window == null) return;
                button.window.SetActive(false);
            }
        }
    }
}
