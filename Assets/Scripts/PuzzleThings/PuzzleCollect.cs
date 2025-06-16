using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PuzzleCollect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string puzzleID;

    void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        PuzzleVariant variant = PuzzleManager.Instance.GetActiveVariant(puzzleID);
        if (variant == null) return;

        foreach (var slot in variant.slots)
        {
            bool PiecePickedUp = InventoryManager.Instance.HasItem(slot.itemID);

            var color = slot.imageSlot.color;
            color.a = PiecePickedUp ? 1f : 0f;
            slot.imageSlot.color = color;

            Sprite puzzleSprite = InventoryManager.Instance.GetSpriteForItem(slot.itemID);
            if (puzzleSprite != null)
            {
                slot.imageSlot.sprite = puzzleSprite;
            }
        }
    }
}
