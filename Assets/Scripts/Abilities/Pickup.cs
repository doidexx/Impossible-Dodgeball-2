using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Type")]
    public bool isDoublePoints = false;
    public bool isShield = false;
    public bool isHighJump = false;
    public bool isSpeed = false;
    [Header("Game Objects")]
    public GameObject shield = null;
    public Player player = null;
    [Header("Time")]
    public float lifeTime = 12f;
    [Header("Spawn Parameters")]
    public float minX = 0;
    public float maxX = 0;
    public float minY = 0;
    public float maxY = 0;

    float timeSinceActive = 0;

    private void OnEnable()
    {
        timeSinceActive = 0;
        GetComponent<Animator>().Rebind();
        var randomX = Random.Range(minX, maxX);
        var randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);
        transform.position = spawnPosition;
    }

    private void Update()
    {
        timeSinceActive += Time.deltaTime;
        if (timeSinceActive >= lifeTime)
            gameObject.SetActive(false);
        else if (timeSinceActive >= lifeTime - 5)
            GetComponent<Animator>().SetBool("Ending", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isShield)
            shield.gameObject.SetActive(true);
        if (isHighJump)
            player.ActivateHighJump();
        if (isSpeed)
            player.ActivateHighSpeed();
        gameObject.SetActive(false);
    }
}
