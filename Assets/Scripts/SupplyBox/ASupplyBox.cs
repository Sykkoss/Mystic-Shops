using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASupplyBox : MonoBehaviour
{
    public GameObject _itemPrefab;

    public int MaxCapacity { get; set; } = 0;
    public int CurrentCapacity { get; set; } = 0;

    protected DisplaySupplyBox _displaySupplyBox;


    public void SetMaxCapacity(int maxCapacity)
    {
        MaxCapacity = maxCapacity;
        CurrentCapacity = MaxCapacity;
    }

    public void SupplyItem(Vector3 position)
    {
        GameObject spawnedItem;
        DraggableObject draggableItem;

        if (CurrentCapacity >= 1)
        {
            spawnedItem = Instantiate(_itemPrefab, position, Quaternion.identity);
            draggableItem = spawnedItem.GetComponent<DraggableObject>();
            if (draggableItem != null)
            {
                draggableItem.SupplyBox = this;
                StartCoroutine(draggableItem.Dragging());
            }
        }
        else
            Refill();
    }

    protected void Refill()
    {
        CurrentCapacity = MaxCapacity;
        _displaySupplyBox.DisplayEmptySlot(false);
    }


    public abstract void DecrementCurrentCapacity();
}
