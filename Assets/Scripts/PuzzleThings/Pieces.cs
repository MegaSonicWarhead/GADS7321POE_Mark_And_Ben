using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    public string puzzleID;
    public string ItemID;

    void Start()
    {
        Updateisibility();
    }


    public void UpdateVisibility()
    {
        bool IsActive = PuzzleManager.Instance != null &&
                PuzzleManager.Instance.IsItemInActiveVariant(PuzzleID, itemID);
        gameObject.SetActive(IsActive);
    }

}
