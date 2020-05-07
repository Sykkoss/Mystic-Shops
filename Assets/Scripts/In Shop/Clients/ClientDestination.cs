using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientDestination
{
    public static Vector2 ComputeSpawnOrQuitPosition()
    {
        int randomSpawn = Random.Range(0, 2);
        float clientSpawnOffset = 2f;
        Bounds cameraBounds = GetCameraBounds();
        Vector2 spawnPosition = ClientSlotManager.Instance.transform.position;

        // If random is 0, spawn at the left of the screen. Otherwise, spawn at the right
        spawnPosition.x = (randomSpawn == 0) ? (-cameraBounds.size.x - clientSpawnOffset) : (cameraBounds.size.x + clientSpawnOffset);
        return spawnPosition;
    }

    private static Bounds GetCameraBounds()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize;
        Bounds bounds = new Bounds(
            Camera.main.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}
