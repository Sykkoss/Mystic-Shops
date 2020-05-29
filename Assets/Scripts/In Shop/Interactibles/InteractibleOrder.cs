using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleOrder : MonoBehaviour, IInteractible
{
    public bool HasFulfilledOrder { get; private set; }
    public OrderInfo Info { get; private set; }

    private DisplayOrderUI _displayOrderUI;


    public bool Interact(ACustomItem item)
    {
        int itemIndexInOrder = GetItemIndexInOrder(item);

        if (item.IsSellable() && itemIndexInOrder != -1)
        {
            Info.orderItems[itemIndexInOrder].IsGiven = true;
            _displayOrderUI.ShowItemAsGiven(itemIndexInOrder);
            item.FreeItemSlot();
            Destroy(item.gameObject);
            if (IsOrderComplete())
                HasFulfilledOrder = true;
            return true;
        }
        return false;
    }

    private bool IsOrderComplete()
    {
        int numberComplete = 0;

        foreach (OrderItems.OrderItem currentItem in Info.orderItems)
        {
            if (currentItem.IsGiven)
                numberComplete += 1;
        }
        if (numberComplete >= Info.orderItems.Count)
            return true;
        return false;
    }

    private int GetItemIndexInOrder(ACustomItem item)
    {
        int index = 0;

        foreach (OrderItems.OrderItem orderItem in Info.orderItems)
        {
            if (item.CheckOrderItem(orderItem) && !orderItem.IsGiven)
                return index;
            index += 1;
        }
        return -1;
    }

    public void InitOrder()
    {
        HasFulfilledOrder = false;
        Info = OrderGeneration.GenerateNewOrder();
        _displayOrderUI = GetComponent<DisplayOrderUI>();
        _displayOrderUI.InitOrder(Info.orderItems);
    }

    public void ShowOrder(bool shouldShow)
    {
        _displayOrderUI.gameObject.SetActive(shouldShow);
        gameObject.SetActive(shouldShow);
    }

    public void DestroyOrder()
    {
        Destroy(_displayOrderUI.gameObject);
        Destroy(this.gameObject);
    }

    public void SetOrderPositionOnScreen(Vector3 orderPosition)
    {
        transform.position = orderPosition;
    }
}
