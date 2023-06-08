using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToGrid : MonoBehaviour
{
    //snaps the structure to the grid using the mouse position


    ObjectPlacer objectPlacer;

    [Header("Ofssets")]
    public float outsideX;
    public float outsideY;
    public float insideX;
    public float insideY;

    // Start is called before the first frame update
    void Start()
    {
        objectPlacer = GameObject.Find("Controller").GetComponent<ObjectPlacer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(objectPlacer.instantiatedStructure == gameObject)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(objectPlacer.rotation % 2 == 0)
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
    }
}
