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

        if (painterType == MaskPainterType.Drop)
        {
            // If potion already contain Red, do not change its color
            if (paint == YokaiMaskPaint.Drop || paint == YokaiMaskPaint.DropFire ||
                paint == YokaiMaskPaint.DropEye || paint == YokaiMaskPaint.DropEyeFire)
                return paint;
            else if (paint == YokaiMaskPaint.Eye)
                return YokaiMaskPaint.DropEye;
            else if (paint == YokaiMaskPaint.Fire)
                return YokaiMaskPaint.DropFire;
            else if (paint == YokaiMaskPaint.EyeFire)
                return YokaiMaskPaint.DropEyeFire;
        }

        if (painterType == MaskPainterType.Eye)
        {
            // If potion already contain Blue, do not change its color
            if (paint == YokaiMaskPaint.Eye || paint == YokaiMaskPaint.EyeFire ||
                paint == YokaiMaskPaint.DropEye || paint == YokaiMaskPaint.DropEyeFire)
                return paint;
            else if (paint == YokaiMaskPaint.Drop)
                return YokaiMaskPaint.DropEye;
            else if (paint == YokaiMaskPaint.Fire)
                return YokaiMaskPaint.EyeFire;
            else if (paint == YokaiMaskPaint.DropFire)
                return YokaiMaskPaint.DropEyeFire;
        }

        if (painterType == MaskPainterType.Fire)
        {
            // If potion already contain Yellow, do not change its color
            if (paint == YokaiMaskPaint.Fire || paint == YokaiMaskPaint.EyeFire ||
                paint == YokaiMaskPaint.DropFire || paint == YokaiMaskPaint.DropEyeFire)
                return paint;
            else if (paint == YokaiMaskPaint.Drop)
                return YokaiMaskPaint.DropFire;
            else if (paint == YokaiMaskPaint.Eye)
                return YokaiMaskPaint.EyeFire;
            else if (paint == YokaiMaskPaint.DropEye)
                return YokaiMaskPaint.DropEyeFire;
        }

        // If nothing matches, do not change potion's color
        return paint;
    }
}
