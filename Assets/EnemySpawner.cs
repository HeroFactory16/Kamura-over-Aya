using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    public List<Enemy> enemies;
    float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsServer)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 1)
        {
            elapsedTime = 0;
            SpawnEnemyRpc();
        }
    }

    [Rpc(SendTo.Server)]
    void SpawnEnemyRpc()
    {
        var enemy = Instantiate(enemies[Random.Range(0, enemies.Count)]);
        var position = enemy.transform.position;

        float posy = Random.Range(2, 13.5f);
        if (posy > 4.5f)
        {
            posy -= 9f;
        }
        position += Vector3.up * Random.Range(-2.5f, 2.5f);
        position += Vector3.right * posy;
        enemy.transform.position = position;

        var networkEnemy = enemy.GetComponent<NetworkObject>();
        networkEnemy.Spawn();
    }
}
