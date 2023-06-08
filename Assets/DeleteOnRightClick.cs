using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnRightClick : MonoBehaviour
{
    ObjectPlacer objectPlacer;

    private void Start()
    {
        objectPlacer = GameObject.Find("Controller").GetComponent<ObjectPlacer>();
    }
    //Destroy the game onbject if you right click

    private void Update()
    {
        if (objectPlacer.instantiatedStructure == gameObject)
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    void OnMouseOver()
    {
        if (objectPlacer.instantiatedStructure != gameObject)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(gameObject);
            }
        }
    }
}
