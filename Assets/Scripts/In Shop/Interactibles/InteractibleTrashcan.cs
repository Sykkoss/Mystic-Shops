using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleTrashcan : MonoBehaviour, IInteractible
{
    public bool Interact(ACustomItem item)
    {
        if (item.IsSellable())
        {
            item.FreeItemSlot();
            PlayerMoneyInLevel.Instance.LooseMoney(ItemsCost.ComputeItemCost(item));
            Destroy(item.gameObject);
            return true;
        }
        return false;
    }
}
