using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cables : MonoBehaviour
{

    //this code really sucks

    public int cableUsed;
    //0 none
    //1 red
    //2 green

    public Material red, green;

    public ObjectPlacer objectPlacer;
    public QDetection qDetection;

    public GameObject structure1;
    public GameObject structure2;

    public GameObject cable;
    public LineRenderer lineRenderer;
    public bool cableInstantiated;

    public Connections struct1Con;
    public Connections struct2Con;

    public GameObject parent;

    public Vector2 clicked;

    public int inputOutput1;
    public int inputOutput2;
    //1 is input 
    //2 os output

    public GameObject ancor1;
    public GameObject ancor2;


    // Start is called before the first frame update
    void Start()
    {
        cableUsed = 0;
        cableInstantiated = false; 

        objectPlacer = GetComponent<ObjectPlacer>();
        qDetection= GetComponent<QDetection>();
        parent = GameObject.Find("Cables");
    }

    // Update is called once per frame
    void Update()
    {
        if(cableUsed != 0)
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(cableInstantiated)
                {
                    Destroy(cable);
                    structure1 = null;
                    structure2 = null;
                    cableInstantiated = false;
                }
                else
                {
                    cableUsed = 0;
                }

            }

            objectPlacer.structure = null;

            if (Input.GetMouseButtonDown(0) && qDetection.hoveringGameObject!=null && structure1 == null)
            {
                structure1 = qDetection.hoveringGameObject;
                
                clicked  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clicked -= new Vector2(structure1.transform.position.x, structure1.transform.position.y);


                inputOutput1 = inputOuput(clicked, structure1);

                if (cableUsed == 1)
                {
                    ancor1 = structure1.transform.Find("AncorPoints/" + inputOutput1.ToString() + "Red").gameObject;
                }
                else
                {
                    ancor1 = structure1.transform.Find("AncorPoints/" + inputOutput1.ToString() + "Green").gameObject;
                }

            }

            if (structure1 != null)
            {
                if (!cableInstantiated)
                {
                    cable = new GameObject("cable");

                    cable.transform.parent = parent.transform;

                    lineRenderer = cable.AddComponent<LineRenderer>();
                    if (cableUsed == 1)
                    {
                        lineRenderer.material = red;
                    }
                    else if (cableUsed == 2)
                    {
                        lineRenderer.material = green;
                    }
                    lineRenderer.startWidth = 0.05f;
                    lineRenderer.endWidth = 0.05f;
                    cableInstantiated = true;
                }

                if(Input.GetMouseButtonDown(0) && qDetection.hoveringGameObject != null)
                {

                    structure2 = qDetection.hoveringGameObject;

                    clicked = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    clicked -= new Vector2(structure2.transform.position.x, structure2.transform.position.y);


                    inputOutput2 = inputOuput(clicked, structure2);

                    if (cableUsed == 1)
                    {
                        ancor2 = structure2.transform.Find("AncorPoints/" + inputOutput2.ToString() + "Red").gameObject;
                    }
                    else
                    {
                        ancor2 = structure2.transform.Find("AncorPoints/" + inputOutput2.ToString() + "Green").gameObject;
                    }



                    if(ancor1 != ancor2){

                        if (cableUsed == 1)
                        {
                            lineRenderer.SetPosition(0, new Vector3(ancor1.transform.position.x, ancor1.transform.position.y, -1));
                            lineRenderer.SetPosition(1, new Vector3(ancor2.transform.position.x, ancor2.transform.position.y, -1));
                        }
                        else
                        {
                            lineRenderer.SetPosition(0, new Vector3(ancor1.transform.position.x, ancor1.transform.position.y, -1));
                            lineRenderer.SetPosition(1, new Vector3(ancor2.transform.position.x, ancor2.transform.position.y, -1));
                        }


                        struct1Con = structure1.GetComponent<Connections>();
                        struct2Con = structure2.GetComponent<Connections>();

                        //red
                        if (cableUsed == 1)
                        {
                            //if the same type of connection exist destroy (the structure needs to already reference each other and the connecion needs to be the same)
                            if (struct1Con.connectionsRed1.Contains(structure2) && struct1Con.connectionPointRed1[struct1Con.connectionsRed1.IndexOf(structure2)]==inputOutput2)
                            {
                                //destroy the old cable
                                Destroy(struct1Con.cablesRed1[struct1Con.connectionsRed1.IndexOf(structure2)].gameObject);
                                //remove refrence to the point of coonncetion
                                //struct1Con.connectionPointRed1.RemoveAt(struct1Con.connectionsRed1.IndexOf(structure2));
                                //remove reference to the structure the cable was connected
                                struct1Con.connectionsRed1.Remove(structure2);

                                //remove reference to the type of connection
                                //struct2Con.connectionPointRed2.RemoveAt(struct2Con.connectionsRed2.IndexOf(structure1));
                                //remove reference to the structure the cable was connected
                                struct2Con.connectionsRed1.Remove(structure1);

                                //destroy the ghost cable
                                Destroy(cable);

                                structure1 = null;
                                structure2 = null;
                                ancor1 = null;
                                ancor2 = null;
                                cableInstantiated = false;
                                cable = null;
                            }
                            else
                            {
                                struct1Con.connectionsRed1.Add(structure2);
                                struct2Con.connectionsRed1.Add(structure1);
                                struct1Con.cablesRed1.Add(cable);
                                struct2Con.cablesRed1.Add(cable);

                                structure1 = structure2;
                                structure2 = null;
                                ancor1 = ancor2;
                                ancor2 = null;
                                cableInstantiated = false;
                                cable = null;
                            }
                        }
                        else
                        {
                            if (struct1Con.connectionsGreen1.Contains(structure2))
                            {
                                Destroy(struct1Con.cablesGreen1[struct1Con.connectionsGreen1.IndexOf(structure2)].gameObject);
                                struct1Con.cablesGreen1.RemoveAt(struct1Con.connectionsGreen1.IndexOf(structure2));
                                struct1Con.connectionsGreen1.Remove(structure2);
                                struct2Con.cablesGreen1.RemoveAt(struct2Con.connectionsGreen1.IndexOf(structure1));
                                struct2Con.connectionsGreen1.Remove(structure1);
                                Destroy(cable);

                                structure1 = null;
                                structure2 = null;
                                ancor1 = null;
                                ancor2 = null;
                                cableInstantiated = false;
                                cable = null;
                            }
                            else
                            {
                                struct1Con.connectionsGreen1.Add(structure2);
                                struct2Con.connectionsGreen1.Add(structure1);
                                struct1Con.cablesGreen1.Add(cable);
                                struct2Con.cablesGreen1.Add(cable);

                                structure1 = structure2;
                                structure2 = null;
                                ancor1 = ancor2;
                                ancor2 = null;
                                cableInstantiated = false;
                                cable = null;
                            }
                        }
                    }
                    


                }
                else
                {
                    lineRenderer.SetPosition(0, new Vector3(ancor1.transform.position.x, ancor1.transform.position.y, -1));
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    lineRenderer.SetPosition(1, new Vector3(mousePosition.x, mousePosition.y, -1));
                }
            }
        }
    }

    public void Green()
    {
        cableUsed = 2;
    }

    public void Red()
    {
        cableUsed = 1;
    }
    
    int inputOuput(Vector2 mouse, GameObject structure)
    {

        RotateSprite rotateSprite = structure.GetComponent<RotateSprite>();
        
        switch(rotateSprite.rotation)
        {
            case 0:
                if (mouse.y > 0)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            case 1:
                if (mouse.x > 0)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            case 2:
                if (mouse.y < 0)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            case 3:
                if (mouse.x < 0)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
        }

        Debug.Log("Error in fucntion inputOuput in script Cables, the return value is 0");
        return 0;
    }
}
