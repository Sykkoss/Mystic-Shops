using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemsOrderable
{
    public static readonly List<System.Type> itemTypes = new List<System.Type>
    {
        typeof(CustomPotion),
        typeof(CustomYokaiMask)
    };

    #region Potions
    /* PotionColors depending on their complexity */
    public static readonly Dictionary<int, List<PotionColor>> potionColorsComplexity = new Dictionary<int, List<PotionColor>>
    {
        { 1,
            new List<PotionColor>
            {
                PotionColor.Blue,
                PotionColor.Red,
                PotionColor.Yellow
            }
        },
        { 2,
            new List<PotionColor>
            {
                PotionColor.Orange,
                PotionColor.Green,
                PotionColor.Purple
            }
        },
        { 3,
            new List<PotionColor>
            {
                PotionColor.Brown
            }
        }
    };
    #endregion Potions

    #region Yokai Masks
    /* Masks painting depending on their complexity */
    public static readonly Dictionary<int, List<YokaiMaskPaint>> yokaiMaskPaintComplexity = new Dictionary<int, List<YokaiMaskPaint>>
    {
        { 1,
            new List<YokaiMaskPaint>
            {
                YokaiMaskPaint.Point,
                YokaiMaskPaint.Diamond,
                YokaiMaskPaint.Flame
            }
        },
        { 2,
            new List<YokaiMaskPaint>
            {
                YokaiMaskPaint.PointDiamond,
                YokaiMaskPaint.PointFlame,
                YokaiMaskPaint.DiamondFlame
            }
        },
        { 3,
            new List<YokaiMaskPaint>
            {
                YokaiMaskPaint.PointDiamondFlame
            }
        }
    };
    #endregion Yokai Masks
}
