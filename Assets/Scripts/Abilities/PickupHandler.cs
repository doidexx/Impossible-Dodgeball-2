using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    public GameManager gameManager = null;
    [Header("Pickups")]
    public Pickup[] pickups = null;
    [Header("Pickups Settings")]
    public int pointsToSpawn = 100;
    public int pointIncreaseMinimum = 100;
    public int pointIncreaseMaximum = 400;
    [Header("Test")]
    public bool testing = false;

    private void Update()
    {
        if (gameManager.score >= pointsToSpawn)
        {
            pointsToSpawn += (int)Random.Range(pointIncreaseMinimum, pointIncreaseMaximum);
            var pickupNumber = (int)Random.Range(0, pickups.Length);
            if (!testing)
                pickups[pickupNumber].gameObject.SetActive(true);
            else
            {
                pickups[0].gameObject.SetActive(true);
                pickups[1].gameObject.SetActive(true);
            }
        }
    }
}
