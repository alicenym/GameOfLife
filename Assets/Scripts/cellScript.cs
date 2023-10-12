using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellScript : MonoBehaviour
{
    public bool alive;
    SpriteRenderer spriteRenderer;
    public bool livesToNextGeneration;
    //public bool livedPreviousGeneration;
    //public  Sprite dyingCell;
  //public Sprite livingCell;

    public void UpdateStatus()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = alive;

       //f(livesToNextGeneration == true && 
    }
}
