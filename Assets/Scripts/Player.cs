using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float jumpHeight = 17f;
    public float gravityModifier = 3f;
    public float movementSpeed = 10f;
    public float minimumHeight = 6;
    public int direction = 0;
    public bool isGrounded = false;
    public bool playerMovementLocked = true;

    [Header("Other")]
    public PlayerState playerState;
    public Joystick joystick = null;
    public GameObject model = null;
    public CamerasTarget cameraTarget = null;

    [Header("Abilities")]
    public int numberOfJumps = 1;

    [Header("Audio Clips")]
    public AudioClip[] sneakerSounds = null;

    int timesJumped = 0;
    float verticalDirection = 0;
    float horizontalDirection = 0;

    Rigidbody rb = null;
    Rigidbody[] ragdoll = new Rigidbody[0];
    Animator animator = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        ConfigureRagdoll();
    }

    private void Update()
    {
        ProcessInput();
        if (transform.position.y < -minimumHeight)
            Hit();
    }

    private void ConfigureRagdoll()
    {
        ragdoll = model.GetComponentsInChildren<Rigidbody>();
        var characterJoints = GetComponentsInChildren<CharacterJoint>();
        for (int i = 0; i < ragdoll.Length; i++)
        {
            ragdoll[i].isKinematic = true;
            ragdoll[i].interpolation = RigidbodyInterpolation.Interpolate;
            ragdoll[i].collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }
        foreach (CharacterJoint joint in characterJoints)
        {
            joint.enableProjection = true;
        }
        cameraTarget.target = ragdoll[0].transform;
    }

    #region Input Processing
    private void ProcessInput()
    {
        if (playerMovementLocked == false && isGrounded == true && playerState != PlayerState.Down)
        {
            timesJumped = 0;
            horizontalDirection = joystick.Horizontal * movementSpeed;

            var dir = direction;
            direction = Mathf.Clamp((int)horizontalDirection, -1, 1);
            if (dir != direction)
                PlaySneakerSound();

            if (Input.GetButtonDown("Jump")) // This is only for keyboard functionality
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    UITwistJump();
                else
                    UIJump();
            }
        }
        else
        {
            verticalDirection += Physics.gravity.y * gravityModifier * Time.deltaTime;
        }
        Vector3 movementDirection = new Vector3(horizontalDirection, verticalDirection, 0);
        rb.velocity = movementDirection;
    }

    private void Jump()
    {
        if (playerMovementLocked == true || CanJump() ==  false)
            return;
        timesJumped++;
        verticalDirection = jumpHeight;
    }

    public void UIJump()
    {
        Jump();
    }

    public void UITwistJump()
    {
        if (direction == 0 || playerMovementLocked == true)
            return;
        Jump();
        animator.SetTrigger("Twist");
    }

    private bool CanJump()
    {
        var notDead = playerState != PlayerState.Down;
        var canStillJump = timesJumped < numberOfJumps;
        return isGrounded == true && notDead && canStillJump;
    }
    #endregion

    public void Hit()
    {
        if (playerState == PlayerState.Down)
            return;
        animator.enabled = false;
        playerState = PlayerState.Down;
        for (int i = 0; i < ragdoll.Length; i++)
        {
            ragdoll[i].isKinematic = false;
        }
    }

    public void PlaySneakerSound()
    {
        if (playerMovementLocked == true || playerState == PlayerState.Down)
            return;
        var clip = sneakerSounds[Random.Range(0, sneakerSounds.Length)];
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Floor")
            isGrounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
            PlaySneakerSound();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
            isGrounded = false;
    }
}
