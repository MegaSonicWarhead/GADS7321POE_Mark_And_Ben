using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MemoryState { Remembered, Fading, Forgotten }
public class MemoryAnchor : MonoBehaviour
{
    public MemoryState state = MemoryState.Remembered;
    public float fadeDelay = 30f;
    private float decayTimer = 0f;

    [Tooltip("Fog tiles linked to this memory anchor")]
    public FogTile[] linkedFogTiles;

    private bool isFading = false;
    public float fadeHoldTime = 10f;

    void Update()
    {
        if (state == MemoryState.Remembered && !isFading)
        {
            decayTimer += Time.deltaTime;

            if (decayTimer >= fadeDelay)
            {
                StartCoroutine(FadeOutMemory());
            }
        }
    }

    public void AnchorMemory()
    {
        state = MemoryState.Remembered;
        decayTimer = 0f;
        isFading = false;

        foreach (FogTile fog in linkedFogTiles)
        {
            fog.Restore();
        }

        gameObject.SetActive(true);
    }

    private IEnumerator FadeOutMemory()
    {
        isFading = true;
        state = MemoryState.Fading;
        Debug.Log("Fog is fading");

        // First, fade in all fog tiles (restore)
        foreach (FogTile fog in linkedFogTiles)
        {
            fog.Restore();
        }

        // Wait until all fog tiles finish restoring (fade in)
        bool stillRestoring = true;
        while (stillRestoring)
        {
            stillRestoring = false;
            foreach (FogTile fog in linkedFogTiles)
            {
                if (fog.IsRestoring())
                {
                    stillRestoring = true;
                    break;
                }
            }
            yield return null; // wait one frame
        }

        // Hold fog fully visible for fadeHoldTime seconds
        yield return new WaitForSeconds(fadeHoldTime);

        // Then start fading away
        foreach (FogTile fog in linkedFogTiles)
        {
            fog.FadeAway();
        }

        // Wait until all fog tiles finish fading (fade out)
        bool stillFading = true;
        while (stillFading)
        {
            stillFading = false;
            foreach (FogTile fog in linkedFogTiles)
            {
                if (fog.IsFading())
                {
                    stillFading = true;
                    break;
                }
            }
            yield return null; // wait one frame
        }

        // Now memory is truly forgotten
        state = MemoryState.Forgotten;
        gameObject.SetActive(false);
        isFading = false;
    }
}
