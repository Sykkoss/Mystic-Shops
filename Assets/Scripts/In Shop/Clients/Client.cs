using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public float _moveTime;
    public GameObject _pileOfCoinPrefab;

    public ClientData Infos { get; private set; }
    public ClientSlot Slot { get; private set; }
    public ClientState State { get; private set; }

    private InteractibleOrder _interactibleOrder;


    private void Awake()
    {
        Slot = null;
    }

    public bool HasSlotAssigned()
    {
        return Slot != null;
    }


    #region Client creation

    public void CreateClient(ClientData newClientData, InteractibleOrder order)
    {
        Infos = newClientData;
        _interactibleOrder = order;
        _interactibleOrder.InitOrder();
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

    private IEnumerator MoveTowardsDestination(Vector3 destination, System.Action methodToCallWhenFinished)
    {
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;

        State = ClientState.Moving;
        while (elapsedTime < _moveTime)
        {
            if (!PlayerTime.Instance.IsPaused)
            {
                transform.position = Vector3.Lerp(startPosition, destination, (elapsedTime / _moveTime));
                elapsedTime += Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();
        }
        transform.position = destination;
        methodToCallWhenFinished();
    }

    private void Order()
    {
        State = ClientState.Ordering;

        //Animation
        _interactibleOrder.ShowOrder(true);
        _interactibleOrder.SetOrderPositionOnScreen(transform.GetChild(0).position);
        StartCoroutine(WaitForOrder());
    }

    private IEnumerator WaitForOrder()
    {
        State = ClientState.Waiting;

        while (!_interactibleOrder.HasFulfilledOrder)
            yield return null;

        _interactibleOrder.ShowOrder(false);
        HasBeenServed();
    }

    private void HasBeenServed()
    {
        Vector2 outOfScreenDestination = ClientDestination.ComputeSpawnOrQuitPosition();

        PutMoneyOnCounter();
        StartCoroutine(MoveTowardsDestination(outOfScreenDestination, DestroyClient));
    }

    private void PutMoneyOnCounter()
    {
        GameObject pileOfCoins = Instantiate(_pileOfCoinPrefab, Slot._pileOfCoinPosition, Quaternion.identity);
        TouchPileOfCoins touchPileOfCoins;

        if (pileOfCoins.TryGetComponent<TouchPileOfCoins>(out touchPileOfCoins))
        {
            touchPileOfCoins.CoinsValue = _interactibleOrder.Info.cost;
            touchPileOfCoins.ClientSlot = Slot;
        }
        else
            Debug.LogError("Error: Pile of coins do not have 'TouchPileOfCoins' script (gameObject named '" + pileOfCoins.name + "'.");
    }

    public void DestroyClient()
    {
        Destroy(this.gameObject);
        _interactibleOrder.DestroyOrder();
    }

    #endregion States
}
