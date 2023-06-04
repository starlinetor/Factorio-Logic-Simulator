using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoomController : MonoBehaviour
{

    //Allows to controll the zoom of the camera
    //The zoom of the camera can be controlled either by writing it in the input field
    //Or with the scroll weel 
    //If a non number is entered in the input field the zoom is changed into the default one

    public float defaultZoom;
    public float zoomIncreaseMod;


    TMP_InputField inputField;
    //New is added to fix error CS0108 hides inherited member 'Component.camera'
    new Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GameObject.Find("ZoomInputField").GetComponent<TMP_InputField>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float zoom;


        if (float.TryParse(inputField.text, out zoom) == false)
        {
            zoom = defaultZoom;
            inputField.text = zoom.ToString();
        }

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            inputField.text = (zoom + Input.mouseScrollDelta.y * zoomIncreaseMod).ToString();
        }
        if (float.Parse(inputField.text) < 1)
        {
            inputField.text = "1";
        }

        camera.orthographicSize = zoom;
    }
}
