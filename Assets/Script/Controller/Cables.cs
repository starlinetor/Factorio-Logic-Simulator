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

                if(Input.GetMouseButtonDown(0) && qDetection.hoveringGameObject != null && qDetection.hoveringGameObject != structure1)
                {
                    structure2 = qDetection.hoveringGameObject;
                    if (cableUsed == 1)
                    {
                        lineRenderer.SetPosition(0, new Vector3(structure1.transform.position.x + 0.3f, structure1.transform.position.y + 0.3f, -1));
                        lineRenderer.SetPosition(1, new Vector3(structure2.transform.position.x + 0.3f, structure2.transform.position.y + 0.3f, -1));
                    }
                    else
                    {
                        lineRenderer.SetPosition(0, new Vector3(structure1.transform.position.x - 0.3f, structure1.transform.position.y - 0.3f, -1));
                        lineRenderer.SetPosition(1, new Vector3(structure2.transform.position.x - 0.3f, structure2.transform.position.y - 0.3f, -1));
                    }


                    struct1Con = structure1.GetComponent<Connections>();
                    struct2Con = structure2.GetComponent<Connections>();

                    //red
                    if(cableUsed == 1)
                    {
                        if (struct1Con.connectionsRed1.Contains(structure2))
                        {
                            Destroy(struct1Con.cablesRed1[struct1Con.connectionsRed1.IndexOf(structure2)].gameObject);
                            struct1Con.cablesRed1.RemoveAt(struct1Con.connectionsRed1.IndexOf(structure2));
                            struct1Con.connectionsRed1.Remove(structure2);
                            struct2Con.cablesRed1.RemoveAt(struct2Con.connectionsRed1.IndexOf(structure1));
                            struct2Con.connectionsRed1.Remove(structure1);
                            Destroy(cable);

                            structure1 = null;
                            structure2 = null;
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
                            cableInstantiated = false;
                            cable = null;
                        }
                    }
                }
                else
                {
                    if (cableUsed == 1)
                    {
                        lineRenderer.SetPosition(0, new Vector3(structure1.transform.position.x+0.3f, structure1.transform.position.y + 0.3f, -1));
                    }
                    else
                    {
                        lineRenderer.SetPosition(0, new Vector3(structure1.transform.position.x - 0.3f, structure1.transform.position.y - 0.3f, -1));
                    }
                    
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
}
