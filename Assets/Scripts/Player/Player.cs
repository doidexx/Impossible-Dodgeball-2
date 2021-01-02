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
    public bool highJump = false;
    public bool highSpeed = false;
    [Header("Abilities Objects")]
    public Light[] lights = null;
    public Color highJumpColor = Color.blue;
    public Color highSpeedColor = Color.yellow;

    [Header("Ability Settings")]
    public float abilitiesTime = 15f;
    [Range(1, 2f)]
    public float highJumpMultiplier = 2;
    [Range(1, 2f)]
    public float highSpeedMultiplier = 2;

    [Header("Audio Clips")]
    public AudioClip[] sneakerSounds = null;

    int timesJumped = 0;
    float verticalDirection = 0;
    float horizontalDirection = 0;
    float highJumpTimer = Mathf.Infinity;
    float highSpeedTimer = Mathf.Infinity;

    Rigidbody rb = null;
    Rigidbody[] ragdoll = new Rigidbody[0];
    Animator animator = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        ConfigureRagdoll();
        lights = GetComponentsInChildren<Light>();
    }

    private void Update()
    {
        ProcessInput();
        if (transform.position.y < -minimumHeight)
            Hit();
        CanHighJump();
        CanHighSpeed();
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
            if (highSpeed == false)
                horizontalDirection = joystick.Horizontal * movementSpeed;
            else
                horizontalDirection = joystick.Horizontal * movementSpeed * highSpeedMultiplier;


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
        if (playerMovementLocked == true || CanJump() == false)
            return;
        timesJumped++;
        if (highJump == true)
            verticalDirection = jumpHeight * highJumpMultiplier;
        else
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

    #region Abilities
    private void CanHighJump()
    {
        if (!highJump)
            return;
        highJumpTimer += Time.deltaTime;

        if (highJumpTimer >= abilitiesTime)
        {
            highJump = false;
            if (highSpeed)
                TurnLights(highSpeed, highSpeedColor, highSpeedColor);
            else
                TurnLights(highJump, Color.black, Color.black);
        }
        else if (highJumpTimer >= abilitiesTime - 4)
        {
            if (highSpeed)
                return;
            foreach (var light in lights)
            {
                light.GetComponent<Animator>().SetBool("Ending", true);
            }
        }
    }

    private void CanHighSpeed()
    {
        if (!highSpeed)
            return;
        highSpeedTimer += Time.deltaTime;

        if (highSpeedTimer >= abilitiesTime)
        {
            highSpeed = false;
            if (highJump)
                TurnLights(highJump, highJumpColor, highJumpColor);
            else
                TurnLights(highSpeed, Color.black, Color.black);
        }
        else if (highSpeedTimer >= abilitiesTime - 4)
        {
            if (highJump)
                return;
            foreach (var light in lights)
            {
                light.GetComponent<Animator>().SetBool("Ending", true);
            }
        }
    }

    public void ActivateHighJump()
    {
        highJump = true;
        highJumpTimer = 0;
        if (highSpeed == true)
            TurnLights(highJump, highJumpColor, highSpeedColor);
        else
            TurnLights(highJump, highJumpColor, highJumpColor);
    }

    public void ActivateHighSpeed()
    {
        highSpeed = true;
        highSpeedTimer = 0;
        if (highJump == true)
            TurnLights(highSpeed, highJumpColor, highSpeedColor);
        else
            TurnLights(highSpeed, highSpeedColor, highSpeedColor);
    }

    public void TurnLights(bool value, Color color, Color secondColor)
    {
        var _color = new Color();
        for (int i = 0; i < lights.Length; i++)
        {
            if (i == 0)
                _color = color;
            else
                _color = secondColor;

            var trail = lights[i].GetComponent<TrailRenderer>();
            lights[i].enabled = value;
            lights[i].color = _color;
            trail.startColor = _color;
            trail.endColor = _color;
            trail.emitting = value;
            if (value == false)
                lights[i].GetComponent<Animator>().Rebind();
        }
        Debug.Log(color == secondColor);
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
        TurnLights(false, Color.black, Color.black);
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
