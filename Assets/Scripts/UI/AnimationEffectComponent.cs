using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AnimationEffectComponent : MonoBehaviour
{    
    public float[] FrameTime;
    public Sprite[] Frame;
    public bool Repeat;

    new private SpriteRenderer renderer;

    int currentFrame = 0;
    float currentTime = 0;

    private void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();

    }

    private void Update()
    {
        if (renderer == null)
            renderer = GetComponentInChildren<SpriteRenderer>();
        currentTime += Time.deltaTime;
        if (currentTime > FrameTime[currentFrame])
        {
            if (currentFrame + 1 > FrameTime.GetUpperBound(0))
            {
                if (Repeat)
                {
                    currentTime = 0;
                    currentFrame = 0;
                    renderer.sprite = Frame[0];
                    return;
                }
                Destroy(gameObject);
                return;
            }
            else
            {
                currentTime = 0;
                currentFrame++;
                renderer.sprite = Frame[currentFrame];
            }
        }
    }

}
