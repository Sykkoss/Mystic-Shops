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

    public bool InteractOnDrop()
    {
        RaycastHit2D hit = GetRaycastHit2D();
        IInteractible interactible;
        bool hasInteracted = false;

        if (hit)
        {
            interactible = hit.transform.GetComponent<IInteractible>();

            if (interactible != null)
            {
                hasInteracted = interactible.Interact(this);
            }
        }

        // Reseting position this way allows interactibles to reset potion's position when needed (useful for animations)
        if (hasInteracted)
            return true;
        else
            return ResetPositionToSlot();
    }

    private RaycastHit2D GetRaycastHit2D()
    {
        Vector2 ray;
        RaycastHit2D hit;
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        boxCollider.enabled = false;
        ray = ControlsManager.Instance.GetDragPosition();
        // Casts a raycast ignoring 8th layer which corresponds to 'TouchableItems'
        hit = Physics2D.Raycast(ray, Vector2.zero, 100f, ~(1 << 8));
        boxCollider.enabled = true;

        return hit;
    }

    public abstract bool IsSellable();
    public abstract bool CheckOrderItem(OrderItems.OrderItem orderItem);
}
