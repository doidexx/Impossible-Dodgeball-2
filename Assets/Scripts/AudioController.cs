using UnityEngine;

public class AudioController : MonoBehaviour
{
    public float SFX_Volume = 1;
    public float timePlayingTrack = 0;
    public float timeBetweenTracks = 2;
    public int trackIndex = 0;
    public AudioSource audioSource = null;
    public AudioClip[] musicTracks = null;
    public string trackName = "";

    private void Start()
    {
        var audioControllers = FindObjectsOfType<AudioController>();
        if (audioControllers.Length > 1)
        {
            Destroy(audioControllers[1].gameObject);
            return;
        }
        DontDestroyOnLoad(this);

        GetLastVolumeSettings();
        trackIndex = Random.Range(0, musicTracks.Length);
        trackName = musicTracks[trackIndex].name;
        audioSource.clip = musicTracks[trackIndex];
        audioSource.Play();
    }

    private void Update()
    {
        timePlayingTrack += Time.deltaTime;
        if (timePlayingTrack >= audioSource.clip.length + timeBetweenTracks)
            GetTrack(1);
    }

    private void GetLastVolumeSettings()
    {
        DataHolder data = FindObjectOfType<DataHolder>();
        SFX_Volume = data.SFXVolume;
        audioSource.volume = data.musicVolume;
    }

    public void GetTrack(int dir)
    {
        timePlayingTrack = 0;
        if (dir == 1)
            trackIndex = GetNextTrackIndex();
        else
            trackIndex = GetPreviousTrackIndex();
        trackName = musicTracks[trackIndex].name;
        audioSource.clip = musicTracks[trackIndex];
        audioSource.Play();
    }

    private int GetNextTrackIndex()
    {
        if (trackIndex + 1 >= musicTracks.Length)
            return 0;
        else
            return trackIndex + 1;
    }
    private int GetPreviousTrackIndex()
    {
        if (trackIndex - 1 < 0)
            return musicTracks.Length - 1;
        else
            return trackIndex - 1;
    }
}
