using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public string itemID;
    public KeyCode interactKey = KeyCode.E;
    public bool isPickup = true; // True when collectible, false when placing

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            if (isPickup)
            {
                InventoryManager.Instance.AddItem(itemID);
                gameObject.SetActive(false); // Hide from scene
            }
            else
            {
                if (InventoryManager.Instance.HasItem(itemID))
                {
                    InventoryManager.Instance.RemoveItem(itemID);
                    gameObject.SetActive(true); // Place back in world

                    // Set alpha to 255 (1f in float range)
                    SpriteRenderer sr = GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        Color c = sr.color;
                        c.a = 1f; // 255 in float is 1.0
                        sr.color = c;
                    }
                }
            }
        }
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
}
