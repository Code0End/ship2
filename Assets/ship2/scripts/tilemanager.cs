using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilemanager : MonoBehaviour
{
    public Transform player;

    public GameObject tile_prefab;

    public Vector3 spawnPos;
    public float spawnDistance = 10.0f;
    public float tileLength = 40f;
    private Vector3 moveDirection;
    public int numTilesonScreen = 2;

    private List<GameObject> activeTiles;

    private void Start()
    {
        moveDirection = Vector3.forward;
        activeTiles = new List<GameObject>();
        spawnPos = new Vector3(0, 0, 0);
        SpawnTile();      
    }

    void Update()
    {
        if(player == null)
        {
            return;
        }

        if (Vector3.Distance(player.position, spawnPos) > spawnDistance)
        {         
            if (activeTiles.Count > numTilesonScreen)
            {
                DeleteTile();
            }
            SpawnTile();
        }
    }

    private void SpawnTile()
    {
        GameObject tile = Instantiate(tile_prefab, new Vector3(player.position.x,0,player.position.z), Quaternion.identity);
        activeTiles.Add(tile);
        spawnPos += player.position;        
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

}
