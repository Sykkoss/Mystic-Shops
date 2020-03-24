using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPotion : ACustomItem
{
    public PotionColor Color { get; set; }

    private PotionUpdateSprite _potionUpdateSprite;


    private void Start()
    {
        Color = PotionColor.Empty;
        SlotPosition = transform.position;
        HasSlotAssigned = false;
        _potionUpdateSprite = GetComponent<PotionUpdateSprite>();

        if (_potionUpdateSprite == null)
            Debug.LogError("Error: No 'PotionUpdateSprite' on '" + name + "'.");
    }

    public override void InteractOnDrop()
    {
        RaycastHit2D hit = GetRaycastHit2D();
        IInteractible interactible;
        PotionColor oldColor = Color;

        if (hit)
        {
            interactible = hit.transform.GetComponent<IInteractible>();

            if (interactible != null)
                interactible.Interact(this);
            else
                ResetPositionToSlot();
        }
        // Reseting position this way allows interactibles to reset potion's position when needed (useful for animations)
        else
            ResetPositionToSlot();

        _potionUpdateSprite.ChangeSpriteColor(oldColor, Color);
    }

    private RaycastHit2D GetRaycastHit2D()
    {
        Vector2 ray;
        RaycastHit2D hit;
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        boxCollider.enabled = false;
        ray = ControlsManager.Instance.GetDragPosition();
        hit = Physics2D.Raycast(ray, Vector2.zero, 100f, ~(1 << 8));
        boxCollider.enabled = true;

        return hit;
    }

    private void OnDestroy()
    {
        FreeItemSlot();
    }
}
