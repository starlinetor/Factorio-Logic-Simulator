using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour
{

    //Allows to controll the speed of the camera
    //The speed of the camera can be controlled either by writing it in the input field
    //Or with the scroll weel + CTRL
    //If a non number is entered in the input field the speed is changed into the default one

    public float defaultSpeed;
    public float speedIncreaseMod;


    TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GameObject.Find("SpeedInputField").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed;


        if (float.TryParse(inputField.text, out speed) == false)
        {
            speed = defaultSpeed;
            inputField.text = speed.ToString();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            inputField.text = (speed + Input.mouseScrollDelta.y * speedIncreaseMod).ToString();
        }

        if (float.Parse(inputField.text) < 0)
        {
            inputField.text = "0";
        }

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        transform.position += new Vector3(Horizontal * speed *Time.deltaTime, Vertical * speed * Time.deltaTime, 0);
    }
}
