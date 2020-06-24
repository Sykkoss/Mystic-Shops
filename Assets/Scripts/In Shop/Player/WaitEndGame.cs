using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitEndGame : MonoBehaviour
{
    [SerializeField]
    private Transform _clientsParent;

    private void Start()
    {
        PlayerTime.Instance.OnTimerFinish += () => StartCoroutine(WaitAllClientsOrder());
    }

    private IEnumerator WaitAllClientsOrder()
    {
        while (_clientsParent.childCount > 0)
            yield return null;
        StartCollectingCoins();
    }

    private void StartCollectingCoins()
    {
        StartCoroutine(CollectAllCoins());
    }

    private IEnumerator CollectAllCoins()
    {
        TouchPileOfCoins[] pileOfCoins = FindObjectsOfType<TouchPileOfCoins>();

        foreach (TouchPileOfCoins currentPile in pileOfCoins)
        {
            currentPile.Touch();
            yield return new WaitForSeconds(0.5f);
        }
        PlayerTime.Instance.OnTimerFinish -= () => StartCoroutine(WaitAllClientsOrder());

        // Display end-game menu
    }
}
