using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connections : MonoBehaviour
{

    //this code sucks
    //I know, still to lazy to fix it

    //input red 
    public List<GameObject> connectionsRed1 = new List<GameObject>();
    public List<GameObject> cablesRed1 = new List<GameObject>();
    public List<int> connectionPointRed1 = new List<int>();

    //input green
    public List<GameObject> connectionsGreen1 = new List<GameObject>();
    public List<GameObject> cablesGreen1 = new List<GameObject>();
    public List<int> connectionPointGreen1 = new List<int>();


    //output red
    public List<GameObject> connectionsRed2 = new List<GameObject>();
    public List<GameObject> cablesRed2 = new List<GameObject>();
    public List<int> connectionPointRed2 = new List<int>();

    //output gren
    public List<GameObject> connectionsGreen2 = new List<GameObject>();
    public List<GameObject> cablesGreen2 = new List<GameObject>();
    public List<int> connectionPointGreen2 = new List<int>();

    //base


    private void Update()
    {

        //if a reference is missing it gets deleted
        checkAbsence(connectionsRed1, connectionPointRed1);
        checkAbsence(connectionsGreen1, connectionPointGreen1);
        checkAbsence(connectionsGreen2, connectionPointGreen2);
        checkAbsence(connectionsRed2, connectionPointRed2);
        checkAbsence(cablesRed1);
        checkAbsence(cablesGreen1);
        checkAbsence(cablesGreen2);
        checkAbsence(cablesRed2);
    }

    void checkAbsence(List<GameObject> structures)
    {
        for (int i = 0; i < structures.Count; i++)
        {
            if (structures[i] == null)
            {
                structures.Remove(structures[i]);
            }
        }
    }


    void checkAbsence(List<GameObject> structures, List<int> connections)
    {
        for(int i=0; i<structures.Count; i++)
        {
            if (structures[i] == null)
            {
                structures.Remove(structures[i]);
                connections.Remove(connections[i]);
            }
        }
    }

}
