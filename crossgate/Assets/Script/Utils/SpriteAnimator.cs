using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator
{
    SpriteRenderer spriteRenderer;
    float framRate;
    float timer;
    bool loop;

    int currentFrame;
    List<Sprite> frames = new List<Sprite>();
    public SpriteAnimator(List<Sprite> frames, SpriteRenderer spriteRenderer, float frameRate = 0.16f, bool loop = true)
    {
        this.frames = frames;
        this.spriteRenderer = spriteRenderer;
        this.framRate = frameRate;
        this.loop = loop;
    }

    public int CurrentFrame
    {
        get { return currentFrame; }
    }

    public void Start()
    {
        currentFrame = 0;
        timer = 0f;
        spriteRenderer.sprite = frames[0];
    }

    public void HandleUpdate()
    {
        timer += Time.deltaTime;
        if (timer > framRate)
        {
            if (loop)
            {
                currentFrame = (currentFrame + 1) % frames.Count;
                spriteRenderer.sprite = frames[currentFrame];
                timer -= framRate;
            }
            else
            {
                if (currentFrame < frames.Count - 1)
                {
                    currentFrame += 1;
                    spriteRenderer.sprite = frames[currentFrame];
                    timer -= framRate;
                }
            }

        }
    }

    public List<Sprite> Frames
    {
        get { return frames; }
    }

}
