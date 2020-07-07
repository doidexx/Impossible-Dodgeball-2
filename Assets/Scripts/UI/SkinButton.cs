using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ID.UI
{
    public class SkinButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
    {
        public SkinType type;
        [SerializeField] private Material material = null;
        [SerializeField] private Sprite miniature = null;
        private SkinManager _manager = null;
        public Image overlay = null;

        private void Start()
        {
            _manager = FindObjectOfType<SkinManager>();
            var images = GetComponentsInChildren<Image>();
            images[0].sprite = miniature;
            overlay = images[1];
            _manager.Subscribe(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _manager.OnButtonEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _manager.OnButtonExit(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (material == null) return;
            _manager.OnButtonClick(this, type, material);
        }
    }
}
