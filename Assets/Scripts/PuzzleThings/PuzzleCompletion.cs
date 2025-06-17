using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.WSA;

public class PuzzleCompletion : MonoBehaviour
{

    public GameObject endGame;

    public string[] requiredItemIDs;

    [Header("UI")]
    public TextMeshProUGUI messageText;
    public float messageDuration = 10f;

    private bool messageShown = false;
    // Start is called before the first frame update
    void Start()
    {
        if (endGame != null)
            endGame.SetActive(false);

        if (messageText !=null)
            messageText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!messageShown && AllPuzzlesComplete())
        {
            /*if (endGame !=null && !endGame.activeSelf)
            {
                endGame.SetActive(true);
                Debug.Log("all pieces collected");
            }*/
            ActivateCompletion();
        }
    }

    void ActivateCompletion()
    {
        if (endGame != null)
            endGame.SetActive(true);
            Debug.Log("all pieces collected");
        if(messageText !=null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "All the pieces have been collected, go back to the start to finish the game";
            StartCoroutine(HideMessageAfterDelay());
        }
        messageShown = true;
    }


    bool AllPuzzlesComplete()
    {
        foreach (var id in requiredItemIDs)
        {
            if (!InventoryManager.Instance.HasItem(id))
                return false;
        }
        return true;
    }

    IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        if (messageText !=null)
            messageText.gameObject.SetActive(false );
    }
}
