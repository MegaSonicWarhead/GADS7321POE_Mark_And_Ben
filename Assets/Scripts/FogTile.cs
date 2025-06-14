using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTile : MonoBehaviour
{
    public float fadeSpeed = 0.02f; // Alpha units per second
    private SpriteRenderer sr;
    private bool fading = false;
    private bool restoring = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
            Debug.LogError("FogTile missing SpriteRenderer.");

        // Start completely transparent white fog
        sr.color = new Color(1f, 1f, 1f, 0f);
        gameObject.SetActive(false); // Hide at start
    }

    void Update()
    {
        if (sr == null) return;

        Color c = sr.color;

        if (restoring)
        {
            // Activate once at start of restore
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);

            c.a += fadeSpeed * Time.deltaTime;
            c.a = Mathf.Clamp01(c.a);

            sr.color = c;

            if (c.a >= 1f)
            {
                restoring = false;
            }
        }
        else if (fading)
        {
            c.a -= fadeSpeed * Time.deltaTime;
            c.a = Mathf.Clamp01(c.a);

            sr.color = c;

            if (c.a <= 0f)
            {
                fading = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void FadeAway()
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        fading = true;
        restoring = false;

        // Ensure GameObject active so fade visible starts correctly
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }

    public bool IsFading()
    {
        return fading;
    }

    public bool IsRestoring()
    {
        return restoring;
    }

    public void Restore()
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        Color c = sr.color;
        c.a = 0f; // Start invisible and fade in
        sr.color = c;

        restoring = true;
        fading = false;

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }
}
