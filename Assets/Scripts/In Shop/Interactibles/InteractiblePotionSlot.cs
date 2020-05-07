using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiblePotionSlot : AItemSlot, IInteractible
{
    public bool Interact(ACustomItem item)
    {
        return ItemSlotManager.Instance.AssignFirstSlotAvailable(item, true);
    }

    public override void AssignSlot(ACustomItem item)
    {
        CustomPotion potion = (CustomPotion)item;

        potion.Slot = this;
        IsOccupied = true;
    }
}
