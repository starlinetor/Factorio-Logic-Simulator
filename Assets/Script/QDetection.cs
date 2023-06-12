using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QDetection : MonoBehaviour
{

    //if the bool is true this script is attached on the controller, else is attached to a structure
    bool controller;
    QDetection qDetection;
    public GameObject hovering;
    ObjectPlacer objectPlacer;
    SaveFileGenerator saveFileGenerator;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name == "Controller")
        {
            controller = true;
        }

        objectPlacer = GameObject.Find("Controller").GetComponent<ObjectPlacer>();


        if (!controller)
        {
            saveFileGenerator = GameObject.Find("Structures").GetComponent<SaveFileGenerator>();
            qDetection = GameObject.Find("Controller").GetComponent<QDetection>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(controller && Input.GetKeyDown(KeyCode.Q))
        {

            objectPlacer.structure = hovering;
            Debug.Log("changed structure");
        }
    }


    void OnMouseOver()
    {
        if(!controller && objectPlacer.instantiatedStructure != gameObject) {

            for(int i = 0; i < saveFileGenerator.prefabs.Length; i++)
            {
                if (CompareTag(saveFileGenerator.prefabs[i].tag))
                {
                    qDetection.hovering = saveFileGenerator.prefabs[i];
                    break;
                }
            }

        }
    }

    void OnMouseExit()
    {
        if (!controller && objectPlacer.instantiatedStructure != gameObject)
        {
            qDetection.hovering = null;

        }
    }

}
