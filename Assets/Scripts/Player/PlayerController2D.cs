using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;

    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Set animation parameters (optional)
        if (animator != null)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        // Interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void Interact()
    {
        //Debug.Log("Interacted with object!");
        // You can implement raycast or trigger logic here for interaction
    }
}
