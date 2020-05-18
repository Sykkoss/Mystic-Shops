using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPotion : ACustomItem
{
    public PotionColor Color { get; set; }

    [HideInInspector]
    public PotionUpdateSprite _potionUpdateSprite;


    public override bool CheckOrderItem(OrderItems.OrderItem orderItem)
    {
        OrderItems.Potion comparedPotion = (OrderItems.Potion)orderItem;

        if (this.GetType() == comparedPotion.Type &&
            Complexity == comparedPotion.Complexity &&
            Color == comparedPotion.Color)
            return true;
        return false;
    }

    private void Start()
    {
        Color = PotionColor.Empty;
        _potionUpdateSprite = GetComponent<PotionUpdateSprite>();

        if (_potionUpdateSprite == null)
            Debug.LogError("Error: No 'PotionUpdateSprite' on '" + name + "'.");
    }

    public override bool IsSellable()
    {
        return Color != PotionColor.Empty;
    }

    public override bool InteractOnDrop()
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

    private void OnDestroy()
    {
        Slot = null;
        FreeItemSlot();
    }
}
