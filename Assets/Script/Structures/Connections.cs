using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Connections : MonoBehaviour
{

    //this code sucks

    public List<GameObject> connectionsRed1 = new List<GameObject>();
    public List<GameObject> cablesRed1 = new List<GameObject>();
    public List<GameObject> connectionsGreen1 = new List<GameObject>();
    public List<GameObject> cablesGreen1 = new List<GameObject>();

    //
    public List<GameObject> connectionsRed2 = new List<GameObject>();
    public List<GameObject> cablesRed2 = new List<GameObject>();
    public List<GameObject> connectionsGreen2 = new List<GameObject>();
    public List<GameObject> cablesGreen2 = new List<GameObject>();

    //base


    private void Update()
    {

        //if a reference is missing it gets deleted
        checkAbsence(connectionsRed1);
        checkAbsence(connectionsGreen1);
        checkAbsence(connectionsGreen2);
        checkAbsence(connectionsRed2);
        checkAbsence(cablesRed1);
        checkAbsence(cablesGreen1);
        checkAbsence(cablesGreen2);
        checkAbsence(cablesRed2);
    }

    void checkAbsence(List<GameObject> structures)
    {
         for(int i=0; i<structures.Count; i++)
         {
            if (structures[i] == null)
            {
                structures.Remove(structures[i]);
            }
         }
    }

}
