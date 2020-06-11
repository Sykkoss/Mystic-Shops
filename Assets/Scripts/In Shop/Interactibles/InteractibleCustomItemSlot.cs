using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleCustomItemSlot : AItemSlot, IInteractible
{
    public string _interactibleItemTypeString;

    private ItemSlotManager _itemSlotManager;


    private void Start()
    {
        _itemSlotManager = transform.parent.GetComponent<ItemSlotManager>();
    }

    public bool Interact(ACustomItem item)
    {
        return _itemSlotManager.AssignFirstSlotAvailable(item, true);
    }

    public override void AssignSlot(ACustomItem item)
    {
        System.Type interactibleType = System.Type.GetType(_interactibleItemTypeString);

        if (item.GetType() == interactibleType)
        {
            item.Slot = this;
            IsOccupied = true;
        }
        else
            item.Slot = null;
    }
}
