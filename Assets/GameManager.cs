using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    //public List<NetPlayerController> alivePlayers;
    //public bool arePlayersDead;
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (IsServer)
    //    {
    //        if (CheckIfPlayersDead())
    //        {
    //            RestartNetworkScene();
    //        }
    //    }
    //}

    //bool CheckIfPlayersDead()
    //{
    //    arePlayersDead = true;
    //    foreach (NetPlayerController player in  alivePlayers)
    //    {
    //        if (player.lifePoints.Value >= 0)
    //        {
    //            arePlayersDead = false;
    //        }
    //    }
    //    return arePlayersDead;
    //}
    //void RestartNetworkScene()
    //{
    //    Debug.Log("Reloading scene");
    //    alivePlayers.Clear();
    //    SceneManager.LoadScene(0);
    //}
}
