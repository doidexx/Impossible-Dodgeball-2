using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckNewUnlock : MonoBehaviour
{
    public SkinButton[] skinButtons = null;
    public List<Sprite> newlyUnlocks = null;
    public Transform content = null;

    private void Start()
    {
        CheckSkins();
        foreach (var image in newlyUnlocks)
        {
            Instantiate(image, content);
        }
    }

    private void CheckSkins()
    {
        foreach (var button in skinButtons)
        {
            button.CheckUnlockable(gameObject);
        }
    }
}
