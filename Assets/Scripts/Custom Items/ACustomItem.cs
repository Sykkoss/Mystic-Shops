using UnityEngine;

public abstract class ACustomItem : MonoBehaviour
{
    public Vector2 SlotPosition { get; set; }
    public ASlot Slot { get; set; }
    public bool HasSlotAssigned { get; set; }


    public abstract bool InteractOnDrop();

    public bool ResetPositionToSlot()
    {
        if (!HasSlotAssigned)
            return SlotManager.Instance.AssignFirstSlotAvailable(this, true);
        transform.position = SlotPosition;
        return true;
    }

    public void FreeItemSlot()
    {
        if (Slot != null)
            Slot.FreeSlot();
    }
}
