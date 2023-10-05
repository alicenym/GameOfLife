using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellScript : MonoBehaviour
{
    public bool alive;
    SpriteRenderer spriteRenderer;
    public bool livesToNextGeneration;
    // List with spriteRenderers so the cell can change over time

    public void UpdateStatus()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = alive;
    }
}
