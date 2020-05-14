using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public float _moveTime;

    public ClientData Infos { get; private set; }
    public ClientSlot Slot { get; private set; }
    public ClientState State { get; private set; }

    private InteractibleOrder _interactibleOrder;


    private void Awake()
    {
        Slot = null;
        _interactibleOrder = transform.GetChild(0).GetComponent<InteractibleOrder>();
        if (_interactibleOrder == null)
            Debug.LogError("Error: No Component 'InteractibleOrder' were found on gameObject '" + transform.GetChild(0).name + "'. Could not continue.");
        else
        {
            // Disable Order until the client actually orders
            _interactibleOrder.gameObject.SetActive(false);
        }
    }

    public bool HasSlotAssigned()
    {
        return Slot != null;
    }


    #region Client creation

    public void CreateClient(ClientData newClientData)
    {
        Infos = newClientData;
        gameObject.name = Infos.name;
        SetSprite(Infos.sprite);
        ClientSlotManager.Instance.AssignRandomSlotAvailable(this);
        if (HasSlotAssigned())
            StartCoroutine(MoveTowardsDestination(Slot.SlotPosition, Order));
    }

    private void SetSprite(Sprite newSprite)
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteRenderer == null)
            Debug.LogError("Error: No 'SpriteRenderer' component was found on gameObject '" + gameObject.name + "'.");
        _spriteRenderer.sprite = newSprite;
    }

    public void SetSlot(ClientSlot newSlot)
    {
        Slot = newSlot;
    }

    #endregion Client creation


    #region States

    private IEnumerator MoveTowardsDestination(Vector2 destination, System.Action methodToCallWhenFinished)
    {
        float elapsedTime = 0;
        Vector2 startPosition = transform.localPosition;

        State = ClientState.Moving;
        destination.y = transform.position.y;
        while (elapsedTime < _moveTime)
        {
            transform.localPosition = Vector2.Lerp(startPosition, destination, (elapsedTime / _moveTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = destination;
        methodToCallWhenFinished();
    }

    private void Order()
    {
        State = ClientState.Ordering;

        //Animation
        _interactibleOrder.gameObject.SetActive(true);
        StartCoroutine(WaitForOrder());
    }

    private IEnumerator WaitForOrder()
    {
        State = ClientState.Waiting;

        while (!_interactibleOrder.HasFulfilledOrder)
            yield return null;

        _interactibleOrder.gameObject.SetActive(false);
        HasBeenServed();
    }

    private void HasBeenServed()
    {
        Vector2 outOfScreenDestination = ClientDestination.ComputeSpawnOrQuitPosition();

        PlayerMoneyInLevel.Instance.EarnMoney(10);
        StartCoroutine(MoveTowardsDestination(outOfScreenDestination, DestroyClient));
    }

    private void DestroyClient()
    {
        Slot.FreeSlot();
        Destroy(this.gameObject);
    }

    #endregion States
}
