using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    //changes the structure sprite based on the rotation
    public Sprite[] sprites;
    public int rotation;
    SpriteRenderer spriteRenderer;
    ObjectPlacer objectPlacer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectPlacer = GameObject.Find("Controller").GetComponent<ObjectPlacer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(objectPlacer.instantiatedStructure == gameObject)
        {
            rotation = objectPlacer.rotation;
            spriteRenderer.sprite = sprites[objectPlacer.rotation];
            if (objectPlacer.rotation % 2 == 0)
            {
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 1.8f);
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 0.9f);
            }
        }
    }
}
