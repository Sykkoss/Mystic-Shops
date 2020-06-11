using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleMaskPainter : MonoBehaviour, IInteractible
{
    public MaskPainterType _maskPainterType;

    private MaskPainterSupplier _supplier;


    private void Start()
    {
        _supplier = GetComponent<MaskPainterSupplier>();
        if (_supplier == null)
            Debug.LogError("Error: No 'MaskPainterSupplier' component on gameObject '" + gameObject.name + "'.");
    }

    public bool Interact(ACustomItem item)
    {
        CustomYokaiMask yokaiMask;

        if (item.GetType() != typeof(CustomYokaiMask))
            return false;

        yokaiMask = (CustomYokaiMask)item;
        // Mix potion only if it already has a slot assigned or if a slot is assigned successfully
        if (!yokaiMask.HasSlotAssigned())
        {
            if (yokaiMask._itemSlotManager.AssignFirstSlotAvailable(item, false))
                return MixPaint(yokaiMask);
        }
        else
            return MixPaint(yokaiMask);
        return false;
    }

    /// <summary>
    /// Mix paint and change 'Paint' type
    /// </summary>
    /// <param name="Paint"></param>
    private bool MixPaint(CustomYokaiMask yokaiMask)
    {
        bool changedColor = false;

        if (_supplier.CurrentCapacity > 0)
            changedColor = ChangeMaskPaintType(yokaiMask);

        if (changedColor)
            yokaiMask.Complexity += 1;
        return changedColor;
    }

    private bool ChangeMaskPaintType(CustomYokaiMask yokaiMask)
    {
        YokaiMaskPaint newColor;

        newColor = MixMaskPaint.MixPaint(yokaiMask.PaintType, _maskPainterType);
        if (newColor != yokaiMask.PaintType && !_supplier._isOccupied)
        {
            _supplier.PaintMask(yokaiMask, newColor);
            return true;
        }
        return false;
    }
}
