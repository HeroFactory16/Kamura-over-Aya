using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletManager : NetworkBehaviour
{
    public float speed = 12f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (IsServer)
        {
            var position = transform.position;
            position += Vector3.up * speed * Time.fixedDeltaTime;
            transform.position = position;
        }
    }
}
