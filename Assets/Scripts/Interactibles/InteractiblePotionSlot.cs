using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiblePotionSlot : ASlot, IInteractible
{
    public bool Interact(ACustomItem item)
    {
        return SlotManager.Instance.AssignFirstSlotAvailable(item, true);
    }

    public override void AssignSlot(ACustomItem item)
    {
        CustomPotion potion = (CustomPotion)item;
        Vector3 newSlotPosition = transform.GetChild(0).transform.position;

        newSlotPosition.z = potion.transform.position.z;
        potion.SlotPosition = newSlotPosition;
        potion.Slot = this;
        potion.HasSlotAssigned = true;
        IsOccupied = true;
    }
}
