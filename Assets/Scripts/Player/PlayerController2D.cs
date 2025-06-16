using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    public AudioSource footstepAudio; // Assign in Inspector
    public AudioClip walkingClip;     // Assign in Inspector

    public TextMeshProUGUI confusionText; // Assign in Inspector (UI Text for the message)

    private Vector2 movement;
    private bool isConfused = false;

    // Confusion timing
    public float minConfusionDelay = 20f;
    public float maxConfusionDelay = 60f;

    void Start()
    {
        if (footstepAudio != null)
        {
            footstepAudio.clip = walkingClip;
            footstepAudio.loop = true;
        }

        if (confusionText != null)
            confusionText.enabled = false;

        StartCoroutine(TriggerRandomConfusion());
    }

    void Update()
    {
        if (!isConfused)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

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
        else
        {
            movement = Vector2.zero;
            if (footstepAudio.isPlaying) footstepAudio.Stop();
        }
    }

    void FixedUpdate()
    {
        if (!isConfused)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
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
            if (isMoving && !footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
            else if (!isMoving && footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }
    }

    IEnumerator TriggerRandomConfusion()
    {
        while (true)
        {
            float waitTime = Random.Range(minConfusionDelay, maxConfusionDelay);
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(DoConfusionEvent());
        }
    }

    IEnumerator DoConfusionEvent()
    {
        isConfused = true;

        if (confusionText != null)
        {
            confusionText.text = Random.value < 0.5f ? "Where am I?" : "Where was I going?";
            confusionText.enabled = true;
        }

        yield return new WaitForSeconds(4f); // Duration of confusion

        if (confusionText != null)
            confusionText.enabled = false;

        isConfused = false;
    }
}
