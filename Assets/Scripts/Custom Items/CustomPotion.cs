using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPotion : MonoBehaviour, ICustomItem
{
    public PotionColor Color { get; set; }
    public Vector2 SlotPosition { get; set; }


    private void Start()
    {
        Color = PotionColor.Empty;
        SlotPosition = Vector2.zero;
    }

    public void InteractOnDrop()
    {
        RaycastHit2D hit = GetRaycastHit2D();
        IInteractible interactible;

        if (hit)
        {
            interactible = hit.transform.GetComponent<IInteractible>();

            if (interactible != null)
                interactible.Interact(this);
            else
                ResetPositionToSlot();
        }
        else
            ResetPositionToSlot();

        print("My color is: " + Color.ToString());
    }

    public void ResetPositionToSlot()
    {
        transform.position = SlotPosition;
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
}
