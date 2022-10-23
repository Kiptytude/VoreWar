using System;
using UnityEngine;


public class FadeInFadeOut : MonoBehaviour
{
    public float FadeInTime = .2f;
    public float HoldTime = .2f;
    public float FadeOutTime = .8f;

    public SpriteRenderer SpriteRenderer;

    bool fadingIn = true;
    bool fadingOut = false;

    float currentTime = 0;

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (fadingIn)
        {
            SpriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, currentTime / FadeInTime));
            if (currentTime > FadeInTime)
            {
                fadingIn = false;
                currentTime = 0;
            }
        }
        else if (fadingOut)
        {
            SpriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, currentTime / FadeOutTime));
            if (currentTime > FadeOutTime)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (currentTime > HoldTime)
            {
                fadingOut = true;
                currentTime = 0;
            }
        }
    }
        
}

