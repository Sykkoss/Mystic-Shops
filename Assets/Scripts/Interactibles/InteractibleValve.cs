using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleValve : MonoBehaviour, IInteractible
{
    public ValveColor _valveColor;

    private ValveSupplier _supplier;


    private void Start()
    {
        _supplier = GetComponent<ValveSupplier>();
        if (_supplier == null)
            Debug.LogError("Error: No 'ValveSupplier' component on gameObject '" + gameObject.name + "'.");
    }

    public bool Interact(ACustomItem item)
    {
        CustomPotion potion = (CustomPotion)item;

        // Mix potion only if it already has a slot assigned or if a slot is assigned successfully
        if (!potion.HasSlotAssigned)
        {
            if (SlotManager.Instance.AssignFirstSlotAvailable(item))
                return MixPotion(potion);
        }
        else
            return MixPotion(potion);
        return false;
    }

    /// <summary>
    /// Mix color and change 'potion' color
    /// </summary>
    /// <param name="potion"></param>
    private bool MixPotion(CustomPotion potion)
    {
        if (_supplier.CurrentCapacity > 0)
        {
            ChangePotionColor(potion);
        }
        return potion.ResetPositionToSlot();
    }

    private void ChangePotionColor(CustomPotion potion)
    {
        PotionColor newColor;

        newColor = MixPotionColor.MixPotion(potion.Color, _valveColor);
        if (newColor != potion.Color)
        {
            _supplier.FillPotion();
            potion.Color = newColor;
        }
    }
}
