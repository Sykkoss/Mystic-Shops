using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleValve : MonoBehaviour, IInteractible
{
    public ValveColor _valveColor;

    public void Interact(ACustomItem item)
    {
        CustomPotion potion = (CustomPotion)item;

        // Mix potion only if it already has a slot assigned or if a slot is assigned successfully
        if (!potion.HasSlotAssigned)
        {
            if (SlotManager.Instance.AssignFirstSlotAvailable(item))
                MixPotion(potion);
        }
        else
            MixPotion(potion);
    }

    /// <summary>
    /// Mix color and change 'potion' color
    /// </summary>
    /// <param name="potion"></param>
    private void MixPotion(CustomPotion potion)
    {
        potion.Color = MixPotionColor.MixPotion(potion.Color, _valveColor);
        potion.ResetPositionToSlot();
    }
}
