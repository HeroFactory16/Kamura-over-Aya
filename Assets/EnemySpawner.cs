using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    public List<Enemy> enemies;
    public List<float> possiblePosY;
    float elapsedTime;
    // Start is called before the first frame update
    void Awake()
    {
        possiblePosY.Add(-4.5f);
        possiblePosY.Add(4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsServer)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 2)
        {
            elapsedTime = 0;
            SpawnEnemyRpc();
        }
    }

    [Rpc(SendTo.Server)]
    void SpawnEnemyRpc()
    {
        var enemy = Instantiate(enemies[Random.Range(0, enemies.Count)]); // Instancie un ennemi aléatoire parmi les prefabs possibles d'ennemis
        var position = enemy.transform.position;
        
        int posy = Random.Range(0, possiblePosY.Count);
        position += Vector3.up * Random.Range(-1.8f, 1.75f);
        position += Vector3.right * possiblePosY[posy];
        enemy.transform.position = position;

        var networkEnemy = enemy.GetComponent<NetworkObject>();
        networkEnemy.Spawn();
    }
}
