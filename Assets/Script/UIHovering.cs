using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHovering : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{

    //marks if the UI element is hovered

    public bool hovering;

    void Start()
    {
        hovering = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }
}
