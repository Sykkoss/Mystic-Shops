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
    public AnimationCurve _timeBetweenClientsCurve;

    private Coroutine _clientSpawnerCoroutine;
    private ClientGenerator _clientGenerator;
    private Queue<ClientData> _clientsList;
    private bool _isClientsInfinite;


    private void Start()
    {
        _clientGenerator = GetComponent<ClientGenerator>();
        if (_clientGenerator == null)
            Debug.LogError("Error: No Component 'ClientGenerator' were found on gameObject '" + gameObject.name + "'. Could not continue.");
        else
        {
            _clientSpawnerCoroutine = null;
            PlayerTime.Instance.OnTimerFinish += StopSpawningClients;
            ClientSlotManager.Instance.InitSlots();
            _clientsList = _clientGenerator.GenerateFullLevelClients(_maxClients);
            _isClientsInfinite = true;
            StartSpawningClients();
        }
    }

    public void StartSpawningClients()
    {
        ClientSpawnerCurrentState = ClientSpawnerState.Spawning;
        if (!_isClientsInfinite)
            _clientSpawnerCoroutine = StartCoroutine(SpawnNextClientAndWaitUntilEmpty());
        else
            _clientSpawnerCoroutine = StartCoroutine(SpawnNextClientAndWaitInfinite());
    }

    private IEnumerator SpawnNextClientAndWaitInfinite()
    {
        float spawnTime;

        while (_isClientsInfinite)
        {
            if (!PlayerTime.Instance.IsPaused)
                SpawnClient();

            spawnTime = (!_isClientsInfinite) ? (0f) : (ComputeSpawnTime());
            yield return new WaitForSeconds(spawnTime);
        }
        StopSpawningClients();
    }

    private IEnumerator SpawnNextClientAndWaitUntilEmpty()
    {
        float spawnTime;

        while (_clientsList.Count > 0)
        {
            if (!PlayerTime.Instance.IsPaused)
                SpawnClient();

            spawnTime = (_clientsList.Count <= 0) ? (0f) : (ComputeSpawnTime());
            yield return new WaitForSeconds(spawnTime);
        }
        StopSpawningClients();
    }

    private void StopSpawningClients()
    {
        if (_clientSpawnerCoroutine != null)
            StopCoroutine(_clientSpawnerCoroutine);
        _clientSpawnerCoroutine = null;
        ClientSpawnerCurrentState = ClientSpawnerState.Finished;
        PlayerTime.Instance.OnTimerFinish -= StopSpawningClients;
    }

    private void SpawnClient()
    {
        Client clientSpawned;
        ClientData clientDataToSpawn;
        GameObject clientSpawnedGameObject;
        GameObject orderSpawnedGameObject;

        clientDataToSpawn = (_isClientsInfinite) ? (_clientGenerator.GetNextClient()) : (_clientsList.Dequeue());
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

    private float ComputeSpawnTime()
    {
        float timeBetweenClients = _timeBetweenClientsCurve.Evaluate(PlayerDifficulty.Instance.Difficulty / 100f) * 10f;
        float randomOffset = Random.Range(-1f, 1f);
        float clientWaitingOffset = 0f;
        
        for (int childNumber = 0; childNumber < transform.childCount; childNumber++)
            clientWaitingOffset += 0.2f;

        timeBetweenClients += clientWaitingOffset;
        timeBetweenClients += randomOffset;
        return timeBetweenClients;
    }
}
