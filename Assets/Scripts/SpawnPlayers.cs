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
        SpawnTrees();

        for (int i = 0; i < 10; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

            GameObject zombie = PhotonNetwork.InstantiateRoomObject(zombiePrefab.name, randomPosition, Quaternion.identity);
            DontDestroyOnLoad(zombie);
        }
    }

    private IEnumerator SpawnTreesWithDelay(List<Vector2> positions)
    {
        float delayBetweenTrees = 0.1f; // Задержка между спауном деревьев

        foreach (Vector2 position in positions)
        {
            SpawnTreeAtPosition(position);
            yield return new WaitForSeconds(delayBetweenTrees);
        }
    }

    private void SpawnTrees()
    {
        int treeCount = 100; // Общее количество деревьев
        List<Vector2> randomPositions = GenerateRandomPositions(treeCount);

        randomPositions.Sort((a, b) => b.y.CompareTo(a.y)); // Сортировка позиций по значению Y в порядке убывания

        StartCoroutine(SpawnTreesWithDelay(randomPositions));
    }

    private List<Vector2> GenerateRandomPositions(int count)
    {
        List<Vector2> positions = new List<Vector2>();
        for (int i = 0; i < count; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            positions.Add(randomPosition);
        }
        return positions;
    }

    private void SpawnTreeAtPosition(Vector2 position)
    {
        GameObject tree = PhotonNetwork.InstantiateRoomObject(treePrefab.name, position, Quaternion.identity);
        DontDestroyOnLoad(tree);
    }


}