using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPotion : ACustomItem
{
    public PotionColor Color { get; set; }

    [HideInInspector]
    public PotionUpdateSprite _potionUpdateSprite;


    private void Start()
    {
        Color = PotionColor.Empty;
        _potionUpdateSprite = GetComponent<PotionUpdateSprite>();

        if (_potionUpdateSprite == null)
            Debug.LogError("Error: No 'PotionUpdateSprite' on '" + name + "'.");
    }

    public override bool IsSellable()
    {
        return Color != PotionColor.Empty;
    }

    public override bool CheckOrderItem(OrderItems.OrderItem orderItem)
    {
        OrderItems.Potion comparedPotion = (OrderItems.Potion)orderItem;

        if (this.GetType() == comparedPotion.Type &&
            Complexity == comparedPotion.Complexity &&
            Color == comparedPotion.Color)
            return true;
        return false;
    }

    private void OnDestroy()
    {
        Slot = null;
        FreeItemSlot();
    }
}
