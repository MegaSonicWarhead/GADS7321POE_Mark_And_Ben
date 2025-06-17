using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUI : MonoBehaviour
{
    [System.Serializable]
    public class PuzzleSlot
    {
        public string ItemID;
        public Image image;
    }

    public PuzzleSlot[] PuzzlePieces;

    void OnEnable()
    {
        UpdatePuzzleUI();

    }

    public void UpdatePuzzleUI()
    {
        foreach (var piece in PuzzlePieces)
        {
            bool hasPiece = InventoryManager.Instance.HasItem(piece.ItemID);
            Color color = piece.image.color;
            color.a = hasPiece ? 1f : 0f; 
            piece.image.color = color;
        }
    }
}
