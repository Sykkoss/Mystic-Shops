using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemsCost
{
    #region GetType And Redirect

    public static int ComputeItemCost(OrderItems.OrderItem orderItem)
    {
        return ComputeItemCostFromType(orderItem.Type, orderItem.Complexity);
    }

    public static int ComputeItemCost(ACustomItem customItem)
    {
        return ComputeItemCostFromType(customItem.GetType(), customItem.Complexity);
    }

    private static int ComputeItemCostFromType(System.Type itemType, int itemComplexity)
    {
        if (itemType == typeof(CustomPotion))
            return ComputePotionCost(itemComplexity);
        else if (itemType == typeof(CustomYokaiMask))
            return ComputeYokaiMaskCost(itemComplexity);
        Debug.LogError("Error: Item of type '" + itemType.ToString() + "' has no currency value.");
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
