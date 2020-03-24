using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public static ControlsManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        PlatformDependantInputs();
    }

    private void PlatformDependantInputs()
    {
    #if UNITY_STANDALONE
        StandaloneControls();
    #endif
    }

    private void StandaloneControls()
    {
        Vector2 ray;
        RaycastHit2D hit;
        DraggableObject draggableObject;

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(ray, Vector2.zero, 100f, ~(1 << 0));

            if (hit)
            {
                // Gets a Draggable object and if found, call its Dragging() method for the object to follow the mouse
                draggableObject = hit.transform.GetComponent<DraggableObject>();

                if (draggableObject != null)
                    StartCoroutine(draggableObject.Dragging());
            }
        }
    }

    public bool IsDragging()
    {
    #if UNITY_STANDALONE
        return Input.GetMouseButton(0);
    #endif
    }

    public Vector2 GetDragPosition()
    {
    #if UNITY_STANDALONE
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    #endif
    }
}
