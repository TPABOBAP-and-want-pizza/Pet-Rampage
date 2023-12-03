using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    [SerializeField] GameObject treePrefab;
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] GameObject helipadPrefab;

    private List<GameObject> zombies = new List<GameObject>();
    private List<GameObject> trees = new List<GameObject>();

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public int treeClusterCount;
    public int treesInCluster;
    public int treesRandeInCluster;
    public int randomTreesInMap;

    private bool hasSpawned = false;
    private bool night = false;
    private int nightTreesCount = 30;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient && !hasSpawned)
        {
            PhotonNetwork.InstantiateRoomObject(zombiePrefab.name, Vector3.zero, Quaternion.identity);
            SpawnTrees();
            SpawnZombies(30);
            hasSpawned = true;
        }

        SpawnPlayer();
    }
    private void Update()
    {
        if (GameManager.IsNight && !night)
        {
            hasSpawned = false;
            night = true;
        }
        if (!GameManager.IsNight && night)
        {
            night = false;
            hasSpawned = false;
        }

        if(!night && !hasSpawned)
        {
            SpawnRandomTreesOnMap(nightTreesCount);
            hasSpawned = true;
            Debug.Log("trees has spawned");
        }
        if(night && !hasSpawned)
        {
            SpawnZombies(30);
            Debug.Log("zombies spawned");
            hasSpawned = true;
        }
    }
    public void SpawnPlayer()
    {
        Vector2 randomPosition = new Vector2(Random.Range(6, 12), Random.Range(29, 34));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }

    private void SpawnZombies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

            GameObject zombie = PhotonNetwork.InstantiateRoomObject(zombiePrefab.name, randomPosition, Quaternion.identity);
            DontDestroyOnLoad(zombie);
            zombies.Add(zombie);
        }
    }
    private void SpawnTrees()
    {
        SpawnTreesInClusters();
        SpawnRandomTreesOnMap(randomTreesInMap);
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
                trees.Add(tree);
            }
        }
    }

    private void SpawnRandomTreesOnMap(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 randomTreePosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            GameObject randomTree = PhotonNetwork.InstantiateRoomObject(treePrefab.name, randomTreePosition, Quaternion.identity);
            DontDestroyOnLoad(randomTree);
            trees.Add(randomTree);
        }
    }
}
