using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetPlayerController : NetworkBehaviour
{
    public PlayerInputAction PIA;
    public Vector2 direction;
    public float speed = 10f;
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>(Vector3.zero);
    public Animator playerAnimator;
    public SpriteRenderer playerSpriteRenderer;
    public NetworkVariable<int> lifePoints = new(30);
    public int attack = 5;
    public int score = 0;

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
        if (IsLocalPlayer)
        {
            SetPositionServerRpc(direction);
            SendDeathAnimationRpc();
        }

        if (IsServer)
        {
            transform.position = Position.Value;
        }
    }

    [Rpc(SendTo.Server)]
    public void SetPositionServerRpc(Vector2 actualInput)
    {
        var position = transform.position;
        position.x += actualInput.x * speed * NetworkManager.ServerTime.FixedDeltaTime;
        position.y += actualInput.y * speed * NetworkManager.ServerTime.FixedDeltaTime;
        Position.Value = position;
        SendAnimationRpc(actualInput.x != 0 || actualInput.y != 0);
        FlipCharacterRpc(actualInput);
    }

    [Rpc(SendTo.Server)]
    public void TakeDamagesRpc(int damages)
    {
        Debug.Log("I'm server!");
        lifePoints.Value -= damages;
        SendDamagesAnimationRpc();
    }

    [Rpc(SendTo.Everyone)]
    public void SendDamagesAnimationRpc()
    {
        playerAnimator.SetTrigger("IsTakingDamages");
    }

    [Rpc(SendTo.Everyone)]
    public void FlipCharacterRpc(Vector2 actualInput)
    {
        if (!playerAnimator.GetBool("IsDead"))
        {
            if (actualInput.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (actualInput.x > 0)
            {
                transform.rotation = Quaternion.identity;
            }
        } 
    }

    [Rpc(SendTo.Everyone)]
    public void SendDeathAnimationRpc()
    {
        if (lifePoints.Value <= 0)
        {
            playerAnimator.SetBool("IsDead", true);
            speed = 0;
        }
    }

    [Rpc(SendTo.Everyone)]
    public void SendAnimationRpc(bool isMoving)
    {
        playerAnimator.SetBool("IsRunning", isMoving);
    }

    [Rpc(SendTo.Everyone)]
    public void SetAttack1ServerRpc(bool canAttack)
    {
        if (canAttack)
        {
            playerAnimator.SetTrigger("IsAttacking1");
        }
    }

    [Rpc(SendTo.Everyone)]
    public void SetAttack2ServerRpc(bool canAttack)
    {
        if (canAttack)
        {
            playerAnimator.SetTrigger("IsAttacking2");
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (NetworkObject.IsLocalPlayer)
        {
            Debug.Log("Name "+ gameObject.name);
            PIA = new PlayerInputAction();
            PIA.Enable();
            PIA.Player.Move.started += context => direction = context.ReadValue<Vector2>();
            PIA.Player.Move.canceled += context => direction = context.ReadValue<Vector2>();
            PIA.Player.Move.performed += context => direction = context.ReadValue<Vector2>();
            PIA.Player.Attack1.started += context => SetAttack1ServerRpc(true);
            PIA.Player.Attack1.canceled += context => SetAttack1ServerRpc(false);
            PIA.Player.Attack1.performed += context => SetAttack1ServerRpc(false);
            PIA.Player.Attack2.started += context => SetAttack2ServerRpc(true);
            PIA.Player.Attack2.canceled += context => SetAttack2ServerRpc(false);
            PIA.Player.Attack2.performed += context => SetAttack2ServerRpc(false);
        }
    }
}
