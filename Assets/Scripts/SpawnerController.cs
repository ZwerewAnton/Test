using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public EnemyController enemy;
    public GameObject player;
    public GameObject enemyZone;
    GameObject playerZone;
    void Start()
    {
        enemyZone = transform.GetChild(0).gameObject;
        playerZone = transform.GetChild(1).gameObject;
        SpawnEnemy();
        SpawnPlayer();
    }
    public void SpawnEnemy()
    {
        Transform transform = enemyZone.transform;
        EnemyController spawndeEnemy = Instantiate(enemy, transform.position, transform.rotation) as EnemyController;
    }
    public void SpawnPlayer()
    {
        Instantiate(player, playerZone.transform.position, playerZone.transform.rotation);
    }


}
