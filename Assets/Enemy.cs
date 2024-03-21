using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : NetworkBehaviour
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>(Vector3.zero);
    public float speed = 5;
    public int attack = 2;
    public Animator flyingEyeAnimator;
    public NetPlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<NetPlayerController>();
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
            position += Vector3.right * speed * Time.fixedDeltaTime;
            transform.position = position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit!");
        if (IsServer)
        {
            if (collision.GetComponent<NetPlayerController>() != null)
            {
                collision.GetComponent<NetPlayerController>().TakeDamagesRpc(attack);
                Debug.Log("Ouch");
            }
        }
    }
}
