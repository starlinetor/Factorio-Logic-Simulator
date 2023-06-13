using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnRightClick : MonoBehaviour
{
    ObjectPlacer objectPlacer;
    SaveFileGenerator saveFileGen;
    QDetection qDetection;

    private void Start()
    {
        saveFileGen = GameObject.Find("Structures").GetComponent<SaveFileGenerator>();
        objectPlacer = GameObject.Find("Controller").GetComponent<ObjectPlacer>();
        qDetection = GameObject.Find("Controller").GetComponent<QDetection>();
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
            if (Input.GetMouseButton(1) && !hoveringUi())
            {
                //we need to remove the gameobject from the parent list so that is not counted in the save. 
                transform.parent = null;

                //Because on mouse exit does not call when you delete the game object we need to remove the hovering variable or you will not be able to deselect the building you have selected
                qDetection.hovering = null;

                saveFileGen.saveFile();
                Destroy(gameObject);
            }
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
