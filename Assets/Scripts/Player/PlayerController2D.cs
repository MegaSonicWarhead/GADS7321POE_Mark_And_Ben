using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    public AudioSource footstepAudio; // Assign in Inspector
    public AudioClip walkingClip;     // Assign in Inspector

    private Vector2 movement;

    void Start()
    {
        if (footstepAudio != null)
        {
            footstepAudio.clip = walkingClip;
            footstepAudio.loop = true;
        }
    }

    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Set animation parameters
        if (animator != null)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        HandleFootstepSound();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void Interact()
    {
        // Implement interaction logic
    }

    void HandleFootstepSound()
    {
        bool isMoving = movement.sqrMagnitude > 0.1f;

        if (footstepAudio != null && walkingClip != null)
        {
            if (isMoving)
            {
                if (!footstepAudio.isPlaying)
                    footstepAudio.Play();
            }
            else
            {
                if (footstepAudio.isPlaying)
                    footstepAudio.Stop();
            }
        }
    }
}
