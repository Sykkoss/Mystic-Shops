using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBox : ASupplier
{
    public GameObject _itemPrefab;


    private void Start()
    {
        SetMaxCapacity(2);
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
            StartCoroutine(Refilling());
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

    private IEnumerator Refilling()
    {
        SetIsRefilling(true);

        yield return new WaitForSeconds(_refillTime);

        ResetCurrentCapacity();

        SetIsRefilling(false);
    }
}
