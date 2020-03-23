using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleValve : MonoBehaviour, IInteractible
{
    public ValveColor _valveColor;

    public void Interact(ICustomItem item)
    {
        CustomPotion potion = (CustomPotion)item;

        // Mix color and change current potion color
        potion.Color = MixPotionColor.MixPotion(potion.Color, _valveColor);

        potion.ResetPositionToSlot();
    }
}
