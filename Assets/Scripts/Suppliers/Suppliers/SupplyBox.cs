using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBox : ASupplier
{
    public GameObject _itemPrefab;


    private void Start()
    {
        SetMaxCapacity(5);
    }

    public override void Tapped(Vector3 positionTapped)
    {
        SupplyItem(positionTapped);
    }

    public void SupplyItem(Vector3 position)
    {
        if (CurrentCapacity >= 1)
            SpawnItem(position);
        else
            Refill();
    }

    private void SpawnItem(Vector3 position)
    {
        GameObject spawnedItem;
        DraggableObject draggableItem;

        spawnedItem = Instantiate(_itemPrefab, position, Quaternion.identity);
        draggableItem = spawnedItem.GetComponent<DraggableObject>();
        if (draggableItem != null)
        {
            draggableItem.SupplyBox = this;
            StartCoroutine(draggableItem.Dragging());
        }
    }

    protected void Refill()
    {
        ResetCurrentCapacity();
    }
}
