using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public ClientSpawnerState ClientSpawnerCurrentState { get; private set; }

    public int _maxClients;
    public GameObject _clientGenericPrefab;
    public GameObject _orderUIPrefab;
    public Transform _orderParent;

    private ClientGenerator _clientGenerator;
    private Queue<ClientData> _clientsList;


    private void Start()
    {
        _clientGenerator = GetComponent<ClientGenerator>();
        if (_clientGenerator == null)
            Debug.LogError("Error: No Component 'ClientGenerator' were found on gameObject '" + gameObject.name + "'. Could not continue.");
        else
        {
            ClientSlotManager.Instance.InitSlots();
            _clientsList = _clientGenerator.GenerateFullLevelClients(_maxClients);
            StartSpawningClients();
        }
    }

    public void StartSpawningClients()
    {
        ClientSpawnerCurrentState = ClientSpawnerState.Spawning;
        StartCoroutine(SpawnNextClientAndWait());
    }

    private IEnumerator SpawnNextClientAndWait()
    {
        while (_clientsList.Count > 0)
        {
            SpawnClient();
            yield return new WaitForSeconds(4f);
        }
        ClientSpawnerCurrentState = ClientSpawnerState.Finished;
    }

    private void SpawnClient()
    {
        ClientData clientDataToSpawn = _clientsList.Dequeue();
        GameObject clientSpawnedGameObject;
        GameObject orderSpawnedGameObject;
        Client clientSpawned;

        clientSpawnedGameObject = Instantiate(_clientGenericPrefab, ClientDestination.ComputeSpawnOrQuitPosition(), Quaternion.identity, transform);
        if (!clientSpawnedGameObject.TryGetComponent<Client>(out clientSpawned))
            Debug.LogError("Error: No Component 'Client' was found on gameObject '" + clientSpawnedGameObject.name + "'.");
        else
        {
            orderSpawnedGameObject = Instantiate(_orderUIPrefab, Camera.main.WorldToScreenPoint(clientSpawnedGameObject.transform.GetChild(0).transform.position), Quaternion.identity, _orderParent);
            clientSpawned.CreateClient(clientDataToSpawn, orderSpawnedGameObject.GetComponent<InteractibleOrder>());
            orderSpawnedGameObject.SetActive(false);
        }
    }
}
