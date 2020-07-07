using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpHeight = 17f;
    [SerializeField] float gravity = 50f;
    [SerializeField] float movementSpeed = 10f;
    Vector3 startingPosition;
    Vector3 direction = Vector3.zero;
    public static float inputDirection = 0;

    CharacterController characterController;
    Rigidbody[] ragdoll;

    public static bool canMove = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Physics.IgnoreLayerCollision(8, 9); //ignore player and ragdoll layer collision
        Physics.IgnoreLayerCollision(8, 10); //ignore player and ball layer collision

        startingPosition = transform.position;
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
        ragdoll = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in ragdoll)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }

    private void ProcessInput()
    {
        if (FindObjectOfType<GameManager>().isPlayerHit) return;
        if (!canMove) return;

        inputDirection = Input.GetAxis("Horizontal");
        GetComponent<Animator>().SetFloat("Blend", -inputDirection * 0.66f);
        if (characterController.isGrounded)
        {
            direction.x = inputDirection * movementSpeed;
            if (Input.GetButtonDown("Jump"))
                direction.y = jumpHeight;
        }
        else
        {
            direction.y -= gravity * Time.deltaTime;
        }
        characterController.Move(direction * Time.deltaTime);
    }

    public void ResetPlayer()
    {
        characterController.enabled = false;
        transform.position = startingPosition;
        GetComponent<Animator>().enabled = true;
        characterController.enabled = true;
    }

    public void Hit()
    {
        GetComponent<Animator>().enabled = false;
        FindObjectOfType<GameManager>().isPlayerHit = true;
    }
}
