using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemsCost
{
    #region GetType And Redirect

    public static int ComputeItemCost(OrderItems.OrderItem orderItem)
    {
        if (orderItem.Type == typeof(CustomPotion))
            return ComputePotionCost(orderItem.Complexity);
        return 0;
    }

    public static int ComputeItemCost(ACustomItem customItem)
    {
        if (customItem.GetType() == typeof(CustomPotion))
            return ComputePotionCost(customItem.Complexity);
        else if (customItem.GetType() == typeof(CustomYokaiMask))
            return ComputeYokaiMaskCost(customItem.Complexity);
        Debug.LogError("Error: Item of type '" + customItem.GetType().ToString() + "' has no currency value.");
        return 0;
    }

    #endregion GetType And Redirect


    #region Potion
    private static int ComputePotionCost(int complexity)
    {
        return 10 + (5 * (complexity - 1));
    }
    #endregion Potion

    #region Yokai Masks
    private static int ComputeYokaiMaskCost(int complexity)
    {
        return 15 + (5 * (complexity - 1));
    }
    #endregion Yokai Masks
}
