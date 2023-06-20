using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CablesV2 : MonoBehaviour
{
    //dimension of the cable
    public float cableWidth;

    //stores the used cable
    public string cableUsed;

    //structures you connect the cable to
    public GameObject primary;
    public GameObject secondary;

    //ancor points for the cable
    public GameObject ancorPrimary;
    public GameObject ancorSecondary;

    //connection point for each structure
    public int connectionPointPrimary;
    public int connectionPointSecondary;

    //components 
    public ObjectPlacer objectPlacer;
    public QDetection qDetection;

    //material for the color of the line
    public Dictionary<string, Material> materials = new Dictionary<string, Material>();
    public Material red;
    public Material green;

    //gameobject all cables are set child of
    public GameObject parent;

    //bool that rappresents if the cable is instantiated
    public bool cableInstantiated;

    //the instantiated cable
    public GameObject cable;

    //the linerendere of the cable
    public LineRenderer lineRenderer;

    // Start is called before the first frame update

    SaveFileGenerator saveFileGen;
    void Start()
    {
        //decalre materials with string so that we can use cablesUsed to chose the right one
        materials["Red"] = red;
        materials["Green"] = green;

        objectPlacer = GetComponent<ObjectPlacer>();
        qDetection = GetComponent<QDetection>();
        parent = GameObject.Find("Cables");
        saveFileGen = GameObject.Find("Structures").GetComponent<SaveFileGenerator>();

        //set cable used to null because is not by defualt
        cableUsed = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (cableUsed != null)
        {
            //if you select the cable lets remove the selected structure
            objectPlacer.structure = null;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnQPressed();
            }

            //when you press left click we get the ancor points and connections
            if (Input.GetMouseButtonDown(0) && qDetection.hoveringGameObject != null)
            {
                if(primary == null)
                {
                    primary = qDetection.hoveringGameObject;
                    connectionPointPrimary = inputOuput(primary,cableUsed);
                    ancorPrimary = GetAncorPoint(primary, connectionPointPrimary);
                }
                else
                {
                    //we check that the structure and the connection are not the same, because you can connect a structure to itself but you can't connect it to the same place
                    if(primary!= qDetection.hoveringGameObject || ancorPrimary != GetAncorPoint(qDetection.hoveringGameObject, inputOuput(qDetection.hoveringGameObject, cableUsed))){
                        secondary = qDetection.hoveringGameObject;
                        connectionPointSecondary = inputOuput(secondary, cableUsed);
                        ancorSecondary = GetAncorPoint(secondary, connectionPointSecondary);

                        MoveCable(ancorPrimary.transform.position, ancorSecondary.transform.position);

                        if (ConnectionExist())
                        {
                            RemoveConnection();
                            Debug.Log("Remove connection");
                        }
                        else
                        {
                            CreateConnection();
                        }

                        //at the end we save the file, we don0t care if is already saving, considering that we don't risk collisions with cables and we don't use the save file for checks
                        saveFileGen.saveFile();

                    }

                }
            }

            if(cableInstantiated == false && primary != null)
            {
                InstantiateCable();
            }

            if(primary != null) {
                MoveCable(ancorPrimary.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }

    //destroys the old connection 
    void RemoveConnection()
    {
        //destroy displayed cable
        Destroy(cable);

        //destroy old cable, we don't need to remove references because they will be removed by themselves inside the gameobject
        ConnectionsV2.References connections = primary.GetComponent<ConnectionsV2>().references[cableUsed + connectionPointPrimary];
        Destroy(connections.cables[connections.structures.IndexOf(secondary)].gameObject);

        //reset parameters
        primary = null;
        secondary = null;
        ancorPrimary= null;
        ancorSecondary = null;
        cableInstantiated = false;
        cable = null;
    }

    //create the connection
    void CreateConnection()
    {
        ConnectionsV2.References connectionsPrimary = primary.GetComponent<ConnectionsV2>().references[cableUsed + connectionPointPrimary];
        ConnectionsV2.References connectionsSecondary = secondary.GetComponent<ConnectionsV2>().references[cableUsed + connectionPointSecondary];

        //add the structures
        connectionsPrimary.structures.Add(secondary);
        connectionsSecondary.structures.Add(primary);

        //Add the cable
        connectionsPrimary.cables.Add(cable);
        connectionsSecondary.cables.Add(cable);

        //Add the connection point of the other structure
        connectionsPrimary.connectedTo.Add(connectionPointSecondary);
        connectionsSecondary.connectedTo.Add(connectionPointPrimary);

        primary = secondary;
        secondary = null;

        ancorPrimary = ancorSecondary;
        ancorSecondary = null;

        connectionPointPrimary = connectionPointSecondary;
        connectionPointSecondary = 0;

        cableInstantiated = false;
        cable = null;
    }

    //function that checksi the connection already exist
    bool ConnectionExist()
    {
        //we iterate in the data that is storing the connection of the first structure, if this connection stores a reference equal to the connection we are trying to do we know that this connection already exist
        ConnectionsV2.References connections = primary.GetComponent<ConnectionsV2>().references[cableUsed+connectionPointPrimary];
        for (int i=0; i<connections.structures.Count; i++)
        {
            if (connections.structures[i]==secondary && connections.connectedTo[i] == connectionPointSecondary)
            {
                return true;
            }
        }

        return false;
    }

    //function that moves the cable to the two give positions
    void MoveCable(Vector2 position1, Vector2 position2)
    {
        lineRenderer.SetPosition(0, new Vector3(position1.x, position1.y, -1));
        lineRenderer.SetPosition(1, new Vector3(position2.x, position2.y, -1));
    }

    //insntantiate the cable and get all the references
    void InstantiateCable()
    {
        cable = new GameObject("cable");
        cable.transform.parent = parent.transform;
        lineRenderer = cable.AddComponent<LineRenderer>();
        lineRenderer.material = materials[cableUsed];
        lineRenderer.startWidth = cableWidth;
        lineRenderer.endWidth = cableWidth;
        cableInstantiated = true;
    }

    //removes or the placed cable or the tool cable
    public void OnQPressed()
    {
        if (cableInstantiated)
        {
            Destroy(cable);

            cable = null;
            
            primary = null;
            secondary = null;

            ancorPrimary = null;
            ancorSecondary = null;

            connectionPointPrimary = 0;
            connectionPointSecondary = 0;

            cableInstantiated = false;
        }
        else
        {
            cableUsed = null;
        }
    }

    //returns the type of connection that is done
    public int inputOuput(GameObject structure, string color)
    {
        float distance = 999999;
        GameObject ancor = null;

        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foreach (Transform t in structure.transform)
        {
            if(t.gameObject.tag == "Ancor")
            {
                if(distance>Vector3.Distance(t.position, new Vector3(mouse.x, mouse.y, 0f)) && t.gameObject.GetComponent<ancorPointData>().color == color)
                {
                    distance = Vector3.Distance(t.position, new Vector3(mouse.x, mouse.y, 0f));
                    ancor = t.gameObject;
                }
            }
        }
        if(ancor != null)
        {
            return ancor.GetComponent<ancorPointData>().inputOutput;
        }

        Debug.Log("No structure found return 0, CablesV2.inputOutPut");
        return 0;
    }

    //returns the ancor point given the connection point
    //rewrite this, is so bad considering it only works with structure that are with both inputs and outputs
    public GameObject GetAncorPoint(GameObject structure, int connectionPoint)
    {
        return structure.transform.Find(cableUsed + connectionPoint).gameObject;
    }

    //functions to change the used cable
    public void SetGreen()
    {
        cableUsed = "Green";
    }

    public void SetRed()
    { 

        cableUsed = "Red";
    }
}
