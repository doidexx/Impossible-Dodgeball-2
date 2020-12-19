using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public float SFX_Volume = 1;

    private void Start()
    {
        var audioControllers = FindObjectsOfType<AudioController>();
        if (audioControllers.Length > 1)
            Destroy(audioControllers[1].gameObject);
        DontDestroyOnLoad(this);

        GetLastVolumeSettings();
    }

    private void GetLastVolumeSettings()
    {
        DataHolder data = FindObjectOfType<DataHolder>();
        SFX_Volume = data.SFXVolume;
        GetComponent<AudioSource>().volume = data.musicVolume;
    }
}
