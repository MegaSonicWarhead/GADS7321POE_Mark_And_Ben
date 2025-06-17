using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public string itemID;
    public KeyCode interactKey = KeyCode.E;
    public bool isPickup = true;

    private bool playerInRange = false;

    public string puzzleID;

    [Header("Audio")]
    public AudioClip pickupSound;
    public AudioClip placeSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            

            if (isPickup)
            {
                InventoryManager.Instance.AddItem(itemID);
                StartCoroutine(PlayPickupAndDeactivate());
            }
            else
            {
                if (InventoryManager.Instance.HasItem(itemID))
                {
                    InventoryManager.Instance.RemoveItem(itemID);
                    gameObject.SetActive(true);
                    PlaySound(placeSound);

                    // Reset sprite alpha to fully visible
                    SpriteRenderer sr = GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        Color c = sr.color;
                        c.a = 1f;
                        sr.color = c;
                    }
                }
            }
        }
    }

    IEnumerator PlayPickupAndDeactivate()
    {
        PlaySound(pickupSound);
        if (pickupSound != null)
        {
            yield return new WaitForSeconds(pickupSound.length);
        }
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
