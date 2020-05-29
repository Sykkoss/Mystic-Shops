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
            OrderItems.Potion customPotion = (OrderItems.Potion)currentItem;

            _itemSlots[index].sprite = OrderPotionItemUI.Instance.GetPotionSprite(customPotion.Color);
            index++;
        }
    }

    public void ShowItemAsGiven(int itemIndexInOrder)
    {
        Color itemColor = _itemSlots[itemIndexInOrder].color;

        itemColor.a = 0.25f;
        _itemSlots[itemIndexInOrder].color = itemColor;
    }
}
