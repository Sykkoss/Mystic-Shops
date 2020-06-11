using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomYokaiMask : ACustomItem
{
    [SerializeField]
    private YokaiMaskType _maskType;

    public YokaiMaskPaint PaintType { get; set; }
    public YokaiMaskType MaskType { get; set; }

    [HideInInspector]
    public PotionUpdateSprite _potionUpdateSprite;


    private void Start()
    {
        MaskType = _maskType;
        PaintType = YokaiMaskPaint.None;
        _potionUpdateSprite = GetComponent<PotionUpdateSprite>();

        if (_potionUpdateSprite == null)
            Debug.LogError("Error: No 'PotionUpdateSprite' on '" + name + "'.");
    }

    public override bool IsSellable()
    {
        return PaintType != YokaiMaskPaint.None;
    }

    public override bool CheckOrderItem(OrderItems.OrderItem orderItem)
    {
        OrderItems.Potion comparedPotion = (OrderItems.Potion)orderItem;

        /*if (this.GetType() == comparedPotion.Type &&
            Complexity == comparedPotion.Complexity &&
            PaintType == comparedPotion.Color)
            return true;*/
        return false;
    }

    private void OnDestroy()
    {
        Slot = null;
        FreeItemSlot();
    }
}
