using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public Puzzle[] allPuzzles;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        foreach (var puzzle in allPuzzles)
        {
            if (puzzle.variants.Length > 0)
            {
                int index = Random.Range(0, puzzle.variants.Length);
                puzzle.activeVariant = puzzle.variants[index];
                Debug.Log($"Puzzle {puzzle.puzzleID} → Variant {puzzle.activeVariant.variantID}");
            }
        }
    }

    public bool IsItemInActiveVariant(string puzzleID, string itemID)
    {
        Puzzle puzzle = System.Array.Find(allPuzzles, p => p.puzzleID == puzzleID);
        if (puzzle == null || puzzle.activeVariant == null) return false;

        foreach (var slot in puzzle.activeVariant.slots)
        {
            if (slot.itemID == itemID) return true;
        }

        return false;
    }

    public PuzzleVariant GetActiveVariant(string puzzleID)
    {
        return System.Array.Find(allPuzzles, p => p.puzzleID == puzzleID)?.activeVariant;
    }
}

[System.Serializable]
public class PuzzleChoice
{
    public string itemID;
    public UnityEngine.UI.Image imageSlot;
}

[System.Serializable]
public class PuzzleVariant
{
    public string variantID;
    public PuzzleChoice[] slots;
}

[System.Serializable]
public class Puzzle
{
    public string puzzleID;
    public PuzzleVariant[] variants;

    [HideInInspector] public PuzzleVariant activeVariant;
}
