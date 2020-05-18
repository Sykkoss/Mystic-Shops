﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OrderGeneration
{
    public static OrderInfo GenerateNewOrder()
    {
        OrderInfo newOrder = new OrderInfo();

        newOrder.orderItems = GenerateItemsInOrder();
        newOrder.cost = ComputeOrderCost(newOrder.orderItems);
        return newOrder;
    }

    private static int ComputeOrderCost(List<OrderItems.OrderItem> items)
    {
        int totalCost = 0;

        foreach (OrderItems.OrderItem currentItem in items)
            totalCost += 10 + (5 * (currentItem.Complexity - 1));
        return totalCost;
    }

    private static List<OrderItems.OrderItem> GenerateItemsInOrder()
    {
        List<OrderItems.OrderItem> orderItems = new List<OrderItems.OrderItem>();
        int numberItemsInOrder = PlayerDifficulty.Instance.GetNumberItemsForOrder();

        while (numberItemsInOrder > 0)
        {
            int itemComplexity = PlayerDifficulty.Instance.GetItemComplexityForOrder();
            orderItems.Add(GenerateItem(itemComplexity));
            numberItemsInOrder -= 1;
        }
        return orderItems;
    }

    private static OrderItems.OrderItem GenerateItem(int itemComplexity)
    {
        int randomTypeIndex = Random.Range(0, ItemsOrderable.itemTypes.Count);
        System.Type item = ItemsOrderable.itemTypes[randomTypeIndex];

        if (item == typeof(CustomPotion))
            return GenerateCustomPotion(itemComplexity);
        return null;
    }


    #region Different Types

    private static OrderItems.OrderItem GenerateCustomPotion(int itemComplexity)
    {
        OrderItems.Potion potion = new OrderItems.Potion();
        PotionColor randomColorWithComplexity;
        int potionColorIndex;

        potion.Complexity = itemComplexity;

        potionColorIndex = Random.Range(0, ItemsOrderable.potionColorsComplexity[itemComplexity].Count);
        randomColorWithComplexity = ItemsOrderable.potionColorsComplexity[itemComplexity][potionColorIndex];
        potion.Color = randomColorWithComplexity;
        return potion;
    }

    #endregion Different Types
}