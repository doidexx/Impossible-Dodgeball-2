using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [Header("Previews")]
    [SerializeField] SkinnedMeshRenderer characterPreview = null;
    [SerializeField] Renderer ballPreview = null;

    [Header("GameObjects")]
    [SerializeField] SkinnedMeshRenderer character = null;

    [Header("Overlay Sprites")]
    [SerializeField] Sprite idle = null;
    [SerializeField] Sprite highlight = null;
    [SerializeField] Sprite selected = null;

    List<SkinButton> buttons = null;
    SkinButton selectedButton = null;
    SkinButton previewing = null;
    SkinButton selectedBallButton = null;
    SkinButton previewingBallButton = null;

    public void Subscribe(SkinButton button)
    {
        if (buttons == null)
            buttons = new List<SkinButton>();
        buttons.Add(button);
    }

    public void OnButtonEnter(SkinButton button)
    {
        if (selectedButton == button || highlight == null || selectedBallButton == button) return;
        button.overlay.sprite = highlight;
    }

    public void OnButtonExit(SkinButton button)
    {
        if (selectedButton == button || idle == null || selectedBallButton == button) return;
        button.overlay.sprite = idle;
    }

    public void OnButtonClick(SkinButton button, SkinType type, Material material)
    {
        if (type == SkinType.Skin)
        {
            if (previewing == button)
                OnButtonSelect(button, SkinType.Skin, material);
            else
            {
                ChangeCharacterSkin(material, characterPreview);
                previewing = button;
            }
        }

        if (type == SkinType.Ball)
        {
            if (previewingBallButton == button)
                OnButtonSelect(button, SkinType.Ball, material);
            else
            {
                ballPreview.material = material;
                previewingBallButton = button;
            }
        }
    }

    public void OnButtonSelect(SkinButton button, SkinType type, Material material)
    {
        ResetButtons(type);
        if (type == SkinType.Skin)
        {
            button.overlay.sprite = selected;
            selectedButton = button;
            ChangeCharacterSkin(material, character);
        }
        else if (type == SkinType.Ball)
        {
            button.overlay.sprite = selected;
            selectedBallButton = button;
            FindObjectOfType<BallSpawner>().ChangeBallSkinTo(material);
        }
    }

    void ResetButtons(SkinType type)
    {
        foreach (SkinButton button in buttons)
        {
            if (button.type != type) continue;
            button.overlay.sprite = idle;
        }
    }

    private void ChangeCharacterSkin(Material material, SkinnedMeshRenderer target)
    {
        target.material = material;
    }
}

public enum SkinType
{
    Ball,
    Skin
}