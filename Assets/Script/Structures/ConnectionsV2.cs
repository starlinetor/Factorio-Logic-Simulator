using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionsV2 : MonoBehaviour
{
    [Serializable]
    public class References
    {
        public List<GameObject> structures = new List<GameObject>();
        public List<GameObject> cables = new List<GameObject>();
        public List<int> connectedTo = new List<int>();
    }

    public Dictionary<string, References> references = new Dictionary<string, References>()
    {
        {"Red1",new References()},
        {"Red2",new References()},
        {"Green1",new References()},
        {"Green2",new References()},
    };

    //these variables are just to being able to see the connection in the inspector
    public References red1 = new References();
    public References red2 = new References();
    public References green1 = new References();
    public References green2 = new References();

    void Update()
    {
        //update the values
        red1 = references["Red1"];
        red2 = references["Red2"];
        green1 = references["Green1"];
        green2 = references["Green2"];


        checkAbsence("Red1");
        checkAbsence("Red2");
        checkAbsence("Green1");
        checkAbsence("Green2");
    }

    void checkAbsence(string value) 
    {
        for(int i=0; i < references[value].structures.Count; i++)
        {
            if (references[value].cables[i] == null)
            {
                references[value].structures.RemoveAt(i);
                references[value].cables.RemoveAt(i);
                references[value].connectedTo.RemoveAt(i);
            }
        }
    }


}
