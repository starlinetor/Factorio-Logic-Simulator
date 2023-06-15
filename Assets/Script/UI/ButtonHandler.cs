using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    //Script to handle buttons for placing structures

    public GameObject structure;
    ObjectPlacer objectPlacer;

    // Start is called before the first frame update
    void Start()
    {
        objectPlacer = GameObject.Find("Controller").GetComponent<ObjectPlacer>();
    }

    // Update is called once per frame
    public void SetStructure()
    {
        objectPlacer.structure = structure;
    }
}
