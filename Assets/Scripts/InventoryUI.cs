using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventorySlotPrefab;  // Prefab with Image component
    public Transform slotParent;

    private Dictionary<string, GameObject> slots = new Dictionary<string, GameObject>();

    public void UpdateDisplay()
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        slots.Clear();

        foreach (string itemID in InventoryManager.Instance.GetItems())
        {
            GameObject slot = Instantiate(inventorySlotPrefab, slotParent);

            // Replace this section with icon logic
            Image icon = slot.GetComponentInChildren<Image>();
            if (icon != null)
            {
                Sprite sprite = InventoryManager.Instance.GetSpriteForItem(itemID);
                icon.sprite = sprite;
            }

            slots[itemID] = slot;
        }
    }
}
