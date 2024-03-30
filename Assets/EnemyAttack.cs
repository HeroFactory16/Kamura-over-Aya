using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyAttack : NetworkBehaviour
{
    public Enemy Parent;
    public Animator EnemyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Is within range");
        if (IsServer)
        {
            if (collision.GetComponent<NetPlayerController>() != null)
            {
                SendAttackAnimationRpc();
            }
        }
    }

    [Rpc(SendTo.Everyone)]
    void SendAttackAnimationRpc()
    {
        EnemyAnimator.SetTrigger("IsPlayerDetected2");
    }
}
