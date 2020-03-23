using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MixPotionColor
{
    public static PotionColor MixPotion(PotionColor potionColor, ValveColor valveColor)
    {
        if (potionColor == PotionColor.Empty)
        {
            // ValveColor has same 1, 2 and 3 indexes as PotionColor (+1 because 0 is Empty)
            return (PotionColor)valveColor + 1;
        }

        if (valveColor == ValveColor.Red)
        {
            // If potion already contain Red, do not change its color
            if (potionColor == PotionColor.Red || potionColor == PotionColor.Orange ||
                potionColor == PotionColor.Purple || potionColor == PotionColor.Brown)
                return potionColor;
            else if (potionColor == PotionColor.Blue)
                return PotionColor.Purple;
            else if (potionColor == PotionColor.Yellow)
                return PotionColor.Orange;
            else if (potionColor == PotionColor.Green)
                return PotionColor.Brown;
        }

        if (valveColor == ValveColor.Blue)
        {
            // If potion already contain Blue, do not change its color
            if (potionColor == PotionColor.Blue || potionColor == PotionColor.Green ||
                potionColor == PotionColor.Purple || potionColor == PotionColor.Brown)
                return potionColor;
            else if (potionColor == PotionColor.Red)
                return PotionColor.Purple;
            else if (potionColor == PotionColor.Yellow)
                return PotionColor.Green;
            else if (potionColor == PotionColor.Orange)
                return PotionColor.Brown;
        }

        if (valveColor == ValveColor.Yellow)
        {
            // If potion already contain Yellow, do not change its color
            if (potionColor == PotionColor.Yellow || potionColor == PotionColor.Green ||
                potionColor == PotionColor.Orange || potionColor == PotionColor.Brown)
                return potionColor;
            else if (potionColor == PotionColor.Red)
                return PotionColor.Orange;
            else if (potionColor == PotionColor.Blue)
                return PotionColor.Green;
            else if (potionColor == PotionColor.Purple)
                return PotionColor.Brown;
        }

        // If nothing matches, do not change potion's color
        return potionColor;
    }
}
