using System.Collections.Generic;
using ID.Core;
using UnityEngine;

namespace ID.UI
{
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

        private List<SkinButton> _buttons = null;
        private SkinButton _selectedButton = null;
        private SkinButton _previewing = null;
        private SkinButton _selectedBallButton = null;
        private SkinButton _previewingBallButton = null;

        public void Subscribe(SkinButton button)
        {
            if (_buttons == null)
                _buttons = new List<SkinButton>();
            _buttons.Add(button);
        }

        public void OnButtonEnter(SkinButton button)
        {
            if (_selectedButton == button || highlight == null || _selectedBallButton == button) return;
            button.overlay.sprite = highlight;
        }

        public void OnButtonExit(SkinButton button)
        {
            if (_selectedButton == button || idle == null || _selectedBallButton == button) return;
            button.overlay.sprite = idle;
        }

        public void OnButtonClick(SkinButton button, SkinType type, Material material)
        {
            if (type == SkinType.Skin)
            {
                if (_previewing == button)
                    OnButtonSelect(button, SkinType.Skin, material);
                else
                {
                    ChangeCharacterSkin(material, characterPreview);
                    _previewing = button;
                }
            }

            if (type == SkinType.Ball)
            {
                if (_previewingBallButton == button)
                    OnButtonSelect(button, SkinType.Ball, material);
                else
                {
                    ballPreview.material = material;
                    _previewingBallButton = button;
                }
            }
        }

        public void OnButtonSelect(SkinButton button, SkinType type, Material material)
        {
            ResetButtons(type);
            if (type == SkinType.Skin)
            {
                button.overlay.sprite = selected;
                _selectedButton = button;
                ChangeCharacterSkin(material, character);
            }
            else if (type == SkinType.Ball)
            {
                button.overlay.sprite = selected;
                _selectedBallButton = button;
                FindObjectOfType<BallSpawner>().ChangeBallSkinTo(material);
            }
        }

        void ResetButtons(SkinType type)
        {
            foreach (SkinButton button in _buttons)
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
}