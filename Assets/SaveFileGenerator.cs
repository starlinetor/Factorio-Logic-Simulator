using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveFileGenerator : MonoBehaviour
{

    public GameObject[] prefabs;
    public List<GameObject> structures;
    public bool saving;
    
    // Start is called before the first frame update
    void Start()
    {
        saving = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //generates a save file based on the saves classes
    public void saveFile()
    {
        saving = true;

        structures.Clear();

        foreach (Transform child in transform)
        {
            structures.Add(child.gameObject); 
        }

        SaveJSON saveJSON = new SaveJSON();
        saveJSON.entities = new Entities[structures.Count];

        for (int i = 0; i < structures.Count; i++)
        {
            //initializing classes
            saveJSON.entities[i] = new Entities();
            saveJSON.entities[i].position = new Position();

            //assinging values
            saveJSON.entities[i].entity_number = i;
            saveJSON.entities[i].name = structures[i].tag;
            saveJSON.entities[i].position.x = structures[i].transform.position.x;
            saveJSON.entities[i].position.y = structures[i].transform.position.y;
            saveJSON.entities[i].direction = structures[i].GetComponent<RotateSprite>().rotation;
        }

        string save = JsonUtility.ToJson(saveJSON,true);

        File.WriteAllText(Application.dataPath + "/save.json", save);

        saving = false;
    }

    //checks between all entities if there is any collision with the one we are spawning
    public bool checkOverlaps(GameObject structure)
    {
        string save = File.ReadAllText(Application.dataPath + "/save.json");
        SaveJSON saveJSON = JsonUtility.FromJson<SaveJSON>(save);

        for(int i = 0; i<structures.Count; i++)
        {
            if (checkOverlap(saveJSON.entities[i], structure))
            {
                return true;
            }
        }

        return false;
    }

    #region variables for checkOverlap

    double x1;
    double x2;
    double y1;
    double y2;

    double minX;
    double minY;

    #endregion

    public bool checkOverlap(Entities structure1, GameObject strucure2)
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if(structure1.name == prefabs[i].tag)
            {
                x1 = prefabs[i].GetComponent<snapToGrid>().x;
                y1 = prefabs[i].GetComponent<snapToGrid>().y;
                break;
            }
        }

        x2 = strucure2.GetComponent<snapToGrid>().x;
        y2 = strucure2.GetComponent<snapToGrid>().y;

        //if they are rotated sideways we need to invert x and y aka width and height
        if (structure1.direction % 2 != 0)
        {
            (x1, y1) = (y1, x1);
        }
        
        if(strucure2.GetComponent<RotateSprite>().rotation % 2 != 0)
        {
            (x2, y2) = (y2, x2);
        }

        //we are getting the minimum distance between the two object by summing half of theyr lenght in both axis
        minX = x1 / 2 + x2 / 2;
        minY = y1 / 2 + y2 /2;
        //Debug.Log("Y1 : " + y1.ToString()+ ", Y2 : " + y2.ToString());
        //Debug.Log("Structure id: "+structure1.entity_number.ToString()+", minX: "+minX.ToString()+", minY:" + minY.ToString()+", x distance: "+ Mathf.Abs((float)structure1.position.x - strucure2.transform.position.x).ToString()+", y distance"+ Mathf.Abs((float)structure1.position.y - strucure2.transform.position.y).ToString());

        //check if the distance X and Y are equal or greater than the minimun distance it will return true
        if (Mathf.Abs((float)structure1.position.x - strucure2.transform.position.x) >= minX || Mathf.Abs((float)structure1.position.y - strucure2.transform.position.y) >= minY)
        {
            return false;
        }

        return true;
    }


    //these classes rappresent the json structure for the save file
    #region saves

    [Serializable]
    public class Position
    {
        public double x;
        public double y;
    }

    [Serializable]
    public class Entities
    {
        public int entity_number;
        public string name;
        public Position position;
        public int direction;
    }

    [Serializable]
    public class SaveJSON
    {
        public Entities[] entities;
    }

    #endregion

}