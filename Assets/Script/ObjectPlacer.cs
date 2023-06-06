using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{

    //Handles placing objects on the grid
    //Allows to also rotate the object
    //Once a prefab is selected it can be spawned untill rightclick is pressed

    public int rotation;
    public GameObject structure;
    public GameObject instantiatedStructure;
    bool structureInstantiated;
    bool structurePlaced;


    // Start is called before the first frame update
    void Start()
    {
        rotation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (structure == null)
        {
            //If the structure is not place destroy the prefab
            if(!structurePlaced)
            {
                Destroy(instantiatedStructure);
            }

            //resett all the variables
            structureInstantiated = false;
            instantiatedStructure = null;

        }
        else
        {

            //if structure exist roate it
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (rotation != 3)
                {
                    rotation++;
                }
                else
                {
                    rotation = 0;
                }
            }

            //instantiate the structure once
            if (!structureInstantiated)
            {
                structureInstantiated = true;
                structurePlaced = false;
                instantiatedStructure = Instantiate(structure, Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
            }
        }
        

        if (Input.GetMouseButtonDown(1))
        {
            //if you press right click remove the structure
            structure = null;
        }

        //if you are not hovering overr UI you can place the selected strucutre
        if (Input.GetMouseButtonDown(0) && structure && !hoveringUi())
        {
            structurePlaced = true;
            structureInstantiated = false;
            instantiatedStructure = null;
        }
    }


    //returns true if you are hovring over an UI element
    //The UI element needs to have the UIHhovering componenet
    GameObject[] UIelements;
    bool hoveringUi()
    {
        UIelements = GameObject.FindGameObjectsWithTag("UI");

        for(int i=0; i<UIelements.Length; i++)
        {
            if (UIelements[i].GetComponent<UIHovering>().hovering == true)
            {
                return true;
            }
        }

        return false;
    }
}



