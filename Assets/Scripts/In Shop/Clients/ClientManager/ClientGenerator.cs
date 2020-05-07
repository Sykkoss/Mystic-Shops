using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientGenerator : MonoBehaviour
{
    public List<ClientData> _availableClients;


    public Queue<ClientData> GenerateFullLevelClients(int maxClients)
    {
        Queue<ClientData> levelClients = new Queue<ClientData>();

        while (levelClients.Count <= maxClients)
        {
            ClientData nextClient = GetNextClient();
            levelClients.Enqueue(nextClient);
        }
        return levelClients;
    }

    private ClientData GetNextClient()
    {
        return _availableClients[Random.Range(0, _availableClients.Count)];
    }
}
