using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetWorkAction : MonoBehaviour
{
    public NetworkManager nm;
    public Canvas UIContainer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartServer()
    {
        nm.StartHost();
        UIContainer.enabled = false;
    }

    public void ConnectToServer()
    {
        nm.StartClient();
        nm.OnClientConnectedCallback += obj => Debug.Log("Connected: " + obj);
        UIContainer.enabled = false;
    }
}
