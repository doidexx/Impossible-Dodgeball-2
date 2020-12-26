using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public float runningAnimationSpeed = 0.6f;
    public Player player = null;

    float timeInIdle = 0;
    Animator animator = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player.playerMovementLocked == true)
            return;
        var movement = player.joystick.Horizontal * runningAnimationSpeed;
        animator.SetFloat("Movement", movement);
        animator.SetBool("Moving", movement != 0);
        Idle(movement);

        animator.SetInteger("Direction", (int)player.direction);
        animator.SetBool("Is Grounded", player.isGrounded);
    }

    private void Idle(float movement)
    {
        if (movement == 0 && player.isGrounded == true)
            timeInIdle += Time.deltaTime;
        else
            timeInIdle = 0;
        animator.SetFloat("Time In Idle", timeInIdle);
    }

    public void Landed()//Animation Event
    {
        animator.SetBool("Twist", false);
        player.PlaySneakerSound();
    }

    public void DoneStretching()
    {
        player.playerMovementLocked = false;
        animator.SetBool("Starting", false);
    }
}
