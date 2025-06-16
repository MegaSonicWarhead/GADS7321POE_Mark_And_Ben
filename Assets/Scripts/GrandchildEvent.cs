using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrandchildEvent : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public AudioClip emotionalSound;
    private AudioSource audioSource;

    private bool triggered = false;

    private string[] dialogueLines = new string[]
    {
        "Grandpa: Jamie! How was your math test?",
        "Jamie: ...Grandpa?",
        "Jamie: That was 10 years ago... I work at the hospital now.",
        "Grandpa: The hospital? But... you’re only 16...",
        "Jamie: No, Grandpa... I’m 26.",
        "Grandpa: ...Oh. Right... I must’ve... forgotten again."
    };

    private int currentLine = 0;
    private float lineDelay = 3f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(PlayDialogue());
        }
    }

    IEnumerator PlayDialogue()
    {


        foreach (string line in dialogueLines)
        {
            messageText.text = line;
            yield return new WaitForSeconds(lineDelay);
        }
        if (emotionalSound != null)
            audioSource.PlayOneShot(emotionalSound);
        messageText.text = ""; // Clear text
    }

}
