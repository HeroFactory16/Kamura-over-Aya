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
    public NetworkVariable<int> lifePoints = new(3);
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
            position += Vector3.right * speed * Time.fixedDeltaTime;
            transform.position = position;
        }
        SendDeathAnimationRpc();
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

    [Rpc(SendTo.Server)]
    public void TakeDamagesRpc(int damages)
    {
        Debug.Log("I'm flying eye server!");
        lifePoints.Value -= damages;
        SendDamagesAnimationRpc();
    }

    [Rpc(SendTo.Everyone)]
    public void SendDamagesAnimationRpc()
    {
        flyingEyeAnimator.SetTrigger("IsAttacked");
    }

    [Rpc(SendTo.Everyone)]
    public void SendDeathAnimationRpc()
    {
        if (lifePoints.Value <= 0)
        {
            flyingEyeAnimator.SetTrigger("IsDying");
            speed = 0;
        }
    }

    [Rpc(SendTo.Everyone)]
    public void DestroyOnDeathRpc()
    {
        Destroy(gameObject);
    }
}
