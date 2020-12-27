using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [Header("Settings")]
    public float baseSpeed = 1;
    public float baseTurningSpeed = 1;
    public float speed = 1;
    public float turningSpeed = 1;
    public float chasingDistance = 10;
    public float lifeSpan = 5f;
    public float targetOffset = 1.5f;
    [Header("Progression settings")]
    [Range(0.5f,1.5f)]
    public float speedRoundMultiplier = 1.2f;
    [Range(0.5f,1.5f)]
    public float turningSpeedMultiplier = 1.2f;
    public float maxSpeed = 60;
    public float maxTurningSpeed = 8;

    Transform target = null;
    Rigidbody rb = null;
    GameManager gameManager = null;
    bool hasCollide = false;
    float lifeTime = Mathf.Infinity;

    private void Awake()
    {
        target = GameObject.FindWithTag("Ragdoll").transform;
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        gameObject.SetActive(false);
    }

    public void Update()
    {
        if (CanChaseTarget())
            rb.velocity = GetSmoothChaseDirection() * speed;
        LifeTime();
    }

    private void LifeTime()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > lifeSpan)
            gameObject.SetActive(false);
    }

    public void IncreaseSpeed(int round)
    {
        float minSpeed = Mathf.Min(maxSpeed, baseSpeed * round * speedRoundMultiplier);
        speed = Mathf.Max(baseSpeed, minSpeed);
        turningSpeed = Mathf.Min(maxTurningSpeed, baseTurningSpeed * round * turningSpeedMultiplier);
    }

    private Vector3 GetSmoothChaseDirection()
    {
        return Vector3.Lerp(rb.velocity.normalized, GetDirection(), turningSpeed * Time.deltaTime);
    }

    private bool CanChaseTarget()
    {
        var inDistance = Vector3.Distance(target.position, transform.position) < chasingDistance;
        var behindPlayer = transform.position.z > target.position.z;
        return inDistance && behindPlayer && hasCollide == false;
    }

    private Vector3 GetDirection()
    {
        Vector3 targetOffsetPosition = target.position + new Vector3(0, Random.Range(targetOffset, targetOffset * 2), 0);
        return (targetOffsetPosition - transform.position).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        hasCollide = true;
        rb.useGravity = true;
        var clip = gameManager.bounceSounds[Random.Range(0, gameManager.bounceSounds.Length)];
        GetComponent<AudioSource>().PlayOneShot(clip);
        if (collision.transform.tag == "Ragdoll")
            FindObjectOfType<Player>().Hit();
    }

    public void Launch()
    {
        rb.velocity = GetDirection() * speed;
    }

    private void OnDisable()
    {
        GetComponent<TrailRenderer>().Clear();
        hasCollide = false;
        rb.useGravity = false;
        gameManager.IncreaseScore();
        lifeTime = 0;
    }
}

