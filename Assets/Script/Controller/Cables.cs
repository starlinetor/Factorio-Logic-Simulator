using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cables : MonoBehaviour
{
    public int cableUsed;
    //0 none
    //1 red
    //2 green

    public Material red, green;

    public ObjectPlacer objectPlacer;
    public QDetection qDetection;

    public GameObject structure1;

    public GameObject cable;
    public LineRenderer lineRenderer;
    public bool cableInstantiated;
    public bool cablePlaced;

    // Start is called before the first frame update
    void Start()
    {
        cableUsed = 0;
        cableInstantiated = false;
        cablePlaced = false; 

        objectPlacer = GetComponent<ObjectPlacer>();
        qDetection= GetComponent<QDetection>();

    }

    // Update is called once per frame
    void Update()
    {
        if(cableUsed != 0)
        {
            objectPlacer.structure = null;

            if (Input.GetMouseButtonDown(0))
            {
                structure1 = qDetection.hoveringGameObject;
            }

            if (structure1 != null)
            {
                if (!cableInstantiated)
                {
                    cable = new GameObject("cable");
                    lineRenderer = cable.AddComponent<LineRenderer>();
                    if(cableUsed == 2)
                    {
                        lineRenderer.material = green;
                    }else if(cableUsed == 1)
                    {
                        lineRenderer.material = red;
                    }
                    lineRenderer.SetWidth(0.1f,0.1f);
                    cableInstantiated = true;
                }

                lineRenderer.SetPosition(0, new Vector3(structure1.transform.position.x, structure1.transform.position.y, -1));

                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                lineRenderer.SetPosition(1, new Vector3(mousePosition.x, mousePosition.y, -1));

                

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
