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

    public override bool InteractOnDrop()
    {
        RaycastHit2D hit = GetRaycastHit2D();
        IInteractible interactible;
        PotionColor oldColor = Color;
        bool interactResponse;

        if (hit)
        {
            interactible = hit.transform.GetComponent<IInteractible>();

            if (interactible != null)
            {
                interactResponse = interactible.Interact(this);
                _potionUpdateSprite.ChangeSpriteColor(oldColor, Color);

                return interactResponse;
            }
            else
                return ResetPositionToSlot();
        }
        // Reseting position this way allows interactibles to reset potion's position when needed (useful for animations)
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
        FreeItemSlot();
    }
}
