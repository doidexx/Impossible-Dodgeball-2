using UnityEngine;
using UnityEngine.Advertisements;

public class MatchCounter : MonoBehaviour
{
    public int matchesForAds = 5;

    DataHolder holder = null;
    int matches = 0;

    private void Awake()
    {
        holder = FindObjectOfType<DataHolder>();
        matches = holder.matchesSinceLastAd + 1;
    }

    private void Start()
    {
        // if (matches >= matchesForAds)
        // {
            Advertisement.Initialize ("1234567", true);
            Advertisement.Show();
        // }
    }
}
