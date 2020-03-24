using UnityEngine;

public abstract class ACustomItem : MonoBehaviour
{
    public Vector2 SlotPosition { get; set; }
    public ASlot Slot { get; set; }
    public bool HasSlotAssigned { get; set; }


    public abstract void InteractOnDrop();

    public void ResetPositionToSlot()
    {
        transform.position = SlotPosition;
    }

    public void FreeItemSlot()
    {
        if (Slot != null)
            Slot.FreeSlot();
    }
}
