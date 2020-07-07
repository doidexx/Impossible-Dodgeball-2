using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ID.UI
{
    public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Image background = null;
        public TabManager tabManager = null;
        public GameObject window = null;

        public bool activateOnStart = false;

        private void Start()
        {
            background = GetComponent<Image>();
            tabManager.Subscribe(this);
            if (activateOnStart)
                tabManager.OnTabClick(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            tabManager.OnTabClick(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tabManager.OnTabEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tabManager.OnTabExit(this);
        }
    }
}
