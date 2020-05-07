using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotManager : MonoBehaviour
{
    public static ItemSlotManager Instance;

    private List<AItemSlot> _slots;


    void Start()
    {
        AItemSlot currentSlot;

        _slots = new List<AItemSlot>();
        foreach (Transform child in transform)
        {
            currentSlot = child.GetComponent<AItemSlot>();
            if (currentSlot != null)
                _slots.Add(currentSlot);
        }
        Instance = this;
    }

    public bool AssignFirstSlotAvailable(ACustomItem item, bool shouldResetPosition)
    {
        bool hasAssigned = false;

        foreach (AItemSlot currentSlot in _slots)
        {
            if (!item.HasSlotAssigned() && !currentSlot.IsOccupied)
            {
                currentSlot.AssignSlot(item);
                hasAssigned = true;
                break;
            }
        }

        if (item.HasSlotAssigned() || hasAssigned)
        {
            if (shouldResetPosition)
                item.ResetPositionToSlot();
            return true;
        }
        else
        {
            // Destroy item if no slot found and do not already has a slot (since it was instanciated by supplybox)
            if (!item.HasSlotAssigned())
                Destroy(item.gameObject);
            return false;
        }
    }
}
