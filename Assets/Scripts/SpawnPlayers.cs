using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    [SerializeField] GameObject treePrefab;
    [SerializeField] GameObject zombiePrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public int treeClusterCount;
    public int treesInCluster;
    public int treesRandeInCluster;
    public int randomTreesInMap;


    private bool hasSpawned = false;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient && !hasSpawned)
        {
            SpawnTreesAndZombies();
            hasSpawned = true;
        }

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }

    private void SpawnTreesAndZombies()
    {
        SpawnTreesInClusters();
        SpawnRandomTreesOnMap();

        for (int i = 0; i < 10; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

            GameObject zombie = PhotonNetwork.InstantiateRoomObject(zombiePrefab.name, randomPosition, Quaternion.identity);
            DontDestroyOnLoad(zombie);
        }
    }

    private void SpawnTreesInClusters()
    {


        for (int i = 0; i < treeClusterCount; i++)
        {
            Vector2 clusterPosition = new Vector2(Random.Range(minX + treesRandeInCluster, maxX - treesRandeInCluster), Random.Range(minY + treesRandeInCluster, maxY - treesRandeInCluster));

            for (int j = 0; j < treesInCluster; j++)
            {
                Vector2 randomOffset = new Vector2(Random.Range(-treesRandeInCluster, treesRandeInCluster), Random.Range(-treesRandeInCluster, treesRandeInCluster)); // Случайное смещение в пределах кластера

                Vector2 treePosition = clusterPosition + randomOffset;

                GameObject tree = PhotonNetwork.InstantiateRoomObject(treePrefab.name, treePosition, Quaternion.identity);
                DontDestroyOnLoad(tree);
            }
        }
    }

    private void SpawnRandomTreesOnMap()
    {
        for (int i = 0; i < randomTreesInMap; i++)
        {
            Vector2 randomTreePosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            GameObject randomTree = PhotonNetwork.InstantiateRoomObject(treePrefab.name, randomTreePosition, Quaternion.identity);
            DontDestroyOnLoad(randomTree);
        }
    }
}
