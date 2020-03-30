using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public SupplyBox SupplyBox { get; set; } = null;

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
        ACustomItem customItem = GetComponent<ACustomItem>();
        bool interactResponse;

        interactResponse = customItem.InteractOnDrop();

        // Decrements supply box capacity only if the drag was coming from a supply box and if the drop was successfull
        // (It can be unsuccessfull if not enough slot available)
        if (SupplyBox != null && interactResponse)
        {
            SupplyBox.DecrementCurrentCapacity();
            SupplyBox = null;
        }
    }
}
