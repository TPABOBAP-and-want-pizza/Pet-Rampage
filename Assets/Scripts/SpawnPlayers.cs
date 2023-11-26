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
            //DontDestroyOnLoad(zombie);
        }
    }

    private void SpawnTrees()
    {
        int treeCount = 500; // ���������� �������� ��� ������

        float Xrange10 = (maxX - minX) / treeCount;
        float Yrange10 = (maxY - minY) / treeCount;

        for (int i = 0; i < treeCount; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(0, treeCount) * Xrange10 + minX + Random.Range(-2, 2), Random.Range(0, treeCount) * Yrange10 + minY + Random.Range(-1, 1));

            GameObject tree = PhotonNetwork.InstantiateRoomObject(treePrefab.name, randomPosition, Quaternion.identity);
            DontDestroyOnLoad(tree);

            // �������� ��������� SpriteRenderer �������
            SpriteRenderer treeRenderer = tree.GetComponent<SpriteRenderer>();
            if (treeRenderer != null)
            {
                // ������������� Sorting Layer � Sorting Order ��� ����������� ������� �����������
                treeRenderer.sortingLayerName = "TreesSortingLayer"; // �������� "YourSortingLayer" �� ��� ���� ����������
                treeRenderer.sortingOrder = i; // ����������� i ��� ����������� ������� ��� ������� ������
            }
        }
    }

}