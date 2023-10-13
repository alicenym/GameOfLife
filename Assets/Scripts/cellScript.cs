using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellScript : MonoBehaviour
{
    public bool alive;
    SpriteRenderer spriteRenderer;
    public bool livesToNextGeneration;
    public bool faded;
    public Sprite fadedCell;
    public Sprite livelyCell;

    public void UpdateStatus()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = alive;

        if (livesToNextGeneration == true)
        {
            spriteRenderer.enabled = alive;
            spriteRenderer.sprite = livelyCell;

        }

        if (faded == true)
        {
            Invoke("FadedSprite", 1f);
        }
    }

    public void FadedSprite()
    {
        spriteRenderer.sprite = fadedCell;
    }

}
