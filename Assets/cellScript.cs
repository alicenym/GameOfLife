using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellScript : MonoBehaviour
{
    public bool alive;
    SpriteRenderer spriteRenderer;
    

    public void UpdateStatus()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = alive;
        int neighbors = 0;
    }
}
