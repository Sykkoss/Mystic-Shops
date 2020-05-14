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
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging(Input.mousePosition);
        }
    }

    private void StartDragging(Vector3 inputPosition)
    {
        Vector2 ray = Camera.main.ScreenToWorldPoint(inputPosition);
        // Casts a raycast ignoring first layer which corresponds to 'Default', letting other 'TouchableItems' available to raycast
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 100f, ~(1 << 0));
        DraggableObject draggableObject;
        TouchableObject touchableObject;
        ASupplier supplier;

        if (hit)
        {
            // Gets a Draggable object and if found, call its Dragging() method for the object to follow the mouse
            if (hit.transform.TryGetComponent<DraggableObject>(out draggableObject))
                StartCoroutine(draggableObject.Dragging());
            else if (hit.transform.TryGetComponent<TouchableObject>(out touchableObject))
                touchableObject.Touching();
            else if (hit.transform.TryGetComponent<ASupplier>(out supplier))
                supplier.Tapped(inputPosition);

            // If no DraggableObject found, search for a Supplier
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
