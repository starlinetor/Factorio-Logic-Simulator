using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMover : MonoBehaviour
{

    //This script moves the background following the camera.
    //When the difference between the controller transform and background transform is less than snapping it will move the background on top of the controller
    //Then it snaps the background to the grid

    public int snapping;
    
    GameObject controller;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Controller"); 
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Mathf.Abs(transform.position.x - controller.transform.position.x) > snapping)
        {
            transform.position = new Vector3(controller.transform.position.x, transform.position.y, transform.position.z);
        }
        if (Mathf.Abs(transform.position.y - controller.transform.position.y) > snapping)
        {
            transform.position = new Vector3(transform.position.x, controller.transform.position.y, transform.position.z);
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }
}
