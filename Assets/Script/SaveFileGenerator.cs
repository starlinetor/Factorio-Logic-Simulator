using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveFileGenerator : MonoBehaviour
{

    public GameObject[] prefabs;
    public List<GameObject> structures;
    public bool saving;
    snapToGrid[] prefabsSnap;
    public SaveJSON saveJSON = new SaveJSON();
    bool loadSaving;

    // Start is called before the first frame update
    void Start()
    {
        saving = false;
        loadSaving = false;

        //cache the snap to grid component of the prefabs
        prefabsSnap = new snapToGrid[prefabs.Length];

        for(int i = 0; i < prefabs.Length; i++)
        {
            prefabsSnap[i] = prefabs[i].GetComponent<snapToGrid>();
        }
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

        //string save = JsonUtility.ToJson(saveJSON,true);

        //File.WriteAllText(Application.dataPath + "/save.json", save);

        saving = false;
    }

    //checks between all entities if there is any collision with the one we are spawning

    #region variables for checkOverlap

    double x1;
    double x2;
    double y1;
    double y2;

    double minX;
    double minY;

    float positionX;
    float positionY;

    #endregion

    public bool checkOverlaps(GameObject structure)
    {
        //read save file
        //string save = File.ReadAllText(Application.dataPath + "/save.json");
        //SaveJSON saveJSON = JsonUtility.FromJson<SaveJSON>(save);


        //get structure x and y and rotate them if necessary

        x2 = structure.GetComponent<snapToGrid>().x;
        y2 = structure.GetComponent<snapToGrid>().y;

        if (structure.GetComponent<RotateSprite>().rotation % 2 != 0)
        {
            (x2, y2) = (y2, x2);
        }

        positionX = structure.transform.position.x;
        positionY = structure.transform.position.y;


        for (int i = 0; i<structures.Count; i++)
        {
            if (checkOverlap(saveJSON.entities[i], structure))
            {
                return true;
            }
        }

        return false;
    }



    public bool checkOverlap(Entities structure1, GameObject strucure2)
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if(structure1.name == prefabs[i].tag)
            {
                x1 = prefabsSnap[i].x;
                y1 = prefabsSnap[i].y;
                break;
            }
        }

        //if they are rotated sideways we need to invert x and y aka width and height
        if (structure1.direction % 2 != 0)
        {
            (x1, y1) = (y1, x1);
        }


        //we are getting the minimum distance between the two object by summing half of theyr lenght in both axis
        minX = x1 / 2 + x2 / 2;
        minY = y1 / 2 + y2 /2;
        //Debug.Log("Y1 : " + y1.ToString()+ ", Y2 : " + y2.ToString());
        //Debug.Log("Structure id: "+structure1.entity_number.ToString()+", minX: "+minX.ToString()+", minY:" + minY.ToString()+", x distance: "+ Mathf.Abs((float)structure1.position.x - strucure2.transform.position.x).ToString()+", y distance"+ Mathf.Abs((float)structure1.position.y - strucure2.transform.position.y).ToString());

        //check if the distance X and Y are equal or greater than the minimun distance it will return true
        if (Mathf.Abs((float)structure1.position.x - positionX) >= minX || Mathf.Abs((float)structure1.position.y - positionY) >= minY)
        {
            return false;
        }

        return true;
    }

    public void save()
    {
        if(loadSaving = true)
        {
            return;
        }



        string save = JsonUtility.ToJson(saveJSON, true);
        File.WriteAllText(Application.dataPath + "/save.json", save);

        
    }

    GameObject structure;

    public void laod()
    {
        
       foreach (Transform child in transform)
       {
            Destroy(child.gameObject);
       }

        //read save file
        string save = File.ReadAllText(Application.dataPath + "/save.json");
        SaveJSON loadSave = JsonUtility.FromJson<SaveJSON>(save);

        for(int i = 0; i<loadSave.entities.Length; i++)
        {   
            foreach(GameObject prefab in prefabs)
            {
                if (prefab.tag == loadSave.entities[i].name)
                {
                    structure = Instantiate(prefab.gameObject);
                }
            }
            structure.transform.parent = transform;
            structure.transform.position = new Vector2((float)loadSave.entities[i].position.x, (float)loadSave.entities[i].position.y);
            structure.GetComponent<RotateSprite>().rotation = loadSave.entities[i].direction;
        }

        saveFile();
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
