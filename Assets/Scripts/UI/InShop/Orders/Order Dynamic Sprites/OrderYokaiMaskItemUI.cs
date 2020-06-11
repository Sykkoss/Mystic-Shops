using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderYokaiMaskItemUI : MonoBehaviour
{
    [System.Serializable]
    public struct MaskAssociation
    {
        public YokaiMaskPaint paint;
        public Sprite sprite;
    }

    public static OrderYokaiMaskItemUI Instance;
    public List<MaskAssociation> _kitsuneMasksAssociations;
    public List<MaskAssociation> _demonMasksAssociations;


    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetMaskSprite(OrderItems.YokaiMask orderItem)
    {
        YokaiMaskPaint maskPaint = orderItem.Paint;
        YokaiMaskType maskType = orderItem.MaskType;

        if (maskType == YokaiMaskType.Kitsune)
            return GetSpriteDependingOnMaskType(maskPaint, _kitsuneMasksAssociations);
        else if (maskType == YokaiMaskType.Demon)
            return GetSpriteDependingOnMaskType(maskPaint, _demonMasksAssociations);
        return null;

    }

    private Sprite GetSpriteDependingOnMaskType(YokaiMaskPaint maskPaint, List<MaskAssociation> maskAssociations)
    {
        foreach (MaskAssociation currentAssociation in maskAssociations)
        {
            if (currentAssociation.paint == maskPaint)
                return currentAssociation.sprite;
        }
        return null;
    }
}
