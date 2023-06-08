using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CheckCollision : MonoBehaviour
{
    //This is terrible, needs to be deleted and changed with someting else, because collision are not fast enoght allowing you to place structure on top of one another. 
    //checks if the structure is colliding with another structure, if it is it will change its color to red 
    public bool colliding;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        colliding = true;
        spriteRenderer.color = Color.red;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        colliding = false;
        spriteRenderer.color = Color.white;
    }
}
