using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemID;
    public Sprite itemSprite;
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("All Collectible Items")]
    public InventoryItem[] allItems;

    private Dictionary<string, Sprite> itemSprites = new Dictionary<string, Sprite>();
    private HashSet<string> inventory = new HashSet<string>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        foreach (var item in allItems)
        {
            itemSprites[item.itemID] = item.itemSprite;
        }
    }

    public bool HasItem(string itemID) => inventory.Contains(itemID);

    public void AddItem(string itemID)
    {
        inventory.Add(itemID);
        Debug.Log("Item added: " + itemID);
        FindObjectOfType<InventoryUI>()?.UpdateDisplay();
    }

    public void RemoveItem(string itemID)
    {
        if (inventory.Remove(itemID))
        {
            Debug.Log("Item removed: " + itemID);
            FindObjectOfType<InventoryUI>()?.UpdateDisplay();
        }
    }

    public IEnumerable<string> GetItems() => inventory;

    public Sprite GetSpriteForItem(string itemID)
    {
        itemSprites.TryGetValue(itemID, out Sprite sprite);
        return sprite;
    }
}
