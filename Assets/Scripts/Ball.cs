using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 1;
    public float turningSpeed = 1;
    public float chasingDistance = 10;
    public float lifeSpan = 5f;
    public float targetOffset = 1.5f;
    [Header("Progression settings")]
    [Range(1,1.5f)]
    public float speedRoundMultiplier = 1.2f;
    [Range(1,1.5f)]
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

    public void IncreaseSpeed()
    {
        speed = Mathf.Min(maxSpeed, speed * gameManager.round * speedRoundMultiplier);
        turningSpeed = Mathf.Min(maxTurningSpeed, turningSpeed * gameManager.round * turningSpeedMultiplier);    
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
        Vector3 targetOffsetPosition = target.position + new Vector3(0, targetOffset, 0);
        return (targetOffsetPosition - transform.position).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.tag);
        hasCollide = true;
        rb.useGravity = true;
        if (collision.transform.tag == "Ragdoll")
        {
            FindObjectOfType<Player>().Hit();
        }
    }

    public void Launch()
    {
        rb.velocity = GetDirection() * speed;
    }

    private void OnDisable()
    {
        hasCollide = false;
        rb.useGravity = false;
        gameManager.IncreaseScore();
        lifeTime = 0;
    }
}

