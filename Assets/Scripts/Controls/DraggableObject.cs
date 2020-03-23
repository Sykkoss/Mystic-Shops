using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public IEnumerator Dragging()
    {
        while (ControlsManager.Instance.IsDragging())
        {
            transform.position = ControlsManager.Instance.GetDragPosition();
            yield return null;
        }

        // When mouse or touch is released, interact with item the object is dropped on
        Drop();
    }

    private void Drop()
    {
        ICustomItem customItem = GetComponent<ICustomItem>();

        customItem.InteractOnDrop();
    }
}
