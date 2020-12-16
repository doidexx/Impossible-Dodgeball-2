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

    [Header("Other")]
    public PlayerState playerState;
    public Joystick joystick = null;
    public GameObject model = null;

    [Header("Abilities")]
    public int numberOfJumps = 1;

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
        foreach(CharacterJoint joint in characterJoints)
        {
            joint.enableProjection = true;
        }
        Camera.main.GetComponent<CameraController>().ragdoll = ragdoll[1].transform;
    }

    private void ProcessInput()
    {
        if (isGrounded == true && playerState != PlayerState.Down)
        {
            timesJumped = 0;
            horizontalDirection = joystick.Horizontal * movementSpeed;
            direction = Mathf.Clamp((int)horizontalDirection, -1, 1);

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
        timesJumped++;
        verticalDirection = jumpHeight;
    }

    public void UIJump()
    {
        if (CanJump() == false)
            return;
        Jump();
    }

    public void UITwistJump()
    {
        if (CanJump() == false || direction == 0)
            return;
        animator.SetBool("Twist", true);
        Jump();
    }

    private bool CanJump()
    {
        return isGrounded == true && playerState != PlayerState.Down && timesJumped < numberOfJumps && animator.GetBool("Twist") == false;
    }

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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Floor")
            isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
            isGrounded = false;
    }
}
