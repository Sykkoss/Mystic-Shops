using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayOrderUI : MonoBehaviour
{
    public List<Image> _itemSlots;

    public void InitOrder(List<OrderItems.OrderItem> orderItems)
    {
        int index = 0;
        foreach (OrderItems.OrderItem currentItem in orderItems)
        {
            _itemSlots[index].sprite = GetSpriteDependingItemType(currentItem);
            index++;
        }
    }

    private Sprite GetSpriteDependingItemType(OrderItems.OrderItem currentItem)
    {
        if (currentItem.Type == typeof(CustomPotion))
            return OrderPotionItemUI.Instance.GetPotionSprite(currentItem as OrderItems.Potion);
        else if (currentItem.Type == typeof(CustomYokaiMask))
            return OrderYokaiMaskItemUI.Instance.GetMaskSprite(currentItem as OrderItems.YokaiMask);
        Debug.LogError("Error: Item of type '" + currentItem.GetType().ToString() + "' has no sprite table for orders.");
        return null;
    }

    public void ShowItemAsGiven(int itemIndexInOrder)
    {
        Color itemColor = _itemSlots[itemIndexInOrder].color;

        itemColor.a = 0.25f;
        _itemSlots[itemIndexInOrder].color = itemColor;
    }
}
