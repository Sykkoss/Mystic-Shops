using UnityEngine;

public abstract class ACustomItem : MonoBehaviour
{
    public AItemSlot Slot { get; set; }


    private void Start()
    {
        Slot = null;
    }

    public bool HasSlotAssigned()
    {
        return Slot != null;
    }

    public bool ResetPositionToSlot()
    {
        if (!HasSlotAssigned())
            return ItemSlotManager.Instance.AssignFirstSlotAvailable(this, true);
        transform.position = Slot.SlotPosition;
        return true;
    }

    public void FreeItemSlot()
    {
        if (HasSlotAssigned())
            Slot.FreeSlot();
    }

    public abstract bool IsSellable();
    public abstract bool InteractOnDrop();
}
