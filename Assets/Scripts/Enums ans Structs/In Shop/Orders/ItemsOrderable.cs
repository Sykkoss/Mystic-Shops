using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemsOrderable
{
    public static readonly List<System.Type> itemTypes = new List<System.Type> { typeof(CustomPotion) };

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
}
