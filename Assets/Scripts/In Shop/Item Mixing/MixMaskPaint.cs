using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MixMaskPaint
{
    public static YokaiMaskPaint MixPaint(YokaiMaskPaint paint, MaskPainterType painterType)
    {
        if (paint == YokaiMaskPaint.None)
        {
            // MaskPainterType has same 1, 2 and 3 indexes as YokaiMaskPaint (+1 because 0 is None)
            return (YokaiMaskPaint)painterType + 1;
        }

        if (painterType == MaskPainterType.Point)
        {
            // If potion already contain Red, do not change its color
            if (paint == YokaiMaskPaint.Point || paint == YokaiMaskPaint.PointFlame ||
                paint == YokaiMaskPaint.PointDiamond || paint == YokaiMaskPaint.PointDiamondFlame)
                return paint;
            else if (paint == YokaiMaskPaint.Diamond)
                return YokaiMaskPaint.PointDiamond;
            else if (paint == YokaiMaskPaint.Flame)
                return YokaiMaskPaint.PointFlame;
            else if (paint == YokaiMaskPaint.DiamondFlame)
                return YokaiMaskPaint.PointDiamondFlame;
        }

        if (painterType == MaskPainterType.Diamond)
        {
            // If potion already contain Blue, do not change its color
            if (paint == YokaiMaskPaint.Diamond || paint == YokaiMaskPaint.DiamondFlame ||
                paint == YokaiMaskPaint.PointDiamond || paint == YokaiMaskPaint.PointDiamondFlame)
                return paint;
            else if (paint == YokaiMaskPaint.Point)
                return YokaiMaskPaint.PointDiamond;
            else if (paint == YokaiMaskPaint.Flame)
                return YokaiMaskPaint.DiamondFlame;
            else if (paint == YokaiMaskPaint.PointFlame)
                return YokaiMaskPaint.PointDiamondFlame;
        }

        if (painterType == MaskPainterType.Flame)
        {
            // If potion already contain Yellow, do not change its color
            if (paint == YokaiMaskPaint.Flame || paint == YokaiMaskPaint.DiamondFlame ||
                paint == YokaiMaskPaint.PointFlame || paint == YokaiMaskPaint.PointDiamondFlame)
                return paint;
            else if (paint == YokaiMaskPaint.Point)
                return YokaiMaskPaint.PointFlame;
            else if (paint == YokaiMaskPaint.Diamond)
                return YokaiMaskPaint.DiamondFlame;
            else if (paint == YokaiMaskPaint.PointDiamond)
                return YokaiMaskPaint.PointDiamondFlame;
        }

        // If nothing matches, do not change potion's color
        return paint;
    }
}
