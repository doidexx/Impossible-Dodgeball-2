using UnityEngine;
using TMPro;

public class SongDisplay : MonoBehaviour
{
    public TextMeshProUGUI songName = null;

    AudioController audioController = null;

    private void Awake()
    {
        audioController = FindObjectOfType<AudioController>();
    }

    private void OnEnable()
    {
        UpdateSongName();
    }

    private void UpdateSongName()
    {
        songName.text = audioController.trackName;
    }

    public void NextSong()
    {
        audioController.GetTrack(1);
        UpdateSongName();
    }

    public void PreviousSong()
    {
        audioController.GetTrack(-1);
        UpdateSongName();
    }
}
