using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleOrder : MonoBehaviour, IInteractible
{
    public bool HasFulfilledOrder { get; private set; }
    public OrderInfo Info { get; private set; }


    private void Start()
    {
        HasFulfilledOrder = false;
        Info = OrderGeneration.GenerateNewOrder();

        int index = 0;
        foreach (OrderItems.OrderItem currentItem in Info.orderItems)
        {
            OrderItems.Potion customPotion = (OrderItems.Potion)currentItem;

            print("[" + index + "] Type = " + currentItem.Type + "      Color = " + customPotion.Color + "      Complexity = " + customPotion.Complexity);
            index++;
        }
        print("Info.cost: " + Info.cost + "\n");
    }

    public bool Interact(ACustomItem item)
    {
        int itemIndexInOrder = GetItemIndexInOrder(item);

        if (item.IsSellable() && itemIndexInOrder != -1)
        {
            Info.orderItems[itemIndexInOrder].IsGiven = true;
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
}
