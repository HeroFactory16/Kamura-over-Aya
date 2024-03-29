using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAttack : NetworkBehaviour
{
    public NetPlayerController Parent;
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
        Debug.Log("Enemy is in collider");
        if (IsServer)
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                Debug.Log("Enemy is attacked");
                collision.GetComponent<Enemy>().TakeDamagesRpc(Parent.attack);
            }
        }
    }
}
