using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToGrid : MonoBehaviour
{
    //snaps the structure to the grid using the mouse position


    ObjectPlacer objectPlacer;

    [Header("Ofssets")]
    public int x;
    public int y;

    float outsideX;
    float outsideY;
    float insideX;
    float insideY;

    // Start is called before the first frame update
    void Start()
    {
        insideX = (x % 2) / 2f;
        insideY = (y % 2) / 2f;
        outsideX = -insideX;
        outsideY = -insideY;
        objectPlacer = GameObject.Find("Controller").GetComponent<ObjectPlacer>();
    }

    // Update is called once per frame
    void Update()
    {
        snap();
    }

    public void snap()
    {
        if (objectPlacer.instantiatedStructure == gameObject)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (GetComponent<RotateSprite>().rotation % 2 == 0)
            {
                mousePosition.x = Mathf.Round(mousePosition.x + insideX) + outsideX;
                mousePosition.y = Mathf.Round(mousePosition.y + insideY) + outsideY;
            }
            else
            {
                mousePosition.x = Mathf.Round(mousePosition.x + insideY) + outsideY;
                mousePosition.y = Mathf.Round(mousePosition.y + insideX) + outsideX;

            }

            transform.position = mousePosition;
        }
        else
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);

            if (GetComponent<RotateSprite>().rotation % 2 == 0)
            {
                pos.x = Mathf.Round(pos.x + insideX) + outsideX;
                pos.y = Mathf.Round(pos.y + insideY) + outsideY;
            }
            else
            {
                pos.x = Mathf.Round(pos.x + insideY) + outsideY;
                pos.y = Mathf.Round(pos.y + insideX) + outsideX;

            }

            transform.position = pos;
        }
    }
}
