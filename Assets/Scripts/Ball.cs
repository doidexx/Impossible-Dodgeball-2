using ID.Core;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float launchForceMultiplier = 1f;
    [SerializeField] private float chaseForceMultiplier = 5f;
    [SerializeField] private float distanceToChasePlayer = 15f;
    [Header("Adjustments")]
    [SerializeField] private float horDirectionOffset = 4f;
    [SerializeField] private float verDirectionOffset = 4f;

    private const float LiveTime = 4f;
    private float _timeSinceLastSpawned = 0;

    private Rigidbody _rigidbody;
    private Transform _target;

    private bool _hasCollided = false;

    private void OnEnable()
    {
        _target = FindObjectOfType<Player>().transform;
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
        _hasCollided = false;
        _rigidbody.useGravity = false;
        _rigidbody.AddForce(GetLaunchDirection() * launchForceMultiplier, ForceMode.Impulse);
        _timeSinceLastSpawned = 0;
    }

    private void Update()
    {
        ProcessLifeTime();
        ProcessChasingState();
    }

    private Vector3 GetLaunchDirection()
    {
        Vector3 targetedPosition;
        var position = _target.position;
        targetedPosition.x = position.x + (UnityEngine.Random.Range(0, horDirectionOffset) * Player.inputDirection);
        targetedPosition.y = position.y + UnityEngine.Random.Range(-1, verDirectionOffset);
        targetedPosition.z = position.z;
        return (targetedPosition - transform.position).normalized;
    }

    private void ProcessLifeTime()
    {
        _timeSinceLastSpawned += Time.deltaTime;
        if (_timeSinceLastSpawned > LiveTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void ProcessChasingState()
    {
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, launchForceMultiplier);
        if (_hasCollided) return;
        if (IsBehindTarget()) return;
        if (GetDistanceFromPlayer() < distanceToChasePlayer)
        {
            _rigidbody.AddForce(GetTargetDirection() * chaseForceMultiplier);
        }
    }

    private Vector3 GetTargetDirection()
    {
        return (_target.position - transform.position).normalized;
    }

    private bool IsBehindTarget()
    {
        return transform.position.z < _target.position.z;
    }

    private float GetDistanceFromPlayer()
    {
        return Vector3.Distance(_target.position, transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.transform.GetComponentInParent<Player>();
        if (player)
            player.Hit();
        _rigidbody.useGravity = true;
        _hasCollided = true;
    }

    private void OnDisable()
    {
        _rigidbody.velocity *= 0;
        FindObjectOfType<GameManager>().IncreaseScore();
    }
}
