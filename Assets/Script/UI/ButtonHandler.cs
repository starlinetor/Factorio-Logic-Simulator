using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    //Script to handle buttons for placing structures

    public GameObject structure;
    ObjectPlacer objectPlacer;
    CablesV2 cablesV2;

    // Start is called before the first frame update
    void Start()
    {
        objectPlacer = GameObject.Find("Controller").GetComponent<ObjectPlacer>();
        cablesV2 = GameObject.Find("Controller").GetComponent <CablesV2>();
    }

    // Update is called once per frame
    public void SetStructure()
    {
        cablesV2.cableUsed = null;
        objectPlacer.structure = structure;
    }
}
