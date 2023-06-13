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
    public GameObject structures;
    SaveFileGenerator saveFileGen;

    bool structureInstantiated;
    bool structurePlaced;


    // Start is called before the first frame update
    void Start()
    {
        rotation = 0;
        structures = GameObject.Find("Structures");
        saveFileGen = structures.GetComponent<SaveFileGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (structure == null)
        {
            //If the structure is not place destroy the prefab
            if (!structurePlaced)
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

            //fi the structure is changed change it
            if (structure.tag != instantiatedStructure.tag)
            {
                //If the structure is not place destroy the prefab
                if (!structurePlaced)
                {
                    Destroy(instantiatedStructure);
                }

                //resett all the variables
                structureInstantiated = false;
                instantiatedStructure = null;
            }
        }

        //this needs to be changed, because when you press q you should use the object you press and if there is nothing structure should be set to null.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //if you press right click remove the structure
            //structure = null;
        }

        //before placing the structure is important to make it snap so we are sure that is actually placed
        if (instantiatedStructure != null)
        {
            instantiatedStructure. GetComponent<snapToGrid>().snap();
        }
        //if you press right click, a structure is selected, you are not hovering over another structure or UI you can place the structure
        if (Input.GetMouseButton(0) && structure && !hoveringUi() && !saveFileGen.checkOverlaps(instantiatedStructure) && !saveFileGen.saving)
        {
            structurePlaced = true;
            structureInstantiated = false;
            instantiatedStructure.transform.parent = structures.transform;
            instantiatedStructure = null;
            saveFileGen.saveFile();
        }
    }


    //returns true if you are hovring over an UI element
    //The UI element needs to have the UIHhovering componenet
    GameObject[] UIelements;
    bool hoveringUi()
    {
        UIelements = GameObject.FindGameObjectsWithTag("UI");

        for (int i = 0; i < UIelements.Length; i++)
        {
            if (UIelements[i].GetComponent<UIHovering>().hovering == true)
            {
                return true;
            }
        }

        return false;
    }
}


