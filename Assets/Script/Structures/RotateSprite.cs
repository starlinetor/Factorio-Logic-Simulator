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
    GameObject ancorPoints;

    float x;
    float y;


    // Start is called before the first frame update
    void Awake()
    {

        x = gameObject.GetComponent<BoxCollider2D>().size.x;
        y = gameObject.GetComponent<BoxCollider2D>().size.y;
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectPlacer = GameObject.Find("Controller").GetComponent<ObjectPlacer>();
        ancorPoints = transform.GetChild(0).gameObject;

        //we need to set this by default because if we don't when we place nmany objects fast the risk is that they will get no rotation
        rotation = objectPlacer.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (objectPlacer.instantiatedStructure == gameObject)
        {
            rotation = objectPlacer.rotation;
        }

        spriteRenderer.sprite = sprites[rotation];

        if (rotation % 2 == 0)
        {
            ancorPoints.transform.position = new Vector2(transform.position.x, transform.position.y);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(x, y);
        }
        else
        {
            ancorPoints.transform.position = new Vector2(transform.position.x, transform.position.y+0.2f);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(y, x);
        }

        ancorPoints.transform.eulerAngles = new Vector3(0, 0, -rotation*90);

    }
}