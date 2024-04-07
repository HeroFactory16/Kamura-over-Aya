using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WorldBarrier : NetworkBehaviour
{
    public GlobalScore globalScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        globalScore = FindObjectOfType<GlobalScore>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsServer)
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                DecrementScoreRpc();
                Destroy(collision.GetComponent<Enemy>().gameObject);
            }
        }
    }

    [Rpc(SendTo.Server)]
    public void DecrementScoreRpc()
    {
        Debug.Log("Decrement called");
        if (globalScore != null)
        {
            globalScore.DecrementScoreRpc();
            Debug.Log("Decrement done");
        }
    }
}
