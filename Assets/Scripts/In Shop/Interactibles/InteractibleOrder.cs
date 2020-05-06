using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleOrder : MonoBehaviour, IInteractible
{
    public bool HasFulfilledOrder { get; private set; }


    private void Start()
    {
        HasFulfilledOrder = false;
    }

    public bool Interact(ACustomItem item)
    {
        if (item.IsSellable())
        {
            item.FreeItemSlot();
            Destroy(item.gameObject);
            HasFulfilledOrder = true;
            return true;
        }
        return false;
    }
}
