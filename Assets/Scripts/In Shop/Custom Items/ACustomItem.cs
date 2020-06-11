using UnityEngine;

public abstract class ACustomItem : MonoBehaviour
{
    public ItemSlotManager _itemSlotManager;
    public AItemSlot Slot { get; set; }
    public int Complexity { get; set; }

    private void Start()
    {
        Slot = null;
        Complexity = 0;
    }

    public void SetItemSlotManager(ItemSlotManager newSlotManager)
    {
        _itemSlotManager = newSlotManager;
    }

    public bool HasSlotAssigned()
    {
        return Slot != null;
    }

    public bool ResetPositionToSlot()
    {
        if (!HasSlotAssigned())
            return _itemSlotManager.AssignFirstSlotAvailable(this, true);
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
    public abstract bool CheckOrderItem(OrderItems.OrderItem orderItem);
}
