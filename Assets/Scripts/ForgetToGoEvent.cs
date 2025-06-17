using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ForgetToGoEvent : MonoBehaviour
{
    public float timeNearToilet = 5f;
    private float timer = 0f;
    private bool inToiletZone = false;
    private bool hasTriggered = false;

    [Header("UI & Audio")]
    public TextMeshProUGUI messageText;
    public AudioClip sadSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // Force 2D
        audioSource.volume = 1f;

        Debug.Log("ForgetToGoEvent initialized.");
    }

    void Update()
    {
        if (hasTriggered) return;

        if (inToiletZone)
        {
            timer += Time.deltaTime;
            Debug.Log($"In toilet zone. Timer: {timer:F2}");

            if (timer >= timeNearToilet)
            {
                Debug.Log("Time limit reached. Triggering forget event.");
                TriggerForgetEvent();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inToiletZone = true;
            timer = 0f;
            Debug.Log("Entered ToiletZone.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inToiletZone = false;
            timer = 0f;
            Debug.Log("Exited ToiletZone. Timer reset.");
        }
    }

    void TriggerForgetEvent()
    {
        hasTriggered = true;
        Debug.Log("Forget event triggered.");

        if (messageText != null)
        {
            messageText.text = "You forgot why you were here...\nThen, an accident happened.";
            Debug.Log("Displayed message to player.");
        }

        if (sadSound != null)
        {
            audioSource.PlayOneShot(sadSound);
            Debug.Log("Played sad sound.");
        }
        else
        {
            Debug.LogWarning("Missing sad sound clip.");
        }
    }

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 180, 30), "Play Sad Sound (Test)"))
    //    {
    //        if (sadSound != null)
    //        {
    //            audioSource.PlayOneShot(sadSound);
    //            Debug.Log("Manual test: Played sad sound.");
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Manual test: Missing sad sound.");
    //        }
    //    }
    //}

}
