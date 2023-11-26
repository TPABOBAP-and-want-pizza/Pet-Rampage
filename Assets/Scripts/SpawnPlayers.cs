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

        for (int i = 0; i < 10; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

            GameObject zombie = PhotonNetwork.InstantiateRoomObject(zombiePrefab.name, randomPosition, Quaternion.identity);
            DontDestroyOnLoad(zombie);
        }
    }

    private void SpawnTreesInClusters()
    {
        int treeClusterCount = 7; // Количество кластеров деревьев
        int treesInCluster = 30; // Количество деревьев в кластере

        for (int i = 0; i < treeClusterCount; i++)
        {
            Vector2 clusterPosition = new Vector2(Random.Range(minX + 17f, maxX - 17f), Random.Range(minY + 17f, maxY - 17f));

            for (int j = 0; j < treesInCluster; j++)
            {
                Vector2 randomOffset = new Vector2(Random.Range(-17f, 17f), Random.Range(-17f, 17f)); // Случайное смещение в пределах кластера

                Vector2 treePosition = clusterPosition + randomOffset;

                GameObject tree = PhotonNetwork.InstantiateRoomObject(treePrefab.name, treePosition, Quaternion.identity);
                DontDestroyOnLoad(tree);
            }
        }
    }
}
