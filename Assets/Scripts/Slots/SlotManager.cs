using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance;

    private List<ASlot> _slots;


    void Start()
    {
        ASlot currentSlot;

        _slots = new List<ASlot>();
        foreach (Transform child in transform)
        {
            currentSlot = child.GetComponent<ASlot>();
            if (currentSlot != null)
                _slots.Add(currentSlot);
        }
        Instance = this;
    }

    public bool AssignFirstSlotAvailable(ACustomItem item)
    {
        bool hasAssigned = false;

        foreach (ASlot currentSlot in _slots)
        {
            if (!item.HasSlotAssigned && !currentSlot.IsOccupied)
            {
                currentSlot.AssignSlot(item);
                hasAssigned = true;
                break;
            }
        }

        if (hasAssigned)
            return true;
        else
        {
            item.ResetPositionToSlot();
            return false;
        }
    }
}
