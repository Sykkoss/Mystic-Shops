using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomYokaiMask : ACustomItem
{
    [SerializeField]
    private YokaiMaskType _maskType;

    public YokaiMaskPaint Paint { get; set; }
    public YokaiMaskType MaskType { get; set; }

    [HideInInspector]
    public PotionUpdateSprite _potionUpdateSprite;


    private void Start()
    {
        MaskType = _maskType;
        Paint = YokaiMaskPaint.None;
        _potionUpdateSprite = GetComponent<PotionUpdateSprite>();

        if (_potionUpdateSprite == null)
            Debug.LogError("Error: No 'PotionUpdateSprite' on '" + name + "'.");
    }

    public override bool IsSellable()
    {
        return Paint != YokaiMaskPaint.None;
    }

    public override bool CheckOrderItem(OrderItems.OrderItem orderItem)
    {
        OrderItems.YokaiMask comparedMask;

        if (orderItem.GetType() != typeof(OrderItems.YokaiMask))
            return false;

        comparedMask = (OrderItems.YokaiMask)orderItem;
        if (this.GetType() == comparedMask.Type &&
            Complexity == comparedMask.Complexity &&
            Paint == comparedMask.Paint &&
            MaskType == comparedMask.MaskType)
            return true;
        return false;
    }

    private void OnDestroy()
    {
        Slot = null;
        FreeItemSlot();
    }
}
