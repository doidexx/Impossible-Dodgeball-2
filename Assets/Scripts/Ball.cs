using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float launchForceMultiplier = 1f;
    [SerializeField] float chaseForceMultiplier = 5f;
    [SerializeField] float distanceToChasePlayer = 15f;
    [Header("Adjustments")]
    [SerializeField] float horDirectionOffset = 4f;
    [SerializeField] float verDirectionOffset = 4f;

    float liveTime = 4f;
    float timeSinceLastSpawned = 0;

    Rigidbody rigidBody;
    Transform target;

    bool hasCollided = false;

    private void OnEnable()
    {
        target = FindObjectOfType<Player>().transform;
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();
        hasCollided = false;
        rigidBody.useGravity = false;
        rigidBody.AddForce(GetLaunchDirection() * launchForceMultiplier, ForceMode.Impulse);
        timeSinceLastSpawned = 0;
    }

    private void Update()
    {
        ProcessLifeTime();
        ProcessChasingState();
    }

    private Vector3 GetLaunchDirection()
    {
        Vector3 targetedPosition;
        targetedPosition.x = target.position.x + (UnityEngine.Random.Range(0, horDirectionOffset) * Player.inputDirection);
        targetedPosition.y = target.position.y + UnityEngine.Random.Range(-1, verDirectionOffset);
        targetedPosition.z = target.position.z;
        return (targetedPosition - transform.position).normalized;
    }

    private void ProcessLifeTime()
    {
        timeSinceLastSpawned += Time.deltaTime;
        if (timeSinceLastSpawned > liveTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void ProcessChasingState()
    {
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, launchForceMultiplier);
        if (hasCollided) return;
        if (IsBehindTarget()) return;
        if (GetDistanceFromPlayer() < distanceToChasePlayer)
        {
            rigidBody.AddForce(GetTargetDirection() * chaseForceMultiplier);
        }
    }

    private Vector3 GetTargetDirection()
    {
        return (target.position - transform.position).normalized;
    }

    private bool IsBehindTarget()
    {
        return transform.position.z < target.position.z;
    }

    private float GetDistanceFromPlayer()
    {
        return Vector3.Distance(target.position, transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.transform.GetComponentInParent<Player>();
        if (player)
            player.Hit();
        rigidBody.useGravity = true;
        hasCollided = true;
    }

    private void OnDisable()
    {
        rigidBody.velocity *= 0;
        FindObjectOfType<GameManager>().IncreaseScore();
    }
}
