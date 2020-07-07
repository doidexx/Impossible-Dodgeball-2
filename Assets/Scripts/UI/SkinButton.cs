using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkinButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    public SkinType type;
    [SerializeField] Material material = null;
    [SerializeField] Sprite miniature = null;
    SkinManager manager = null;
    public Image overlay = null;

    private void Start()
    {
        manager = FindObjectOfType<SkinManager>();
        var images = GetComponentsInChildren<Image>();
        images[0].sprite = miniature;
        overlay = images[1];
        manager.Subscribe(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        manager.OnButtonEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        manager.OnButtonExit(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (material == null) return;
        manager.OnButtonClick(this, type, material);
    }
}
