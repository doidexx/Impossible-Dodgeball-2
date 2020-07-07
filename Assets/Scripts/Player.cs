using ID.Core;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 17f;
    [SerializeField] private float gravity = 50f;
    [SerializeField] private float movementSpeed = 10f;
    private Vector3 _startingPosition;
    private Vector3 _direction = Vector3.zero;
    public static float inputDirection = 0;

    CharacterController _characterController;
    Rigidbody[] _ragdoll;

    public static bool canMove = false;
    private GameManager _gameManager;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
        _characterController = GetComponent<CharacterController>();
        Physics.IgnoreLayerCollision(8, 9); //ignore player and ragdoll layer collision
        Physics.IgnoreLayerCollision(8, 10); //ignore player and ball layer collision

        _startingPosition = transform.position;
        ConfigureRagdoll();
    }

    private void Update()
    {
        ProcessInput();
        if (transform.position.y < -6)
            Hit();
    }

    private void ConfigureRagdoll()
    {
        _ragdoll = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in _ragdoll)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }

    private void ProcessInput()
    {
        if (_gameManager.isPlayerHit) return;
        if (!canMove) return;

        inputDirection = Input.GetAxis("Horizontal");
        _animator.SetFloat("Blend", -inputDirection * 0.66f);
        if (_characterController.isGrounded)
        {
            _direction.x = inputDirection * movementSpeed;
            if (Input.GetButtonDown("Jump"))
                _direction.y = jumpHeight;
        }
        else
        {
            _direction.y -= gravity * Time.deltaTime;
        }
        _characterController.Move(_direction * Time.deltaTime);
    }

    public void ResetPlayer()
    {
        _characterController.enabled = false;
        transform.position = _startingPosition;
        _animator.enabled = true;
        _characterController.enabled = true;
    }

    public void Hit()
    {
        _animator.enabled = false;
        _gameManager.isPlayerHit = true;
    }
}
