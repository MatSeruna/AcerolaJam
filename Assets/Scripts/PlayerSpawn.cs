using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform[] playerSpawns;
    public GameObject playerPrefab;

    public Player SpawnPlayer()
    {
        int spawnPosIndex = Random.Range(0, playerSpawns.Length);
        Player player = Instantiate(playerPrefab, playerSpawns[spawnPosIndex].position, Quaternion.identity).GetComponent<Player>();
        return player;
    }
}
